using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using WebExpress.Application;
using WebExpress.Config;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.Module;
using WebExpress.Plugin;
using WebExpress.Uri;
using WebExpress.WebJob;
using WebExpress.WebPage;
using WebExpress.WebResource;

namespace WebExpress
{
    /// <summary>
    /// siehe RFC 2616
    /// </summary>
    public class HttpServer : IHost, II18N
    {
        /// <summary>
        /// Liefert das Verbindungslimit
        /// </summary>
        private const int ConnectionLimit = 100;

        /// <summary>
        /// Liefert den Listener, welcher auf die Anfragen reagiert
        /// </summary>
        private HttpListener Listener { get; } = new HttpListener();

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
        /// Konstruktor
        /// </summary>
        /// <param name="context">Der Serverkontext</param>
        public HttpServer(HttpServerContext context)
        {
            Context = new HttpServerContext
            (
                context.Uris,
                context.AssetPath,
                context.ConfigPath,
                context.ContextPath,
                context.Culture,
                context.Log
            );
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
                Context.Log.Info(message: this.I18N("webexpress:httpserver.run"));
            }

            if (!HttpListener.IsSupported)
            {
                Context.Log.Error(message: this.I18N("webexpress:httpserver.notsupported"));
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

            foreach (var prefix in Context.Uris)
            {
                Listener.Prefixes.Add(prefix);
            }

            try
            {
                Listener.Start();
            }
            catch (HttpListenerException ex)
            {
                Context.Log.Info(message: this.I18N("webexpress:httpserver.listener.exeption"));
                Context.Log.Info(message: this.I18N("webexpress:httpserver.listener.try"));
                foreach (var prefix in Context.Uris)
                {
                    Context.Log.Info(message: "    " + this.I18N("webexpress:httpserver.listener.windows"), args: prefix);
                }


                Context.Log.Exception(ex);

                return;
            }

            // Server-Task
            while (!ServerTokenSource.IsCancellationRequested)
            {
                var context = Listener.GetContext();

                if (count++ < ConnectionLimit)
                {
                    Task.Factory.StartNew(() => { HandleClient(context); }, ClientTokenSource.Token);
                    //HandleClient(context);
                }
                else
                {
                    Context.Log.Warning(message: this.I18N("webexpress:httpserver.connectionlimit"));
                }

                count--;
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
        /// <param name="context">Der Context ders HttpListener</param>
        private void HandleClient(HttpListenerContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            var request = new Request(context.Request);
            var response = null as Response;
            var culture = Culture;
            var uri = request?.Uri;

            Context.Log.Info(message: this.I18N("webexpress:httpserver.connected"), args: request.RemoteEndPoint);

            // Kultur ermitteln
            try
            {
                culture = new CultureInfo(request?.Header?.AcceptLanguage.FirstOrDefault());
            }
            catch
            {
            }

            try
            {
                Context.Log.Debug(message: this.I18N("webexpress:httpserver.request"), args: new object[] { request.RemoteEndPoint, $"{ request?.Method } { request?.Uri } { "HTTP/" + request?.Version }" });

                // Suche Seite in Sitemap
                var resource = ResourceManager.Find(request?.Uri.ToString(), new SearchContext()
                {
                    Culture = culture,
                    Uri = request?.Uri
                });

                // Ressource ausführen
                if (resource?.Instance != null)
                {
                    var moduleContext = ModuleManager.GetModule(resource?.Context?.Module);
                    request.Uri = new UriResource(moduleContext, uri, resource, culture);
                    request.AddParameter(resource.Variables.Select(x => new Parameter(x.Key, x.Value, ParameterScope.Url)));

                    resource.Instance?.PreProcess(request);
                    response = resource.Instance?.Process(request);
                    response = resource.Instance?.PostProcess(request, response);

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

            // Response an Client schicken
            try
            {
                context.Response.ProtocolVersion = new Version(1, 1);
                context.Response.StatusCode = response.Status;
                context.Response.StatusDescription = response.Reason;
                context.Response.RedirectLocation = response.Header.Location;
                //context.Response.KeepAlive = false;

                if (!string.IsNullOrWhiteSpace(response.Header.CacheControl))
                {
                    context.Response.AddHeader("Cache-Control", response.Header.CacheControl);
                }

                if (!string.IsNullOrWhiteSpace(response.Header.ContentType))
                {
                    context.Response.AddHeader("Content-Type", response.Header.ContentType);
                }

                if (response.Header.WWWAuthenticate)
                {
                    context.Response.AddHeader("WWW-Authenticate", "Basic realm=\"Bereich\"");
                }

                if (!request.Header.Cookies.Where(x => x.Name.Equals("session")).Any() && request.Session != null)
                {
                    context.Response.SetCookie(new Cookie("session", request.Session.ID.ToString()) { Expires = DateTime.MaxValue });
                }

                foreach (var c in response.Header.CustomHeader)
                {
                    context.Response.AppendHeader(c.Key, c.Value);
                }

                if (response?.Content is byte[] byteContent)
                {
                    context.Response.ContentLength64 = byteContent.Length;

                    var bw = new BinaryWriter(context.Response.OutputStream);
                    bw.Write(byteContent);
                    bw.Flush();
                }
                else if (response?.Content is string strContent)
                {
                    var content = request.Header.ContentEncoding.GetBytes(strContent);

                    context.Response.ContentLength64 = content.Length;

                    var bw = new BinaryWriter(context.Response.OutputStream);
                    bw.Write(content);
                    bw.Flush();
                }
                else if (response?.Content is IHtmlNode htmlContent)
                {
                    var content = request.Header.ContentEncoding.GetBytes(htmlContent?.ToString());

                    context.Response.ContentLength64 = content.Length;

                    var bw = new BinaryWriter(context.Response.OutputStream);
                    bw.Write(content);
                    bw.Flush();
                }
                else
                {
                }

                context.Response.OutputStream.Close();
            }
            catch (Exception ex)
            {
                Context.Log.Error(context.Request.RemoteEndPoint.Address + ": " + ex.Message);
            }

            stopwatch.Stop();

            Context.Log.Info(message: this.I18N("webexpress:httpserver.request.done"), args: new object[] { request.RemoteEndPoint, stopwatch.ElapsedMilliseconds, response.Status });
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
            var moduleContext = ResponseManager.GetDefaultModule(response.Status, request?.Uri.ToString(), context?.Module);

            try
            {
                culture = new CultureInfo(request?.Header?.AcceptLanguage?.FirstOrDefault()?.ToLower());
            }
            catch
            {
            }

            IPageStatus statusPage = ResponseManager.Create(massage, response.Status, moduleContext, new UriAbsolute(request?.Uri.ToString()));

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
