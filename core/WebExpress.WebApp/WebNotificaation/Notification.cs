using System;
using System.Text.Json.Serialization;

namespace WebExpress.WebApp.WebNotificaation
{
    public class Notification
    {
        /// <summary>
        /// Die ID der Benachrichtigung
        /// </summary>
        [JsonPropertyName("id")]
        public string ID { get; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Die Überschrift
        /// </summary>
        [JsonPropertyName("heading")]
        public string Heading { get; set; }

        /// <summary>
        /// Die Benachrichtigungsnachricht
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// Die Lebensdauer der Benachrichtigung
        /// </summary>
        [JsonPropertyName("durability")]
        public int Durability { get; set; }

        /// <summary>
        /// Liefert das Icon oder null
        /// </summary>
        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Liefert die Erstellungszeit
        /// </summary>
        [JsonPropertyName("created")]
        public DateTime Created { get; } = DateTime.Now;

        /// <summary>
        /// Der Fortschritt in Prozent 0-100%. <0 Ohne Fortschritt
        /// </summary>
        [JsonPropertyName("progress")]
        public int Progress { get; set; } = -1;

        /// <summary>
        /// Liefert oder setzt den Benachrichtigungstyp
        /// </summary>
        [JsonPropertyName("type"), JsonConverter(typeof(TypeNotificationConverter))]
        public TypeNotification Type { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Notification()
        {
        }
    }
}
