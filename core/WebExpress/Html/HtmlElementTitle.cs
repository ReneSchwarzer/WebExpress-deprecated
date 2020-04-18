using System.Text;

namespace WebExpress.Html
{
    public class HtmlElementTitle : HtmlElement
    {
        /// <summary>
        /// Der Titel
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementTitle()
            : base("title")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="title">Der Titel</param>
        public HtmlElementTitle(string title)
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
