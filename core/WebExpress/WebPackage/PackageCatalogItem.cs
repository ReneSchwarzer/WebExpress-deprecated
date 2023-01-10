using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using WebExpress.WebApplication;
using WebExpress.WebModule;
using WebExpress.WebPlugin;
using WebExpress.WebResource;

namespace WebExpress.WebPackage
{
    [XmlRoot("package")]
    public class PackageCatalogItem
    {
        /// <summary>
        /// Liefert oder setzt die ID
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; }

        /// <summary>
        /// Liefert oder setzt den Dateinamen
        /// </summary>
        [XmlAttribute("file")]
        public string File { get; set; }

        /// <summary>
        /// Liefert oder setzt den Zustand
        /// </summary>
        [XmlAttribute("state")]
        public PackageCatalogeItemState State { get; set; }

        /// <summary>
        /// Die zum Paket gehörenden Plugins
        /// </summary>
        [XmlIgnore]
        public List<IPluginContext> Plugins { get; } = new List<IPluginContext>();

        /// <summary>
        /// Liefert oder setzt das Paket. Kann null sein, wenn das Paket nicht vorhanden
        /// </summary>
        [XmlIgnore]
        public PackageItem Package { get; set; }

        /// <summary>
        /// Umwandlung in eine Stringreräsentation des Objektes
        /// </summary>
        /// <returns>Das Objekt als String</returns>
        public override string ToString()
        {
            return Id;
        }
    }
}
