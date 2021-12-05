using Microsoft.AspNetCore.Http.Features;
using System.Linq;
using System.Net;
using System.Text;
using WebExpress.Uri;

namespace WebExpress.Message
{
    public class HttpContext
    {
        /// <summary>
        /// Liefert die ID
        /// </summary>
        public string ID { get; protected set; }

        /// <summary>
        /// Liefert die Anforderung
        /// </summary>
        public Request Request { get; protected set; }

        /// <summary>
        /// Ruft die IP-Adresse und Anschlussnummer des Servers ab, an den die Anforderung gerichtet ist.
        /// </summary>
        public EndPoint LocalEndPoint { get; protected set; }

        /// <summary>
        /// Ruft die IP-Adresse und Anschlussnummer des Clients ab, von dem die Anforderung stammt.
        /// </summary>
        public EndPoint RemoteEndPoint { get; protected set; }

        /// <summary>
        /// Satz von Features.
        /// </summary>
        public IFeatureCollection Features { get; protected set; }

        /// <summary>
        /// Das Encoding
        /// </summary>
        public Encoding Encoding { get; protected set; } = Encoding.Default;

        /// <summary>
        /// Liefert die URL
        /// </summary>
        public IUri Uri { get; internal set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        internal HttpContext()
        {

        }

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

            Encoding = requestFeature.Headers.ContentEncoding.Any() ? Encoding.GetEncoding(requestFeature.Headers.ContentEncoding) : Encoding.Default;

            Request = new Request(contextFeatures);
        }
    }
}
