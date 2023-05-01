using WebExpress.WebAttribute;

namespace WebExpress.WebApp.WebResource
{
    /// <summary>
    /// Lieferung einer im Assamby eingebetteten Ressource
    /// </summary>
    [WebExID("Asset")]
    [WebExSegment("assets")]
    [WebExContextPath("/")]
    [WebExIncludeSubPaths(true)]
    [WebExModule("webexpress.webapp")]
    public sealed class Asset : WebExpress.WebResource.ResourceAsset
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Asset()
        {
        }
    }
}