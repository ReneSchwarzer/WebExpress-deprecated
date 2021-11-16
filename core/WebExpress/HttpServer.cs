﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Sockets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
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
    public class HttpServer : IHost, II18N, IHttpApplication<HttpContext>
    {
        /// <summary>
        /// Liefert den KestrelServer, welcher auf die Anfragen reagiert
        /// </summary>
        private KestrelServer Kestrel { get; set; }

        /// <summary>
        /// Threadbeendigung des Servers
        /// </summary>
        private CancellationToken ServerToken { get; } = new CancellationToken();

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
                context.Endpoints,
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

            var logger = new LogFactory();
            var transportOptions = new OptionsWrapper<SocketTransportOptions>(new SocketTransportOptions());
            var transport = new SocketTransportFactory(transportOptions, logger);
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddMemoryCache();
            serviceCollection.AddLogging(x =>
            {
                x.SetMinimumLevel(LogLevel.Trace);
                x.AddProvider(logger);
            });
            serviceCollection.AddHttpLogging(x =>
            {
                x.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
            });

            var serverOptions = new OptionsWrapper<KestrelServerOptions>(new KestrelServerOptions()
            {
                AllowSynchronousIO = true,
                AllowResponseHeaderCompression = true,
                AddServerHeader = true,
                ApplicationServices = serviceCollection.BuildServiceProvider()

            });

            serverOptions.Value.Limits.MaxConcurrentConnections = Config?.Limit?.ConnectionLimit;

            foreach (var endpoint in Config.Endpoints)
            {
                AddEndpoint(serverOptions, endpoint);
            }

            Kestrel = new KestrelServer(serverOptions, transport, logger);

            Kestrel.StartAsync(this, ServerToken);
        }

        /// <summary>
        /// Fügt ein Endpunkt hinzu
        /// </summary>
        /// <param name="serverOptions">Die Serveroptionen</param>
        /// <param name="endPoint">Der Endpunkt</param>
        private void AddEndpoint(OptionsWrapper<KestrelServerOptions> serverOptions, EndpointConfig endPoint)
        {
            try
            {
                var uri = new UriAbsolute(endPoint.Uri);
                var asterisk = uri.Authority.Host.Equals("*");

                var host = asterisk ? Dns.GetHostEntry(Dns.GetHostName()) : Dns.GetHostEntry(uri.Authority.Host);
                var addressList = host.AddressList
                    .Union(asterisk ? Dns.GetHostEntry("localhost").AddressList : Array.Empty<IPAddress>())
                    .Where(x => x.AddressFamily == AddressFamily.InterNetwork || x.AddressFamily == AddressFamily.InterNetworkV6);

                var port = uri.Authority.Port;
                if (!port.HasValue)
                {
                    port = uri.Scheme switch
                    {
                        UriScheme.Http => 80,
                        UriScheme.Https => 443,
                        _ => 80
                    };
                }

                Context.Log.Info(message: this.I18N("webexpress:httpserver.endpoint"), args: endPoint.Uri);

                foreach (var ipAddress in addressList)
                {
                    var ep = new IPEndPoint(ipAddress, port.Value);

                    switch (uri.Scheme)
                    {
                        case UriScheme.Https: { AddEndpoint(serverOptions, ep, endPoint.PfxFile, endPoint.Password); break; }
                        default: { AddEndpoint(serverOptions, ep); break; }
                    }

                }
            }
            catch (Exception ex)
            {
                Context.Log.Error(message: this.I18N("webexpress:httpserver.listen.exeption"), args: endPoint);
                Context.Log.Exception(ex);

            }
        }

        /// <summary>
        /// Fügt ein Endpunkt hinzu
        /// </summary>
        /// <param name="serverOptions">Die Serveroptionen</param>
        /// <param name="endPoint">Der Endpunkt</param>
        private void AddEndpoint(OptionsWrapper<KestrelServerOptions> serverOptions, IPEndPoint endPoint)
        {
            serverOptions.Value.Listen(endPoint);

            Context.Log.Info(message: this.I18N("webexpress:httpserver.listen"), args: endPoint.ToString());
        }

        /// <summary>
        /// Fügt ein Endpunkt hinzu
        /// </summary>
        /// <param name="serverOptions">Die Serveroptionen</param>
        /// <param name="pfxFile">Das Zertifikat</param>
        /// <param name="password">Das Passwort zum Zertifikat</param>
        /// <param name="endPoint">Der Endpunkt</param>
        private void AddEndpoint(OptionsWrapper<KestrelServerOptions> serverOptions, IPEndPoint endPoint, string pfxFile, string password)
        {
            serverOptions.Value.Listen(endPoint, configure => 
            { 
                var cert = new X509Certificate2(pfxFile, password);
                
                    configure.UseHttps(cert);
            });

            Context.Log.Info(message: this.I18N("webexpress:httpserver.listen"), args: endPoint.ToString());
        }

        /// <summary>
        /// Stoppt den HTTPServer
        /// </summary>
        public void Stop()
        {
            // Laufende Threads beenden
            Kestrel.StopAsync(ServerToken);

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
        /// <param name="context">Der Kontext der Webanforderung</param>
        private async Task HandleClient(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            var request = context.Request;
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
                Context.Log.Debug(message: this.I18N("webexpress:httpserver.request"), args: new object[] { request.RemoteEndPoint, $"{ request?.Method } { request?.Uri } { request?.Protocoll }" });

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
                var responseFeature = context.Features.Get<IHttpResponseFeature>();
                var responseBodyFeature = context.Features.Get<IHttpResponseBodyFeature>();

                responseFeature.StatusCode = response.Status;
                responseFeature.ReasonPhrase = response.Reason;
                responseFeature.Headers.KeepAlive = "true";

                if (response.Header.Location != null)
                {
                    responseFeature.Headers.Location = response.Header.Location;
                }

                if (!string.IsNullOrWhiteSpace(response.Header.CacheControl))
                {
                    responseFeature.Headers.CacheControl = response.Header.CacheControl;
                }

                if (!string.IsNullOrWhiteSpace(response.Header.ContentType))
                {
                    responseFeature.Headers.ContentType = response.Header.ContentType;
                }

                if (response.Header.WWWAuthenticate)
                {
                    responseFeature.Headers.WWWAuthenticate = "Basic realm=\"Bereich\"";
                }

                if (!request.Header.Cookies.Where(x => x.Name.Equals("session")).Any() && request.Session != null)
                {
                    var cookie = new Cookie("session", request.Session.ID.ToString()) { Expires = DateTime.MaxValue };
                    responseFeature.Headers.SetCookie = new StringValues(cookie.ToString());
                }

                ////    foreach (var c in response.Header.CustomHeader)
                ////    {
                ////        context.Response.AppendHeader(c.Key, c.Value);
                ////    }

                if (response?.Content is byte[] byteContent)
                {
                    responseFeature.Headers.ContentLength = byteContent.Length;
                    await responseBodyFeature.Stream.WriteAsync(byteContent);
                    await responseBodyFeature.Stream.FlushAsync();
                }
                else if (response?.Content is string strContent)
                {
                    var content = request.Header.ContentEncoding.GetBytes(strContent);

                    responseFeature.Headers.ContentLength = content.Length;
                    await responseBodyFeature.Stream.WriteAsync(content);
                    await responseBodyFeature.Stream.FlushAsync();
                }
                else if (response?.Content is IHtmlNode htmlContent)
                {
                    var content = request.Header.ContentEncoding.GetBytes(htmlContent?.ToString());

                    responseFeature.Headers.ContentLength = content.Length;
                    await responseBodyFeature.Stream.WriteAsync(content);
                    await responseBodyFeature.Stream.FlushAsync();
                }

                responseBodyFeature.Stream.Close();
            }
            catch (Exception ex)
            {
                Context.Log.Error(request.RemoteEndPoint + ": " + ex.Message);
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

        /// <summary>
        /// Erstellen Sie einen HttpContext mit einer Auflistung von HTTP-Features.
        /// </summary>
        /// <param name="contextFeatures">Eine Auflistung von HTTP-Features, die zum Erstellen des HttpContexts verwendet werden sollen.</param>
        /// <returns>Der erstellte HttpContext.</returns>
        public HttpContext CreateContext(IFeatureCollection contextFeatures)
        {
            return new HttpContext(contextFeatures);
        }

        /// <summary>
        /// Verarbeitet einen HttpContext asynchron.
        /// </summary>
        /// <param name="context">Der HttpContext, den der Vorgang verarbeitet.</param>
        /// <returns>Stellt einen asynchronen Vorgang zur Verfügung, welcher den HttpContext verarbeitet.</returns>
        public Task ProcessRequestAsync(HttpContext context)
        {
            return HandleClient(context);
        }

        /// <summary>
        /// Verwerfen eines angegebenen HttpContexts.
        /// </summary>
        /// <param name="context">Der zu verwerfende HttpContext.</param>
        /// <param name="exception">Die Ausnahme, die ausgelöst wird, wenn die Verarbeitung nicht erfolgreich abgeschlossen wurde, andernfalls NULL.</param>
        public void DisposeContext(HttpContext context, Exception exception)
        {

        }
    }
}
