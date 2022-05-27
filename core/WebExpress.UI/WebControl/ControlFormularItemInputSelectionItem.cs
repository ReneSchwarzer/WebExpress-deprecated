using System.Text.Json.Serialization;

namespace WebExpress.UI.WebControl
{
    public class ControlFormularItemInputSelectionItem
    {
        /// <summary>
        /// Liefert oder setzt die ID
        /// </summary>
        [JsonPropertyName("id")]
        public string ID { get; set; }

        /// <summary>
        /// Liefert oder setzt die Beschriftung
        /// </summary>
        [JsonPropertyName("label")]
        public string Label { get; set; }

        /// <summary>
        /// Liefert oder setzt die CSS-Klasse des Icons
        /// </summary>
        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Liefert oder setzt die Url des Bildes oder null
        /// </summary>
        [JsonPropertyName("image")]
        public string Image { get; set; }

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Liefert oder setzt die Farbe
        /// </summary>
        [JsonPropertyName("color")]
        public string Color { get; set; }

        /// <summary>
        /// Liefert oder setzt den Befehl (z.B. Hinzufügen)
        /// </summary>
        [JsonPropertyName("instruction")]
        public string Instruction { get; set; }
    }
}
