using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace WebExpress.WebPackage
{
    [XmlRoot("catalog")]
    public class PackageCatalog
    {
        /// <summary>
        /// Liefert oder setzt die Paketeinträge im Katalog
        /// </summary>
        [XmlElement("package")]
        public List<PackageCatalogItem> Packages { get; } = new List<PackageCatalogItem>();

        /// <summary>
        /// Sucht ein bestimmtes Katalogelement
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <returns>Das Katalogelement</returns>
        public PackageCatalogItem Find(string id) 
        {
            return Packages.Where(x => x.Id.Equals(id)).FirstOrDefault();
        }
    }
}
