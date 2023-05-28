using System.Xml.Serialization;

namespace WebExpress.WebPackage
{
    [XmlRoot("dependencies")]
    public class PackageItemSpecDependencies
    {
        /// <summary>
        /// Returns or sets the compatible version of webexpress.
        /// </summary>
        [XmlElement("webexpress", IsNullable = true)]
        public string WebExpressVersion { get; set; }

        /// <summary>
        /// Returns or sets the libs.
        /// </summary>
        [XmlElement("lib", IsNullable = true)]
        public string[] Libs { get; set; }

        /// <summary>
        /// Returns or sets the runtimes.
        /// </summary>
        [XmlElement("runtime", IsNullable = true)]
        public string[] Runtimes { get; set; }
    }
}
