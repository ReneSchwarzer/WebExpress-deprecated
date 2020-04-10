using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace WebExpress.Config
{
    /// <summary>
    /// Klasse zum Auslesen der Konfigurationsdatei
    /// </summary>
    [XmlRoot(ElementName = "config", IsNullable = false)]
    public sealed class PluginConfig
    {
        [XmlAttribute(AttributeName = "version", DataType = "int")]
        public int Version { get; set; }

        /// <summary>
        /// Der Basispfad des Plugins
        /// </summary>
        [XmlElement(ElementName = "urlbasepath")]
        public string UrlBasePath { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PluginConfig()
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="fileName">Die zu ladende XML-Datei</param>
        public PluginConfig(string fileName)
        {
            if (!string.IsNullOrWhiteSpace(fileName) && File.Exists(fileName))
            {
                var doc = XDocument.Load(fileName);
                Load(doc.Root);
            }
        }

        /// <summary>
        /// Lädt die Konfigurationsdatei aus dem XML-Element.
        /// </summary>
        /// <param name="xml">Der Root-Knoten (<config/>) der Konfigurationsdatei</param>
        public void Load(XElement xml)
        {
            if (xml.Element("urlbasepath") != null)
            {
                UrlBasePath = xml.Element("urlbasepath").Value;
            }
        }
    }
}