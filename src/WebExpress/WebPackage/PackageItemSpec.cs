using System.Xml.Serialization;

namespace WebExpress.WebPackage
{
    /// <summary>
    /// The package specification is an XML file containing the specification of the package.
    /// </summary>
    [XmlRoot("package")]
    public class PackageItemSpec
    {
        /// <summary>
        /// Returns or sets the package id.
        /// </summary>
        [XmlElement("id")]
        public string Id { get; set; }

        /// <summary>
        /// Returns or sets the package version.
        /// </summary>
        [XmlElement("version")]
        public string Version { get; set; }

        /// <summary>
        /// Returns or sets the titles.
        /// </summary>
        [XmlElement("title")]
        public string Title { get; set; }

        /// <summary>
        /// Returns or sets the authors.
        /// </summary>
        [XmlElement("authors", IsNullable = true)]
        public string Authors { get; set; }

        /// <summary>
        /// Returns or sets the license.
        /// </summary>
        [XmlElement("license", IsNullable = true)]
        public string License { get; set; }

        /// <summary>
        /// Returns or sets the url of the license.
        /// </summary>
        [XmlElement("licenseUrl", IsNullable = true)]
        public string LicenseUrl { get; set; }

        /// <summary>
        /// Returns or sets the icon of the package.
        /// </summary>
        [XmlElement("icon", IsNullable = true)]
        public string Icon { get; set; }

        /// <summary>
        /// Returns or sets the package's readme file (md format).
        /// </summary>
        [XmlElement("readme", IsNullable = true)]
        public string Readme { get; set; }

        /// <summary>
        /// Returns or sets the description.
        /// </summary>
        [XmlElement("description", IsNullable = true)]
        public string Description { get; set; }

        /// <summary>
        /// Returns or sets the tags.
        /// </summary>
        [XmlElement("tags", IsNullable = true)]
        public string Tags { get; set; }

        /// <summary>
        /// Returns or sets the artifacts.
        /// </summary>
        [XmlElement("artifacts", IsNullable = true)]
        public string[] Artifacts { get; set; }

        /// <summary>
        /// Returns or sets the dependencies.
        /// </summary>
        [XmlElement("dependencies", IsNullable = true)]
        public PackageItemSpecDependencies Dependencies { get; set; }
    }
}
