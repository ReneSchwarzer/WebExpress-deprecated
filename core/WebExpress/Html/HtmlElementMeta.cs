using System.Text;

namespace WebServer.Html
{
    public class HtmlElementMeta : HtmlElement
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
        /// Konstruktor
        /// </summary>
        public HtmlElementMeta()
            : base("meta")
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementMeta(string key)
            : this()
        {
            Key = key;
            SetAttribute(Key, "");
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementMeta(string key, string value)
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
