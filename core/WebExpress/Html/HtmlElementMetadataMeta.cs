using System.Text;

namespace WebExpress.Html
{
    /// <summary>
    /// Wird für die Definition von Metadaten verwenden, die mit keinem anderen HTML-Element definiert werden können.
    /// </summary>
    public class HtmlElementMetadataMeta : HtmlElement, IHtmlElementMetadata
    {
        /// <summary>
        /// Liefert oder setzt den Attributnamen
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Liefert oder setzt den Wert
        /// </summary>
        public string Value
        {
            get => GetAttribute(Key);
            set => SetAttribute(Key, value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public HtmlElementMetadataMeta()
            : base("meta")
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public HtmlElementMetadataMeta(string key)
            : this()
        {
            Key = key;
            SetAttribute(Key, "");
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public HtmlElementMetadataMeta(string key, string value)
            : this()
        {
            Key = key;
            SetAttribute(Key, value);
        }

        /// <summary>
        /// In String konvertieren unter Zuhilfenahme eines StringBuilder
        /// </summary>
        /// <param name="builder">Der StringBuilder</param>
        /// <param name="deep">Die Aufrufstiefe</param>
        public override void ToString(StringBuilder builder, int deep)
        {
            builder.AppendLine();
            builder.Append(string.Empty.PadRight(deep));
            builder.Append("<");
            builder.Append(ElementName);
            builder.Append(" ");
            builder.Append(Key);
            builder.Append("='");
            builder.Append(Value);
            builder.Append("'>");
        }
    }
}
