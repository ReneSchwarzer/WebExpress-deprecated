using System.Xml.Serialization;

namespace WebExpress.Config
{
    /// <summary>
    /// Klasse zum Auslesen der Zertifikatseigenschaften
    /// </summary>
    [XmlRoot("endpoint", IsNullable = false)]
    public sealed class EndpointConfig
    {
        /// <summary>
        /// Die Uri (z.B. https://localhost:443/)
        /// </summary>
        [XmlAttribute("uri")]
        public string Uri { get; set; }

        /// <summary>
        /// Das Zertifikat als PFX-Datei
        /// </summary>
        [XmlAttribute("pfx")]
        public string PfxFile { get; set; }

        /// <summary>
        /// Das Passwort
        /// </summary>
        [XmlAttribute("password")]
        public string Password { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public EndpointConfig()
        {
        }
    }
}