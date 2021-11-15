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
        /// Das Verbindungslimit
        /// </summary>
        [XmlElement("connectionlimit", DataType = "int")]
        public int ConnectionLimit { get; set; }

        /// <summary>
        /// Verzeichnis, indem sich die zu ladenden Plugins befinden
        /// </summary>
        [XmlElement("deployment")]
        public string Deployment { get; set; }

        /// <summary>
        /// Root-Verzeichnis der Assets
        /// </summary>
        [XmlElement("assets")]
        public string AssetBase { get; set; }

        /// <summary>
        /// Der Basispfad des WebServers
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
        /// Konstruktor
        /// </summary>
        public HttpServerConfig()
        {
        }
    }
}