using WebExpress.WebAttribute;

namespace WebExpress.UI.WebResource
{
    /// <summary>
    /// Delivery of a resource embedded in the assembly.
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