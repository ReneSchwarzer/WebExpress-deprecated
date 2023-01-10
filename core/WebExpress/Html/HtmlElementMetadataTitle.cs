using System.Text;

namespace WebExpress.Html
{
    /// <summary>
    /// Definiert den Titel eines Dokuments, der in der Titelzeile des Browsers im Tab der betreffenden Seite angezeigt wird. Darf ausschließlich Text enthalten. Eventuell enthaltene Tags werden nicht interpretiert.
    /// </summary>
    public class HtmlElementMetadataTitle : HtmlElement, IHtmlElementMetadata
    {
        /// <summary>
        /// Der Titel
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public HtmlElementMetadataTitle()
            : base("title")
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title">Der Titel</param>
        public HtmlElementMetadataTitle(string title)
            : this()
        {
            Title = title;
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
            builder.Append(">");

            builder.Append(Title);

            builder.Append("</");
            builder.Append(ElementName);
            builder.Append(">");
        }
    }
}
