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
        /// Das Uploadlimit in Bytes
        /// </summary>
        [XmlElement("uploadlimit", DataType = "long")]
        public long UploadLimit { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public LimitConfig()
        {
        }
    }
}