using System.IO;
using WebExpress.Attribute;
using WebExpress.Module;
using WebExpress.Uri;

namespace WebExpress.UI.WebResource
{
    /// <summary>
    /// Lieferung einer im Assamby eingebetteten Ressource
    /// </summary>
    [ID("Asset")]
    [Segment("assets")]
    [Path("/")]
    [IncludeSubPaths(true)]
    [Module("webexpress")]
    public sealed class ResourceAsset : WebExpress.WebResource.ResourceAsset
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ResourceAsset()
        {
        }
    }
}