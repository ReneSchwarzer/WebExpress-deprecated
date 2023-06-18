using WebExpress.WebAttribute;

namespace WebExpress.WebApp.WebResource
{
    /// <summary>
    /// Delivery of a resource embedded in the assamby.
    /// </summary>
    [Segment("assets")]
    [ContextPath("/")]
    [IncludeSubPaths(true)]
    [Module<Module>]
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