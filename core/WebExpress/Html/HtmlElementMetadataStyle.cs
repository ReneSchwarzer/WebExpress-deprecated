using System.Text;

namespace WebExpress.Html
{
    /// <summary>
    /// Definition eines internen CSS-Stylesheets.
    /// </summary>
    public class HtmlElementMetadataStyle : HtmlElement, IHtmlElementMetadata
    {
        /// <summary>
        /// Liefert oder setzt den Text
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public HtmlElementMetadataStyle()
            : base("style")
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="code">Der Text</param>
        public HtmlElementMetadataStyle(string code)
            : this()
        {
            Code = code;
        }

        /// <summary>
        /// In String konvertieren unter Zuhilfenahme eines StringBuilder
        /// </summary>
        /// <param name="builder">Der StringBuilder</param>
        /// <param name="deep">Die Aufrufstiefe</param>
        public override void ToString(StringBuilder builder, int deep)
        {
            builder.Append(string.Empty.PadRight(deep));
            builder.Append("<");
            builder.Append(ElementName);
            builder.Append(">");

            if (!string.IsNullOrWhiteSpace(Code))
            {
                builder.Append("\n");
                builder.Append(Code);
                builder.Append("\n");
            }
            builder.Append(string.Empty.PadRight(deep));
            builder.Append("</");
            builder.Append(ElementName);
            builder.Append(">");
            builder.Append("\n");
        }
    }
}
