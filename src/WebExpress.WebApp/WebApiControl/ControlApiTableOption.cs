using System.Text.Json.Serialization;

namespace WebExpress.WebApp.WebApiControl
{
    /// <summary>
    /// Metainformationen einer CRUD-Option (z.B. Edit, Delete, ...)
    /// </summary>
    public class ControlApiTableOption
    {
        /// <summary>
        /// Returns or sets the label. des Optionseintrages. Null für Trenner
        /// </summary>
        [JsonPropertyName("label")]
        public string Label { get; set; }

        /// <summary>
        /// Returns or sets the icon. des Optionseintrages
        /// </summary>
        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Liefert oder setzt das Farbe des Optionseintrages
        /// </summary>
        [JsonPropertyName("color")]
        public string Color { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ControlApiTableOption()
        {
        }
    }
}
