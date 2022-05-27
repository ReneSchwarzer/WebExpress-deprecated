using System.Text.Json.Serialization;

namespace WebExpress.WebApp.WebApiControl
{
    /// <summary>
    /// Metainformationen einer CRUD-Option (z.B. Edit, Delete, ...)
    /// </summary>
    public class ControlApiTableOption
    {
        /// <summary>
        /// Liefert oder setzt die Beschriftung des Optionseintrages. Null für Trenner
        /// </summary>
        [JsonPropertyName("label")]
        public string Label { get; set; }

        /// <summary>
        /// Liefert oder setzt das Icon des Optionseintrages
        /// </summary>
        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Liefert oder setzt das Farbe des Optionseintrages
        /// </summary>
        [JsonPropertyName("color")]
        public string Color { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlApiTableOption()
        {
        }
    }
}
