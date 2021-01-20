using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
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
using WebExpress.WebResource;

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
        /// Warteschlange für noch nicht verarbeitete Anfragen
        /// </summary>
        private Queue<TcpClient> Queue { get; } = new Queue<TcpClient>();

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

                // Ausführung der Plugins starten
                PluginManager.Boot();

                // Ausführung der Anwendungen starten
                ApplicationManager.Boot();

                // Ausführung der Module starten
                ModuleManager.Boot();
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
            // Client-Task
            var clientTask = Task.Factory.StartNew(() =>
            {
                while (!ClientTokenSource.IsCancellationRequested)
                {
                    TcpClient client = null;

                    lock (Queue)
                    {
                        client = Queue.Count > 0 ? Queue.Dequeue() : null;
                    }

                    if (client != null)
                    {
                        // Worker-Task
                        var workerTask = Task.Factory.StartNew(() =>
                        {
                            HandleClient(client);
                        });
                    }
                    else
                    {
                        Thread.Sleep(10);
                    }
                }
            }, ClientTokenSource.Token);

            // Server-Task
            while (!ServerTokenSource.IsCancellationRequested)
            {

                if (Listener == null)
                {
                    break;
                }

                try
                {
                    var client = Listener.AcceptTcpClient();

                    lock (Queue)
                    {
                        if (Queue.Count < ConnectionLimit)
                        {
                            Queue.Enqueue(client);
                        }
                        else
                        {
                            client.Close();
                        }
                    }
                }
                catch
                {
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

            lock (Queue)
            {
                Listener.Stop();
                Listener = null;
            }

            // Ausführung der Module beenden
            ModuleManager.ShutDown();

            // Ausführung der Anwendungen beenden
            ApplicationManager.ShutDown();

            // Ausführung der Plugins beenden
            PluginManager.ShutDown();
        }

        /// <summary>
        /// Behandelt einen eingehenden Anforderung
        /// Wird nebenläufig ausgeführt
        /// </summary>
        /// <param name="client">Der Client</param>
        private void HandleClient(TcpClient client)
        {
            //lock (Context)
            {
                var stopwatch = Stopwatch.StartNew();

                Response response = new ResponseNotFound();
                var request = null as Request;
                var ip = client.Client.RemoteEndPoint;
                using var stream = client.GetStream();
                var reader = new BinaryReader(stream);
                var writer = new StreamWriter(stream);
                var moduleContext = null as IModuleContext;
                var uri = null as IUri;

                if (!stream.DataAvailable)
                {
                    //Context.Log.Debug(message: this.I18N("httpserver.rejected"), args: ip);
                    //return;
                }

                Context.Log.Info(message: this.I18N("httpserver.connected"), args: ip);


                try
                {
                    request = Request.Create(reader, ip.ToString());
                }
                catch (Exception ex)
                {
                    Context.Log.Exception(ex);
                }

                if (request == null)
                {
                    response = new ResponseBadRequest();
                    var statusPage = CreateStatusPage(response.Status, request, moduleContext, uri);
                    if (statusPage != null)
                    {
                        statusPage.StatusMessage = "";
                        response.Content = statusPage;
                    }
                    else
                    {
                        response.Content = $"<h4>{response.Status}</h4>Bad Request";
                    }

                    response.HeaderFields.ContentLength = response.Content != null ? response.Content.ToString().Length : 0;
                }

                try
                {
                    Context.Log.Debug(message: this.I18N("httpserver.request"), args: new object[] { ip, $"{request?.Method} {request?.URL} {request?.Version}" });

                    var resource = ResourceManager.Find(request?.URL.TrimEnd('/'));
                    if (resource != null && resource.Type != null)
                    {
                        var type = resource.Type;
                        var culture = Culture;
                        moduleContext = resource.ModuleContext;
                        uri = new UriResource(resource.ModuleContext, request.URL, resource, culture);

                        try
                        {
                            culture = new CultureInfo(request.HeaderFields?.AcceptLanguage?.TrimStart().Substring(0, 2).ToLower());
                        }
                        catch
                        {
                        }

                        if (type?.Assembly.CreateInstance(type?.FullName) is IResource instance)
                        {
                            if (instance is II18N i18n)
                            {
                                i18n.Culture = culture;
                            }

                            if (instance is Resource res)
                            {
                                res.Request = request;
                                res.Uri = uri;
                                res.Context = resource.ModuleContext;
                                res.ResourceContext = resource.ResourceContext;

                                foreach (var p in request.Param)
                                {
                                    res.AddParam(p.Value);
                                }

                                foreach (var v in resource.Variables)
                                {
                                    res.AddParam(v.Key, v.Value, ParameterScope.Url);
                                }
                            }

                            if (instance is IPage page)
                            {
                                page.Title = resource.Title;
                            }

                            instance.Initialization();
                            instance.PreProcess(request);
                            response = instance.Process(request);
                            response = instance.PostProcess(request, response);

                            if (instance is IPage)
                            {
                                response.Content += $"<!--{ stopwatch.ElapsedMilliseconds } ms -->";
                            }
                        }
                    }

                    if (response is ResponseNotFound)
                    {
                        var statusPage = CreateStatusPage(response.Status, request, moduleContext, uri);
                        if (statusPage != null)
                        {
                            response.Content = statusPage;
                        }
                        else
                        {
                            response.Content = $"<h4>{ response.Status }</h4>";
                        }

                        response.HeaderFields.ContentLength = response.Content != null ? response.Content.ToString().Length : 0;
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

                    response = new ResponseInternalServerError();
                    var statusPage = CreateStatusPage(response.Status, request, moduleContext, uri);
                    if (statusPage != null)
                    {
                        statusPage.StatusMessage = $"<h4>Message</h4>{ ex.Message }<br/><br/>" +
                            $"<h5>Source</h5>{ ex.Source }<br/><br/>" +
                            $"<h5>StackTrace</h5>{ ex.StackTrace.Replace("\n", "<br/>\n") }<br/><br/>" +
                            $"<h5>InnerException</h5>{ ex.InnerException?.ToString().Replace("\n", "<br/>\n") }";

                        response.Content = statusPage;
                    }
                    else
                    {
                        response.Content = $"<h4>{ response.Status }</h4> { ex }";
                    }

                    response.HeaderFields.ContentLength = response.Content != null ? response.Content.ToString().Length : 0;
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
        }

        /// <summary>
        /// Erstellt eine Statusseite
        /// </summary>
        /// <param name="statusCode">Der Statuscode</param>
        /// <returns></returns>
        private IPageStatus CreateStatusPage(int statusCode, Request request, IModuleContext moduleContext, IUri uri)
        {
            try
            {
                var culture = Culture;
                var statusPage = null as IPageStatus;

                try
                {
                    culture = new CultureInfo(request?.HeaderFields?.AcceptLanguage?.TrimStart().Substring(0, 2).ToLower());
                }
                catch
                {
                }

                if (moduleContext != null && !string.IsNullOrWhiteSpace(moduleContext.ApplicationID))
                {
                    statusPage = ResponseManager.Create(statusCode, moduleContext?.ApplicationID);
                }

                if (statusPage == null)
                {
                    statusPage = ResponseManager.Create(statusCode, "webexpress");
                }

                if (statusPage == null)
                {
                    return null;
                }

                statusPage.StatusCode = statusCode;

                if (statusPage is II18N i18n)
                {
                    i18n.Culture = culture;
                }

                if (statusPage is Resource res)
                {
                    res.Request = request;
                    res.Uri = uri;
                    res.Context = moduleContext;
                }
                statusPage.Initialization();
                statusPage.Process();

                return statusPage;
            }
            catch
            {

            }

            return null;
        }
    }
}
