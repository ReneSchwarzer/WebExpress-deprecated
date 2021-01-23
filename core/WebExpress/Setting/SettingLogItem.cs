using System.Xml.Serialization;

namespace WebExpress.Setting
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
    }
}
