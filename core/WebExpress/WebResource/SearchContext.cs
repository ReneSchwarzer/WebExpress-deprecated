using System.Globalization;
using WebExpress.Message;
using WebExpress.Uri;

namespace WebExpress.WebResource
{
    public class SearchContext
    {
         /// <summary>
        /// Liefert die Kultur
        /// </summary>
        public CultureInfo Culture { get; internal set; }

        /// <summary>
        /// Die Uri der Ressource, welche angefragt wird
        /// </summary>
        public IUri Uri { get; internal set; }

        /// <summary>
        /// Liefert oder setzt die Anfrage
        /// </summary>
        public Request Request { get; internal set; }

    }
}
