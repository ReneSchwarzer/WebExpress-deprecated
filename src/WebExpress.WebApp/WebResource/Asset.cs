using WebExpress.WebAttribute;

namespace WebExpress.WebApp.WebResource
{
    /// <summary>
    /// Delivery of a resource embedded in the assamby.
    /// </summary>
    [WebExSegment("assets")]
    [WebExContextPath("/")]
    [WebExIncludeSubPaths(true)]
    [WebExModule<Module>]
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