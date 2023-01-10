using System.Xml.Serialization;

namespace WebExpress.WebPackage
{
    /// <summary>
    /// Die Paketspezifikation ist eine XML-Datei mit dem Spezifikationen des Paketes
    /// </summary>
    [XmlRoot("package")]
    public class PackageItemSpec
    {
        /// <summary>
        /// Liefert oder setzt die ID
        /// </summary>
        [XmlElement("id")]
        public string Id { get; set; }

        /// <summary>
        /// Liefert oder setzt die Version
        /// </summary>
        [XmlElement("version")]
        public string Version { get; set; }

        /// <summary>
        /// Liefert oder setzt die Titel
        /// </summary>
        [XmlElement("title")]
        public string Title { get; set; }

        /// <summary>
        /// Liefert oder setzt die Autoren
        /// </summary>
        [XmlElement("authors", IsNullable = true)]
        public string Authors { get; set; }

        /// <summary>
        /// Liefert oder setzt die Lizenz
        /// </summary>
        [XmlElement("license", IsNullable = true)]
        public string License { get; set; }

        /// <summary>
        /// Liefert oder setzt die Url der Lizenz
        /// </summary>
        [XmlElement("licenseUrl", IsNullable = true)]
        public string LicenseUrl { get; set; }

        /// <summary>
        /// Liefert oder setzt das Icon des Paketes
        /// </summary>
        [XmlElement("icon", IsNullable = true)]
        public string Icon { get; set; }

        /// <summary>
        /// Liefert oder setzt die Readme.Datei des Paketes (md-Format)
        /// </summary>
        [XmlElement("readme", IsNullable = true)]
        public string Readme { get; set; }

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        [XmlElement("description", IsNullable = true)]
        public string Description { get; set; }

        /// <summary>
        /// Liefert oder setzt die Tags
        /// </summary>
        [XmlElement("tags", IsNullable = true)]
        public string Tags { get; set; }

        /// <summary>
        /// Liefert oder setzt das Reposetory
        /// </summary>
        //[XmlElement("repository", IsNullable = true)]
        //public Repository Repository { get; set; }

        /// <summary>
        /// Liefert oder setzt die Anhängikeiten
        /// </summary>
        //[XmlElement("dependencies", IsNullable = true)]
        //public Dependencies Dependencies { get; set; }
    }
}
