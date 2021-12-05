using Microsoft.AspNetCore.Http.Features;
using System;
using System.Net;
using WebExpress.Uri;

namespace WebExpress.Message
{
    public class HttpExceptionContext : HttpContext
    {
        /// <summary>
        /// Liefert oder setzt eine Fehlernachricht, wenn der Context nicht erstellt werden konnte
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="exception">Eine Ausnahme, welche die Erstellung des Kontextes verhindert hat</param>
        /// <param name="contextFeatures">Anfänglicher Satz von Features.</param>
        public HttpExceptionContext(Exception exception, IFeatureCollection contextFeatures)
        {
            var connectionFeature = contextFeatures.Get<IHttpConnectionFeature>();
            var requestFeature = contextFeatures.Get<IHttpRequestFeature>();

            Features = contextFeatures;
            ID = connectionFeature.ConnectionId;

            Uri = new UriRelative(requestFeature.Path);
            LocalEndPoint = new IPEndPoint(connectionFeature.LocalIpAddress, connectionFeature.LocalPort);
            RemoteEndPoint = new IPEndPoint(connectionFeature.RemoteIpAddress, connectionFeature.RemotePort);

            Exception = exception;
        }
    }
}
