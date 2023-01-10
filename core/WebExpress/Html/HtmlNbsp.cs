using System.Text;

namespace WebExpress.Html
{
    public class HtmlNbsp : IHtmlNode
    {
        /// <summary>
        /// Liefert oder setzt die Text
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public HtmlNbsp()
        {
            Value = "&nbsp;";
        }

        /// <summary>
        /// In String konvertieren unter Zuhilfenahme eines StringBuilder
        /// </summary>
        /// <param name="builder">Der StringBuilder</param>
        /// <param name="deep">Die Aufrufstiefe</param>
        public virtual void ToString(StringBuilder builder, int deep)
        {
            builder.Append(Value);
        }
    }
}
