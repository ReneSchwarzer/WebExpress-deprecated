﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Sockets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using WebExpress.Config;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.Uri;
using WebExpress.WebApplication;
using WebExpress.WebComponent;
using WebExpress.WebJob;
using WebExpress.WebModule;
using WebExpress.WebPackage;
using WebExpress.WebPage;
using WebExpress.WebPlugin;
using WebExpress.WebResource;
using WebExpress.WebSitemap;

namespace WebExpress
{
    /// <summary>
    /// The web server for processing http requests (see RFC 2616). The web server uses Kestrel internally.
    /// </summary>
    public class HttpServer : IHost, II18N, IHttpApplication<HttpContext>
    {
        /// <summary>
        /// Event is triggered after the web server is started.
        /// </summary>
        public event EventHandler Started;

        /// <summary>
        /// Provides the KestrelServer, which responds to the requests.
        /// </summary>
        private KestrelServer Kestrel { get; set; }

        /// <summary>
        /// Server thread termination.
        /// </summary>
        private CancellationToken ServerToken { get; } = new CancellationToken();

        /// <summary>
        /// Returns or sets the configuration
        /// </summary>
        public HttpServerConfig Config { get; set; }

        /// <summary>
        /// Returns or sets the context.
        /// </summary>
        public IHttpServerContext Context { get; protected set; }

        /// <summary>
        /// Returns the culture.
        /// </summary>
        public CultureInfo Culture { get; set; }

        /// <summary>
        /// Returns the execution time of the web server.
        /// </summary>
        public static DateTime ExecutionTime { get; } = DateTime.Now;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Der Serverkontext.</param>
        public HttpServer(HttpServerContext context)
        {
            Context = new HttpServerContext
            (
                context.Uri,
                context.Endpoints,
                context.PackagePath,
                context.AssetPath,
                context.DataPath,
                context.ConfigPath,
                context.ContextPath,
                context.Culture,
                context.Log,
                this
            );

            Culture = Context.Culture;

            ComponentManager.Initialization(Context);
        }

        /// <summary>
        /// Starts the HTTP(S) server.
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
                // start the manager
                ComponentManager.Execute();

                //// start running the scheduler
                //ScheduleManager.Execute();
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

            serverOptions.Value.Limits.MaxConcurrentConnections = Config?.Limit?.ConnectionLimit > 0 ? Config?.Limit?.ConnectionLimit : serverOptions.Value.Limits.MaxConcurrentConnections;
            serverOptions.Value.Limits.MaxRequestBodySize = Config?.Limit?.UploadLimit > 0 ? Config?.Limit?.UploadLimit : serverOptions.Value.Limits.MaxRequestBodySize;

            foreach (var endpoint in Config.Endpoints)
            {
                AddEndpoint(serverOptions, endpoint);
            }

            Kestrel = new KestrelServer(serverOptions, transport, logger);

            Kestrel.StartAsync(this, ServerToken);

            Context.Log.Info(message: this.I18N("webexpress:httpserver.start"), args: new object[] { ExecutionTime.ToShortDateString(), ExecutionTime.ToLongTimeString() });

            Started?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Adds an endpoint.
        /// </summary>
        /// <param name="serverOptions">The server options.</param>
        /// <param name="endPoint">The endpoint.</param>
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
        /// Adds an endpoint.
        /// </summary>
        /// <param name="serverOptions">The server options.</param>
        /// <param name="endPoint">The endpoint.</param>
        private void AddEndpoint(OptionsWrapper<KestrelServerOptions> serverOptions, IPEndPoint endPoint)
        {
            serverOptions.Value.Listen(endPoint);

            Context.Log.Info(message: this.I18N("webexpress:httpserver.listen"), args: endPoint.ToString());
        }

        /// <summary>
        /// Adds an endpoint.
        /// </summary>
        /// <param name="serverOptions">The server options.</param>
        /// <param name="pfxFile">The certificate.</param>
        /// <param name="password">The password to the certificate.</param>
        /// <param name="endPoint">The endpoint.</param>
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
        /// Stops the HTTP(S) server
        /// </summary>
        public void Stop()
        {
            // End running threads
            Kestrel.StopAsync(ServerToken);

            // Stop running
            ComponentManager.ShutDown();
        }

        /// <summary>
        /// Handles an incoming request
        /// Concurrent execution
        /// </summary>
        /// <param name="context">The context of the web request.</param>
        /// <returns>The response to be sent back to the caller.</returns>
        private Response HandleClient(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            var request = context.Request;
            var response = null as Response;
            var culture = request.Culture;
            var uri = request?.Uri;

            Context.Log.Info(message: this.I18N("webexpress:httpserver.connected"), args: context?.RemoteEndPoint);

            try
            {
                Context.Log.Debug(message: this.I18N("webexpress:httpserver.request"), args: new object[] { context.RemoteEndPoint, $"{request?.Method} {request?.Uri} {request?.Protocoll}" });

                // search page in sitemap
                var resource = SitemapManager.Find(context?.Uri.ToString(), new SearchContext()
                {
                    Culture = culture,
                    Uri = request?.Uri
                });

                // execute resource
                if (resource?.Instance != null)
                {
                    var moduleContext = resource?.Context?.ModuleContext;
                    request.Uri = new UriResource(moduleContext, uri, resource, culture);
                    request.AddParameter(resource.Variables.Select(x => new Parameter(x.Key, x.Value, ParameterScope.Url)));

                    resource.Instance?.PreProcess(request);
                    response = resource.Instance?.Process(request);
                    response = resource.Instance?.PostProcess(request, response);

                    if (resource.Instance is IPage)
                    {
                        response.Content += $"<!-- {stopwatch.ElapsedMilliseconds} ms -->";
                    }

                    if (response is ResponseNotFound)
                    {
                        response = CreateStatusPage<ResponseNotFound>(string.Empty, request, resource.Context, resource.ApplicationContext);
                    }

                    if
                    (
                        !response.Header.Cookies.Where(x => x.Name.Equals("session")).Any() &&
                        !request.Header.Cookies.Where(x => x.Name.Equals("session")).Any() &&
                        request.Session != null
                    )
                    {
                        var cookie = new Cookie("session", request.Session.ID.ToString()) { Expires = DateTime.MaxValue };
                        response.Header.Cookies.Add(cookie);
                    }
                }
                else
                {
                    // Resource not found
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

                var message = $"<h4>Message</h4>{ex.Message}<br/><br/>" +
                        $"<h5>Source</h5>{ex.Source}<br/><br/>" +
                        $"<h5>StackTrace</h5>{ex.StackTrace.Replace("\n", "<br/>\n")}<br/><br/>" +
                        $"<h5>InnerException</h5>{ex.InnerException?.ToString().Replace("\n", "<br/>\n")}";

                response = CreateStatusPage<ResponseInternalServerError>(message, request);
            }

            stopwatch.Stop();

            Context.Log.Info(message: this.I18N("webexpress:httpserver.request.done"), args: new object[] { context?.RemoteEndPoint, stopwatch.ElapsedMilliseconds, response.Status });

            return response;
        }

        /// <summary>
        /// Sends the response message
        /// </summary>
        /// <param name="context">The context of the request</param>
        /// <param name="response">The reply message</param>
        /// <returns>Sending the message as a task, which is executed concurrently.</returns>
        private async Task SendResponseAsync(HttpContext context, Response response)
        {
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

                if (response.Header.Cookies.Any())
                {
                    responseFeature.Headers.SetCookie = string.Join(" ", response.Header.Cookies);
                }

                if (response?.Content is byte[] byteContent)
                {
                    responseFeature.Headers.ContentLength = byteContent.Length;
                    await responseBodyFeature.Stream.WriteAsync(byteContent);
                    await responseBodyFeature.Stream.FlushAsync();
                }
                else if (response?.Content is string strContent)
                {
                    var content = context.Encoding.GetBytes(strContent);

                    responseFeature.Headers.ContentLength = content.Length;
                    await responseBodyFeature.Stream.WriteAsync(content);
                    await responseBodyFeature.Stream.FlushAsync();
                }
                else if (response?.Content is IHtmlNode htmlContent)
                {
                    var content = context.Encoding.GetBytes(htmlContent?.ToString());

                    responseFeature.Headers.ContentLength = content.Length;
                    await responseBodyFeature.Stream.WriteAsync(content);
                    await responseBodyFeature.Stream.FlushAsync();
                }

                responseBodyFeature.Stream.Close();
            }
            catch (Exception ex)
            {
                Context.Log.Error(context.RemoteEndPoint + ": " + ex.Message);
            }
        }

        /// <summary>
        /// Creates a status page
        /// </summary>
        /// <param name="massage">The error message.</param>
        /// <param name="request">The request.</param>
        /// <param name="applicationContext">The context of the application being called or null.</param>
        /// <param name="resourceContext">The context of the resource being called or null.</param>
        /// <returns>The response.</returns>
        private Response CreateStatusPage<T>(string massage, Request request, IResourceContext resourceContext = null, IApplicationContext applicationContext = null) where T : Response, new()
        {
            var response = new T() as Response;
            var culture = Culture;
            var moduleContext = ComponentManager.ResponseManager.GetDefaultModule(response.Status, request?.Uri.ToString(), resourceContext?.ModuleContext);

            try
            {
                culture = new CultureInfo(request?.Header?.AcceptLanguage?.FirstOrDefault()?.ToLower());
            }
            catch
            {
            }

            IPageStatus statusPage = ComponentManager.ResponseManager.Create(massage, response.Status, applicationContext, moduleContext, new UriAbsolute(request?.Uri.ToString()));

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
        /// Creates a status page
        /// </summary>
        /// <param name="massage">The error message</param>
        /// <param name="context">The context of the request</param>
        /// <returns>The response</returns>
        private Response CreateStatusPage<T>(string massage, HttpContext context) where T : Response, new()
        {
            var response = new T() as Response;
            var culture = Culture;
            var moduleContext = ComponentManager.ResponseManager.GetDefaultModule(response.Status, context.Uri.ToString());

            IPageStatus statusPage = ComponentManager.ResponseManager.Create(massage, response.Status, null, moduleContext, new UriAbsolute(context?.Uri.ToString()));

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

                statusPage.PreProcess(null);
                response = statusPage.Process(null);
                statusPage.PostProcess(null, response);

                return response;
            }

            return response;
        }

        /// <summary>
        /// Create an HttpContext with a collection of HTTP features.
        /// </summary>
        /// <param name="contextFeatures">A collection of HTTP features to use to create the HttpContext.</param>
        /// <returns>The HttpContext created.</returns>
        public HttpContext CreateContext(IFeatureCollection contextFeatures)
        {
            try
            {
                return new HttpContext(contextFeatures, this.Context);
            }
            catch (Exception ex)
            {
                return new HttpExceptionContext(ex, contextFeatures);
            }
        }

        /// <summary>
        /// Verarbeitet einen HttpContext asynchron.
        /// </summary>
        /// <param name="context">Der HttpContext, den der Vorgang verarbeitet.</param>
        /// <returns>Stellt einen asynchronen Vorgang zur Verfügung, welcher den HttpContext verarbeitet.</returns>
        public async Task ProcessRequestAsync(HttpContext context)
        {
            if (context is HttpExceptionContext exceptionContext)
            {
                var message = "<html><head><title>404</title></head><body>" +
                       $"<h4>Message</h4>{exceptionContext.Exception.Message}<br/><br/>" +
                       $"<h5>Source</h5>{exceptionContext.Exception.Source}<br/><br/>" +
                       $"<h5>StackTrace</h5>{exceptionContext.Exception.StackTrace.Replace("\n", "<br/>\n")}<br/><br/>" +
                       $"<h5>InnerException</h5>{exceptionContext.Exception.InnerException?.ToString().Replace("\n", "<br/>\n")}" +
                       "</body></html>";

                var response500 = CreateStatusPage<ResponseInternalServerError>(message, context);

                await SendResponseAsync(exceptionContext, response500);

                return;
            }

            var response = HandleClient(context);

            await SendResponseAsync(context, response);
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
