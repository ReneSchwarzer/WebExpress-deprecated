using System.Text;

namespace WebExpress.Html
{
    public class HtmlRaw : IHtmlNode
    {
        /// <summary>
        /// Liefert oder setzt den Text
        /// </summary>
        public string Html { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlRaw()
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="html">Der Text</param>
        public HtmlRaw(string html)
        {
            Html = html;
        }

        /// <summary>
        /// In String konvertieren
        /// </summary>
        /// <returns>Das Objekt als String</returns>
        public override string ToString()
        {
            return Html;
        }

        /// <summary>
        /// In String konvertieren unter Zuhilfenahme eines StringBuilder
        /// </summary>
        /// <param name="builder">Der StringBuilder</param>
        /// <param name="deep">Die Aufrufstiefe</param>
        public virtual void ToString(StringBuilder builder, int deep)
        {
            builder.Append(Html);
        }
    }
}
