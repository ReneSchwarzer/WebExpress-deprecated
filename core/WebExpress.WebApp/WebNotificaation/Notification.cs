using System;

namespace WebExpress.WebApp.WebNotificaation
{
    public class Notification
    {
        /// <summary>
        /// Die ID der Benachrichtigung
        /// </summary>
        public string ID { get; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Die Überschrift
        /// </summary>
        public string Heading { get; internal set; }

        /// <summary>
        /// Die Benachrichtigungsnachricht
        /// </summary>
        public string Message { get; internal set; }

        /// <summary>
        /// Die Lebensdauer der Benachrichtigung
        /// </summary>
        public int Durability { get; internal set; }

        /// <summary>
        /// Liefert das Icon oder null
        /// </summary>
        public string Icon { get; internal set; }

        /// <summary>
        /// Liefert die Erstellungszeit
        /// </summary>
        public DateTime Created { get; } = DateTime.Now;

        /// <summary>
        /// Liefert oder setzt den Benachrichtigungstyp
        /// </summary>
        public string Type { get; internal set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Notification()
        {
        }
    }
}
