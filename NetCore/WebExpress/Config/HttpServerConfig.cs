using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace WebExpress.Config
{
    /// <summary>
    /// Klasse zum Auslesen der Konfigurationsdatei
    /// </summary>
    [XmlRoot(ElementName = "config", IsNullable = false)]
    public class HttpServerConfig
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
        [XmlElement(ElementName = "stagedirectory")]
        public string StageDirectory { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="fileName">Die zu ladende XML-Datei</param>
        public HttpServerConfig(string fileName)
        {
        }

        /// <summary>
        /// Lädt die Konfigurationsdatei aus dem XML-Element.
        /// </summary>
        /// <param name="xml">Der Root-Knoten (<config/>) der Konfigurationsdatei</param>
        protected virtual void Load(XElement xml)
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

            if (xml.Element("stagedirectory") != null)
            {
                StageDirectory = xml.Element("stagedirectory").Value;
            }
        }
    }
}
