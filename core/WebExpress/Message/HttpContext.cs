using Microsoft.AspNetCore.Http.Features;

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
        /// Satz von Features.
        /// </summary>
        public IFeatureCollection Features { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="contextFeatures">Anfänglicher Satz von Features.</param>
        public HttpContext(IFeatureCollection contextFeatures)
        {
            var connectionFeature = contextFeatures.Get<IHttpConnectionFeature>();

            Features = contextFeatures;
            ID = connectionFeature.ConnectionId;
            Request = new Request(contextFeatures);
        }
    }
}
