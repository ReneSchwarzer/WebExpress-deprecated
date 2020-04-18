using System.Text;

namespace WebExpress.Html
{
    public class HtmlComment : IHtmlNode
    {
        /// <summary>
        /// Liefert oder setzt den Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlComment()
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="text">Der Text</param>
        public HtmlComment(string text)
        {
            Text = text;
        }

        /// <summary>
        /// In String konvertieren unter Zuhilfenahme eines StringBuilder
        /// </summary>
        /// <param name="builder">Der StringBuilder</param>
        /// <param name="deep">Die Aufrufstiefe</param>
        public virtual void ToString(StringBuilder builder, int deep)
        {
            builder.Append("<!-- ");
            builder.Append(Text);
            builder.Append(" -->");
        }
    }
}
