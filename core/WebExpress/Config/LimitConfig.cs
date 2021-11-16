using System.Xml.Serialization;

namespace WebExpress.Config
{
    /// <summary>
    /// Klasse zum Auslesen der Limitierungen
    /// </summary>
    [XmlRoot("limit", IsNullable = false)]
    public sealed class LimitConfig
    {
        /// <summary>
        /// Das Verbindungslimit
        /// </summary>
        [XmlElement("connectionlimit", DataType = "int")]
        public int ConnectionLimit { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public LimitConfig()
        {
        }
    }
}