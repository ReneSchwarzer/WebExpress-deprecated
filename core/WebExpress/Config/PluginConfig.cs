using System.Xml.Serialization;

namespace WebExpress.Config
{
    /// <summary>
    /// Klasse zum Auslesen der Konfigurationsdatei
    /// </summary>
    [XmlRoot("config", IsNullable = false)]
    public sealed class PluginConfig
    {
        [XmlAttribute("version", DataType = "int")]
        public int Version { get; set; }

        /// <summary>
        /// Der Basispfad des Plugins
        /// </summary>
        [XmlElement("contextpath")]
        public string ContextPath { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public PluginConfig()
        {
        }
    }
}