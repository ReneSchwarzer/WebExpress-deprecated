using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using WebExpress.Application;
using WebExpress.Config;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.Module;
using WebExpress.Plugin;
using WebExpress.Uri;
using WebExpress.WebPage;
using WebExpress.WebResource;
using WebExpress.WebJob;

namespace WebExpress
{
    /// <summary>
    /// siehe RFC 2616
    /// </summary>
    public class HttpServer : IHost, II18N
    {
        private const int ConnectionLimit = 100;

        /// <summary>
        /// Setzt oder Liefert den 
        /// </summary>
        public int Port { get; private set; }

        /// <summary>
        /// Setzt oder liefert den Listener
        /// </summary>
        private TcpListener Listener { get; set; }

        /// <summary>
        /// Threadbeendigung des Servers
        /// </summary>
        private CancellationTokenSource ServerTokenSource { get; } = new CancellationTokenSource();

        /// <summary>
        /// Threadbeendigung des Clients
        /// </summary>
        private CancellationTokenSource ClientTokenSource { get; } = new CancellationTokenSource();

        /// <summary>
        /// Liefert oder setzt die Konfiguration
        /// </summary>
        public HttpServerConfig Config { get; set; }

        /// <summary>
        /// Liefert oder setzt den Kontext
        /// </summary>
        public IHttpServerContext Context { get; protected set; }

        /// <summary>
        /// Liefert die Kultur
        /// </summary>
        public CultureInfo Culture { get; set; }

        /// <summary>
        /// Liefert den I18N-Key
        /// </summary>
        public string I18N_PluginID => "webexpress";

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="port">Der Port auf dem der Server höhren soll</param>
        /// <param name="context">Der Serverkontext</param>
        public HttpServer(int port, HttpServerContext context)
        {
            Context = new HttpServerContext
            (
                context.Uri,
                port,
                context.AssetPath,
                context.ConfigPath,
                context.ContextPath,
                context.Culture,
                context.Log
            );

            Port = port;
            Culture = Context.Culture;

            InternationalizationManager.Register(typeof(HttpServer).Assembly, "webexpress");

            InternationalizationManager.Initialization(Context);
            PluginManager.Initialization(Context);
            ApplicationManager.Initialization(Context);
            ModuleManager.Initialization(Context);
            ResourceManager.Initialization(Context);
            ResponseManager.Initialization(Context);
            ScheduleManager.Initialization(Context);
        }

        /// <summary>
        /// Startet den HTTPServer
        /// </summary>
        public void Start()
        {
            if (Context != null && Context.Log != null)
            {
                Context.Log.Info(message: this.I18N("httpserver.run"), args: Port);
            }

            if (Config != null)
            {
                // Plugins laden
                PluginManager.Register(Config.Deployment);

                // Internationalisierung laden
                InternationalizationManager.Register();

                // Anwendungen laden
                ApplicationManager.Register();

                // Module laden
                ModuleManager.Register();

                // Ressourcen laden
                ResourceManager.Register();

                // Statusseiten
                ResponseManager.Register();

                // Jobs
                ScheduleManager.Register();

                // Ausführung der Plugins starten
                PluginManager.Boot();

                // Ausführung der Anwendungen starten
                ApplicationManager.Boot();

                // Ausführung der Module starten
                ModuleManager.Boot();

                // Ausführung des Terminplaners starten
                ScheduleManager.Boot();
            }

            Listener = new TcpListener(IPAddress.Any, Port);
            Listener.Start();

            var task = Task.Factory.StartNew(() =>
            {
                Run();
            }, ServerTokenSource.Token);

        }

        /// <summary>
        /// Beginnt auf dem Port zu lauschen
        /// Wird nebenläufig ausgeführt
        /// </summary>
        private void Run()
        {
            var count = 0;

            // Server-Task
            while (!ServerTokenSource.IsCancellationRequested)
            {
                if (Listener == null)
                {
                    break;
                }

                var client = Listener.AcceptTcpClient();
                count++;

                if (count < ConnectionLimit)
                {
                    var workerTask = Task.Factory.StartNew(() =>
                    {
                        HandleClient(client);
                        client.Close();
                        count--;
                    });
                }
                else
                {
                    client.Close();
                    count--;

                    Context.Log.Warning(message: this.I18N("httpserver.connectionlimit"));
                }
            }
        }

        /// <summary>
        /// Stoppt den HTTPServer
        /// </summary>
        public void Stop()
        {
            // Laufende Threads beenden
            ClientTokenSource.Cancel();
            ServerTokenSource.Cancel();

            Listener.Stop();
            Listener = null;

            // Ausführung der Module beenden
            ModuleManager.ShutDown();

            // Ausführung der Anwendungen beenden
            ApplicationManager.ShutDown();

            // Ausführung der Plugins beenden
            PluginManager.ShutDown();

            // Ausführung des Terminplaners beenden
            ScheduleManager.ShutDown();
        }

        /// <summary>
        /// Behandelt einen eingehenden Anforderung
        /// Wird nebenläufig ausgeführt
        /// </summary>
        /// <param name="client">Der Client</param>
        private void HandleClient(TcpClient client)
        {
            Response response = new ResponseNotFound();
            var request = null as Request;
            var ip = client.Client.RemoteEndPoint;
            using var stream = client.GetStream();
            var reader = new BinaryReader(stream);
            var writer = new StreamWriter(stream);
            var stopwatch = Stopwatch.StartNew();
            var culture = Culture;

            //if (!stream.DataAvailable)
            //{
            //    //Context.Log.Debug(message: this.I18N("httpserver.rejected"), args: ip);
            //    //return;
            //}

            Context.Log.Info(message: this.I18N("httpserver.connected"), args: ip);

            try
            {
                request = Request.Create(reader, ip.ToString());
                stopwatch.Restart();
            }
            catch (Exception ex)
            {
                Context.Log.Exception(ex);
            }

            try
            {
                culture = new CultureInfo(request?.HeaderFields?.AcceptLanguage?.TrimStart().Substring(0, 2).ToLower());
            }
            catch
            {
            }

            // Anfrage behandeln
            if (request != null)
            {
                try
                {
                    Context.Log.Debug(message: this.I18N("httpserver.request"), args: new object[] { ip, $"{request?.Method} {request?.Uri} {request?.Version}" });

                    // Suche Seite in Sitemap
                    var resource = ResourceManager.Find(request?.Uri.ToString().TrimEnd('/'), new SearchContext()
                    {
                        Culture = culture,
                        Uri = request.Uri
                    });

                    var moduleContext = ModuleManager.GetModule(resource.Context.ApplicationID, resource.Context.ModuleID);
                    request.Uri = new UriResource(moduleContext, request.Uri, resource, culture);
                    request.AddParameter(resource.Variables.Select(x => new Parameter(x.Key, x.Value, ParameterScope.Url)));

                    // Ressource ausführen
                    if (resource != null)
                    {
                        resource.Instance.PreProcess(request);
                        response = resource.Instance.Process(request);
                        response = resource.Instance.PostProcess(request, response);

                        if (resource.Instance is IPage)
                        {
                            response.Content += $"<!-- { stopwatch.ElapsedMilliseconds } ms -->";
                        }

                        if (response is ResponseNotFound)
                        {
                            response = CreateStatusPage<ResponseNotFound>(string.Empty, request, resource.Context);
                        }
                    }
                    else
                    {
                        // Seite nicht gefunden
                        response = CreateStatusPage<ResponseNotFound>(string.Empty, request);
                    }
                }
                catch (RedirectException ex)
                {
                    if (ex.Permanet)
                    {
                        response = new ResponseRedirectPermanentlyMoved(ex.Url);
                    }
                    else
                    {
                        response = new ResponseRedirectTemporarilyMoved(ex.Url);
                    }
                }
                catch (Exception ex)
                {
                    Context.Log.Exception(ex);

                    var message = $"<h4>Message</h4>{ ex.Message }<br/><br/>" +
                            $"<h5>Source</h5>{ ex.Source }<br/><br/>" +
                            $"<h5>StackTrace</h5>{ ex.StackTrace.Replace("\n", "<br/>\n") }<br/><br/>" +
                            $"<h5>InnerException</h5>{ ex.InnerException?.ToString().Replace("\n", "<br/>\n") }";

                    response = CreateStatusPage<ResponseInternalServerError>(message, request);
                }
            }
            else
            {
                // Anfrage fehlerhaft
                response = CreateStatusPage<ResponseBadRequest>(string.Empty, request);
            }

            // Response an Client schicken
            try
            {
                writer.Write(response.GetHeader());
                writer.Flush();

                if (response.Content is byte[] content)
                {
                    using var bw = new BinaryWriter(writer.BaseStream);
                    bw.Write(content);
                }
                else
                {
                    writer.Write(response.Content ?? "");
                }

                writer.Flush();
            }
            catch (Exception ex)
            {
                Context.Log.Error(ip + ": " + ex.Message);
            }

            stopwatch.Stop();

            Context.Log.Info(message: this.I18N("httpserver.request.done"), args: new object[] { ip, stopwatch.ElapsedMilliseconds, response.Status });
        }

        /// <summary>
        /// Erstellt eine Statusseite
        /// </summary>
        /// <param name="massage">Die Fehlernachricht</param>
        /// <param name="request">Die Anfrage</param>
        /// <param name="context">Der Kontext der aufgerufenen Ressource oder null</param>
        /// <returns></returns>
        private Response CreateStatusPage<T>(string massage, Request request, IResourceContext context = null) where T : Response, new()
        {
            var response = new T() as Response;
            var culture = Culture;
            var statusPage = null as IPageStatus;
            var moduleContext = ResponseManager.GetDefaultModule(response.Status, request?.Uri.ToString(), context?.ModuleID);

            try
            {
                culture = new CultureInfo(request?.HeaderFields?.AcceptLanguage?.TrimStart().Substring(0, 2).ToLower());
            }
            catch
            {
            }

            statusPage = ResponseManager.Create(massage, response.Status, moduleContext, request?.Uri);

            if (statusPage != null)
            {
                if (statusPage is II18N i18n)
                {
                    i18n.Culture = culture;
                }

                if (statusPage is Resource resource)
                {
                    resource.Initialization(new ResourceContext(moduleContext));
                }

                statusPage.PreProcess(request);
                response = statusPage.Process(request);
                statusPage.PostProcess(request, response);

                return response;
            }

            return response;
        }
    }
}
