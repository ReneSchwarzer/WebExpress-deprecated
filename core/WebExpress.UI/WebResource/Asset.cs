using WebExpress.WebAttribute;

namespace WebExpress.UI.WebResource
{
    /// <summary>
    /// Delivery of a resource embedded in the assembly.
    /// </summary>
    [Id("Asset")]
    [Segment("assets")]
    [ContextPath("/")]
    [IncludeSubPaths(true)]
    [Module("webexpress.ui")]
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