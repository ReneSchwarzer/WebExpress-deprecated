using System;
using System.Text.Json.Serialization;

namespace WebExpress.WebApp.WebNotificaation
{
    public class Notification
    {
        /// <summary>
        /// Returns or sets the notification id.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Returns or sets the heading.
        /// </summary>
        [JsonPropertyName("heading")]
        public string Heading { get; set; }

        /// <summary>
        /// Returns or sets the notification message.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// Returns or sets the lifetime of the notification.
        /// </summary>
        [JsonPropertyName("durability")]
        public int Durability { get; set; }

        /// <summary>
        /// Returns the icon. Can be null.
        /// </summary>
        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Returns the creation time.
        /// </summary>
        [JsonPropertyName("created")]
        public DateTime Created { get; } = DateTime.Now;

        /// <summary>
        /// Progress as a percentage: 0-100%. <0 Without progress.
        /// </summary>
        [JsonPropertyName("progress")]
        public int Progress { get; set; } = -1;

        /// <summary>
        /// Returns or sets the notification type.
        /// </summary>
        [JsonPropertyName("type"), JsonConverter(typeof(TypeNotificationConverter))]
        public TypeNotification Type { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Notification()
        {
        }
    }
}
