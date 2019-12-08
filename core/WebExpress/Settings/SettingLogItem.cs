using System.Xml.Linq;
using System.Xml.Serialization;

namespace WebExpress.Settings
{
    /// <summary>
    /// Klasse zur Verwaltung der Log-Einstellungen
    /// </summary>
    [XmlType("log")]
    public class SettingLogItem : ISettingItem
    {
        [XmlAttribute(AttributeName = "modus")]
        public string Modus { get; set; }

        [XmlAttribute(AttributeName = "path")]
        public string Path { get; set; }

        [XmlAttribute(AttributeName = "encoding")]
        public string Encoding { get; set; }

        [XmlAttribute(AttributeName = "filename")]
        public string Filename { get; set; }

        [XmlAttribute(AttributeName = "timepattern")]
        public string Timepattern { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public SettingLogItem()
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="xml">XML-Node</param> 
        public SettingLogItem(XElement xml)
        {
            Modus = xml.Attribute("modus") != null ? xml.Attribute("modus").Value : "";
            Path = xml.Attribute("path") != null ? xml.Attribute("path").Value : "";
            Encoding = xml.Attribute("encoding") != null ? xml.Attribute("encoding").Value : "";
            Filename = xml.Attribute("filename") != null ? xml.Attribute("filename").Value : "";
            Timepattern = xml.Attribute("timepattern") != null ? xml.Attribute("timepattern").Value : "";
        }
    }
}
