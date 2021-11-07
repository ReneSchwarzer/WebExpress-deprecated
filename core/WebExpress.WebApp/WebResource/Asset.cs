using System.IO;
using WebExpress.Attribute;

namespace WebExpress.WebApp.WebResource
{
    /// <summary>
    /// Lieferung einer im Assamby eingebetteten Ressource
    /// </summary>
    [ID("Asset")]
    [Segment("assets")]
    [Path("/")]
    [IncludeSubPaths(true)]
    [Module("webexpress.webapp")]
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