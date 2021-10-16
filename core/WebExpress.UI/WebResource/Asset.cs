using System.IO;
using WebExpress.Attribute;

namespace WebExpress.UI.WebResource
{
    /// <summary>
    /// Lieferung einer im Assamby eingebetteten Ressource
    /// </summary>
    [ID("Asset")]
    [Segment("assets")]
    [Path("/")]
    [IncludeSubPaths(true)]
    [Module("webexpress.ui")]
    public sealed class Asset : WebExpress.WebResource.ResourceAsset
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public Asset()
        {
        }
    }
}