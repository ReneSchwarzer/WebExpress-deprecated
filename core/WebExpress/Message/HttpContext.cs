using Microsoft.AspNetCore.Http.Features;
using System;
using System.Net;
using WebExpress.Uri;

namespace WebExpress.Message
{
    public class HttpContext
    {
        /// <summary>
        /// Liefert die ID
        /// </summary>
        public string ID { get; private set; }

        /// <summary>
        /// Liefert die Anforderung
        /// </summary>
        public Request Request { get; private set; }

        /// <summary>
        /// Ruft die IP-Adresse und Anschlussnummer des Servers ab, an den die Anforderung gerichtet ist.
        /// </summary>
        public EndPoint LocalEndPoint { get; private set; }

        /// <summary>
        /// Ruft die IP-Adresse und Anschlussnummer des Clients ab, von dem die Anforderung stammt.
        /// </summary>
        public EndPoint RemoteEndPoint { get; private set; }

        /// <summary>
        /// Satz von Features.
        /// </summary>
        public IFeatureCollection Features { get; private set; }

        /// <summary>
        /// Liefert die URL
        /// </summary>
        public IUri Uri { get; internal set; }

        /// <summary>
        /// Liefert oder setzt eine Fehlernachricht, wenn der Context nicht erstellt werden konnte
        /// </summary>
        internal Exception CreatedException { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="contextFeatures">Anfänglicher Satz von Features.</param>
        public HttpContext(IFeatureCollection contextFeatures)
        {
            var connectionFeature = contextFeatures.Get<IHttpConnectionFeature>();
            var requestFeature = contextFeatures.Get<IHttpRequestFeature>();

            Features = contextFeatures;
            ID = connectionFeature.ConnectionId;
            Uri = new UriRelative(requestFeature.Path);
            LocalEndPoint = new IPEndPoint(connectionFeature.LocalIpAddress, connectionFeature.LocalPort);
            RemoteEndPoint = new IPEndPoint(connectionFeature.RemoteIpAddress, connectionFeature.RemotePort);

            Request = new Request(contextFeatures);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="exception">Eine Ausnahme, welche die Erstellung des Kontextes verhindert hat</param>
        /// <param name="contextFeatures">Anfänglicher Satz von Features.</param>
        public HttpContext(Exception exception, IFeatureCollection contextFeatures)
        {
            var connectionFeature = contextFeatures.Get<IHttpConnectionFeature>();
            var requestFeature = contextFeatures.Get<IHttpRequestFeature>();

            Features = contextFeatures;
            ID = connectionFeature.ConnectionId;

            Uri = new UriRelative(requestFeature.Path);
            LocalEndPoint = new IPEndPoint(connectionFeature.LocalIpAddress, connectionFeature.LocalPort);
            RemoteEndPoint = new IPEndPoint(connectionFeature.RemoteIpAddress, connectionFeature.RemotePort);

            CreatedException = exception;
        }
    }
}
