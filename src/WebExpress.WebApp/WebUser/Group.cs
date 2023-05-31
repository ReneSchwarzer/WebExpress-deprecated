using System;
using System.Xml.Serialization;

namespace WebExpress.WebApp.WebUser
{
    public class Group
    {
        /// <summary>
        /// Returns or sets the id.
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; }

        /// <summary>
        /// Returns or sets the name.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Returns or sets the timestamp of the creation.
        /// </summary>
        [XmlAttribute("created")]
        public DateTime Created { get; set; }

        /// <summary>
        /// The timestamp of the last change.
        /// </summary>
        [XmlAttribute("updated")]
        public DateTime Updated { get; set; }
    }
}
