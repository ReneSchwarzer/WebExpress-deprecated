using System.Text.Json.Serialization;

namespace WebExpress.WebApp.WebResource
{
    /// <summary>
    /// Metainformationen einer CRUD-Tabellensplate 
    /// </summary>
    public class ResourceRestCrudColumn
    {
        /// <summary>
        /// Returns or sets the label. der Splalte
        /// </summary>
        [JsonPropertyName("label")]
        public string Label { get; set; }

        /// <summary>
        /// Returns or sets the icon. der Spalte oder null
        /// </summary>
        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Returns or sets the width. der Tabellenspalte in %, null für auto
        /// </summary>
        [JsonPropertyName("width")]
        public uint? Width { get; set; }

        /// <summary>
        /// Liefert oder setzt den Javascriptcode, welche die Daten der Zelle rendert
        /// </summary>
        [JsonPropertyName("render")]
        public string Render { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="label">Die Beschriftung der Splalte</param>
        public ResourceRestCrudColumn(string label)
        {
            Label = label;
        }
    }
}
