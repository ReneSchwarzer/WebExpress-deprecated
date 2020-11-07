using System;
using System.Xml.Linq;
using System.Xml.Serialization;
using WebExpress.Settings;

namespace WebExpress.Config
{
    /// <summary>
    /// Klasse zum Auslesen der Konfigurationsdatei
    /// </summary>
    [XmlRoot(ElementName = "config", IsNullable = false)]
    public sealed class HttpServerConfig
    {
        [XmlAttribute(AttributeName = "version", DataType = "int")]
        public int Version { get; set; }

        [XmlElement(ElementName = "port", DataType = "int")]
        public int Port { get; set; }

        [XmlElement(ElementName = "connectionlimit", DataType = "int")]
        public int ConnectionLimit { get; set; }

        /// <summary>
        /// Verzeichnis, indem sich die zu ladenden Plugins befinden
        /// </summary>
        [XmlElement(ElementName = "deployment")]
        public string Deployment { get; set; }

        /// <summary>
        /// Root-Verzeichnis der Assets
        /// </summary>
        [XmlElement(ElementName = "assets")]
        public string AssetBase { get; set; }

        /// <summary>
        /// Der Basispfad des Plugins
        /// </summary>
        [XmlElement(ElementName = "urlbasepath")]
        public string UrlBasePath { get; set; }

        /// <summary>
        /// Log-Einstellungen
        /// </summary>
        [XmlElement(ElementName = "log")]
        public SettingLogItem Log { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HttpServerConfig()
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="fileName">Die zu ladende XML-Datei</param>
        public HttpServerConfig(string fileName)
        {
            var doc = XDocument.Load(fileName);
            Load(doc.Root);
        }

        /// <summary>
        /// Lädt die Konfigurationsdatei aus dem XML-Element.
        /// </summary>
        /// <param name="xml">Der Root-Knoten (<config/>) der Konfigurationsdatei</param>
        public void Load(XElement xml)
        {
            Port = -1;

            if (xml.Attribute("version") != null)
            {
                Version = Convert.ToInt32(xml.Attribute("version").Value);
            }

            if (xml.Element("port") != null)
            {
                Port = Convert.ToInt32(xml.Element("port").Value);
            }

            if (xml.Element("connectionlimit") != null)
            {
                ConnectionLimit = Convert.ToInt32(xml.Element("connectionlimit").Value);
            }

            if (xml.Element("deployment") != null)
            {
                Deployment = xml.Element("deployment").Value;
            }

            if (xml.Element("assets") != null)
            {
                AssetBase = xml.Element("assets").Value;
            }

            if (xml.Element("root") != null)
            {
                //Root = xml.Element("root").Value;
            }

            if (xml.Element("urlbasepath") != null)
            {
                UrlBasePath = xml.Element("urlbasepath").Value;
            }

            Log = new SettingLogItem(xml.Element("log"));
        }
    }
}