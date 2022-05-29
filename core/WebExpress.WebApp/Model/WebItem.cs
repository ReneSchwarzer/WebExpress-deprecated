using System;
using System.Text.Json.Serialization;

namespace WebExpress.WebApp.Model
{
    public class WebItem
    {
        /// <summary>
        /// Die Guid des Objektes
        /// </summary>
        [JsonPropertyName("id")]
        public string ID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Die Uri
        /// </summary>
        [JsonPropertyName("uri")]
        public virtual string Uri { get; set; }

        /// <summary>
        /// Die Bezeichnung des Objektes
        /// </summary>
        [JsonPropertyName("label")]
        public virtual string Label { get; set; }

        /// <summary>
        /// Die Name des Objektes
        /// </summary>
        [JsonPropertyName("name")]
        public virtual string Name { get; set; }

        /// <summary>
        /// Das Bild des Objektes
        /// </summary>
        [JsonPropertyName("image")]
        public virtual string Image { get; set; }
    }
}
