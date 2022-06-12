using System.IO;
using WebExpress.WebAttribute;

namespace WebExpress.UI.WebResource
{
    /// <summary>
    /// Lieferung einer im Assamby eingebetteten Ressource
    /// </summary>
    [Id("Asset")]
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