using System;
using System.Xml.Serialization;

namespace WebExpress.WebApp.WebUser
{
    public class Group
    {
        /// <summary>
        /// Die Id
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; }

        /// <summary>
        /// Liefert oder setzt den Namen
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Der Zeitstempel der Erstellung
        /// </summary>
        [XmlAttribute("created")]
        public DateTime Created { get; set; }

        /// <summary>
        /// Der Zeitstempel der letzten Änderung
        /// </summary>
        [XmlAttribute("updated")]
        public DateTime Updated { get; set; }
    }
}
