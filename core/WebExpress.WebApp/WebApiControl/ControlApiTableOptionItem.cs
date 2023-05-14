using System.Text.Json.Serialization;

namespace WebExpress.WebApp.WebApiControl
{
    /// <summary>
    /// Metainformationen einer CRUD-Option (z.B. Edit, Delete, ...)
    /// </summary>
    public class ControlApiTableOptionItem
    {
        /// <summary>
        /// Die Arten eines Optionseintrages
        /// </summary>
        public enum OptionType { Item, Header, Divider };

        /// <summary>
        /// Returns or sets the type. des Optionseintrages
        /// </summary>
        [JsonIgnore]
        public OptionType Type { get; private set; }

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
        /// Liefert oder setzt Uri
        /// </summary>
        [JsonPropertyName("uri")]
        public string Uri { get; set; }

        /// <summary>
        /// Liefert oder setzt die Beschriftung des Optionseintrages. Null für Trenner
        /// </summary>
        [JsonPropertyName("css")]
        public string CSS => Type switch { OptionType.Divider => "dropdown-divider", OptionType.Header => "dropdown-header", _ => "dropdown-item" };

        /// <summary>
        /// Liefert oder setzt eine Funktion, welche ermittelt, on der Eintrag deaktivert werden soll.
        /// Der Wert beschreibt den Body einer Javascript-Funktion, welcher ein bool-Wert zurückgebt (z.B. return true;).
        /// Als Parameter steht das Item (Datenobjekt) zur Verfügung.
        /// </summary>
        [JsonPropertyName("disabled")]
        public string Disabled { get; set; }

        /// <summary>
        /// Liefert oder setzt eine Aktion, die aufgreufen werden soll, wenn auf den Link geklickt wird.
        /// Der Wert beschreibt den Body einer Javascript-Funktion, welcher beim Klichen auf den Link auferufen wird (z.B. alter("Hallo");). 
        /// Als Parameter steht das Item (Datenobjekt) und die Optionen (dieses Objekt) zur Verfügung.
        /// </summary>
        [JsonPropertyName("onclick")]
        public string OnClick { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ControlApiTableOptionItem()
        {
            Type = OptionType.Divider;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="label">Die Beschriftung der Splalte</param>
        /// <param name="type">Der Typ des Optionseintrages</param>
        public ControlApiTableOptionItem(string label, OptionType type = OptionType.Item)
        {
            Label = label;
            Type = type;
        }
    }
}
