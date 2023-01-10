using System.Collections.Generic;
using System.Xml.Serialization;
using WebExpress.Setting;

namespace WebExpress.Config
{
    /// <summary>
    /// Klasse zum Auslesen der Konfigurationsdatei
    /// </summary>
    [XmlRoot("config", IsNullable = false)]
    public sealed class HttpServerConfig
    {
        /// <summary>
        /// Die Konfiguarationsversion
        /// </summary>
        [XmlAttribute("version", DataType = "int")]
        public int Version { get; set; }

        /// <summary>
        /// Die Endpunkte des Webservers
        /// </summary>
        [XmlElement("endpoint")]
        public List<EndpointConfig> Endpoints { get; set; }

        /// <summary>
        /// Die Uir des Webservers
        /// </summary>
        [XmlElement("uri")]
        public string Uri { get; set; }

        /// <summary>
        /// Das Limitierungen
        /// </summary>
        [XmlElement("limit")]
        public LimitConfig Limit { get; set; }

        /// <summary>
        /// Verzeichnis, indem sich die Pakete befinden
        /// </summary>
        [XmlElement("packages")]
        public string PackageBase { get; set; }

        /// <summary>
        /// Root-Verzeichnis der Assets
        /// </summary>
        [XmlElement("assets")]
        public string AssetBase { get; set; }

        /// <summary>
        /// Root-Verzeichnis der Daten
        /// </summary>
        [XmlElement("data")]
        public string DataBase { get; set; }

        /// <summary>
        /// Der URI-Basispfad des WebServers
        /// </summary>
        [XmlElement("contextpath")]
        public string ContextPath { get; set; }

        /// <summary>
        /// Die Kultur
        /// </summary>
        [XmlElement("culture")]
        public string Culture { get; set; }

        /// <summary>
        /// Log-Einstellungen
        /// </summary>
        [XmlElement("log")]
        public SettingLogItem Log { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public HttpServerConfig()
        {
        }
    }
}