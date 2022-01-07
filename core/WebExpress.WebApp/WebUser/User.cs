using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace WebExpress.WebApp.WebUser
{
    public class User
    {
        /// <summary>
        /// Die ID
        /// </summary>
        [XmlAttribute("id")]
        public string ID { get; set; }

        /// <summary>
        /// Liefert oder setzt die Loginkennung
        /// </summary>
        [XmlAttribute("login")]
        public string Login { get; set; }

        /// <summary>
        /// Liefert oder setzt den Namen
        /// </summary>
        [XmlAttribute("firstname")]
        public string Firstname { get; set; }

        /// <summary>
        /// Liefert oder setzt den Namen
        /// </summary>
        [XmlAttribute("lastname")]
        public string Lastname { get; set; }

        /// <summary>
        /// Liefert oder setzt die E-Mail-Adresse
        /// </summary>
        [XmlAttribute("email")]
        public string Email { get; set; }

        /// <summary>
        /// Liefert oder setzt das Passwort
        /// </summary>
        [XmlAttribute("password")]
        public string Password { get; set; }

        /// <summary>
        /// Liefert oder setzt die Gruppen-IDs
        /// </summary>
        [XmlElement("groups")]
        public List<string> GroupIDs { get; set; } = new List<string>();

        /// <summary>
        /// Liefert die Gruppen
        /// </summary>
        [XmlIgnore]
        public IEnumerable<Group> Groups => from group1 in UserManager.Groups join group2 in GroupIDs on group1.ID equals group2 select group1;

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
