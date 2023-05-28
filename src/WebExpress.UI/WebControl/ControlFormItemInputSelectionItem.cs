using System.Text.Json.Serialization;

namespace WebExpress.UI.WebControl
{
    public class ControlFormItemInputSelectionItem
    {
        /// <summary>
        /// Returns or sets the id.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Returns or sets the label.
        /// </summary>
        [JsonPropertyName("label")]
        public string Label { get; set; }

        /// <summary>
        /// Returns or sets the css class of the icon.
        /// </summary>
        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Returns or sets the uri of the image.
        /// </summary>
        [JsonPropertyName("image")]
        public string Image { get; set; }

        /// <summary>
        /// Returns or sets the description.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Returns or sets the color.
        /// </summary>
        [JsonPropertyName("color")]
        public string Color { get; set; }

        /// <summary>
        /// Returns or sets the command (e.g. Add).
        /// </summary>
        [JsonPropertyName("instruction")]
        public string Instruction { get; set; }
    }
}
