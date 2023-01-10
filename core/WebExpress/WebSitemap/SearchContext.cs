using System.Globalization;
using WebExpress.Message;
using WebExpress.Uri;

namespace WebExpress.WebSitemap
{
    /// <summary>
    /// The search context for searches within the sitemap.
    /// </summary>
    public class SearchContext
    {
        /// <summary>
        /// Returns the culture.
        /// </summary>
        public CultureInfo Culture { get; internal set; }

        /// <summary>
        /// The uri of the resource being requested.
        /// </summary>
        public IUri Uri { get; internal set; }

        /// <summary>
        /// Returns the request.
        /// </summary>
        public Request Request { get; internal set; }
    }
}
