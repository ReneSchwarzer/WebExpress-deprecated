using WebExpress.WebAttribute;

namespace WebExpress.UI.WebResource
{
    /// <summary>
    /// Delivery of a resource embedded in the assembly.
    /// </summary>
    [WebExID("Asset")]
    [WebExSegment("assets")]
    [WebExContextPath("/")]
    [WebExIncludeSubPaths(true)]
    [WebExModule("webexpress.ui")]
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