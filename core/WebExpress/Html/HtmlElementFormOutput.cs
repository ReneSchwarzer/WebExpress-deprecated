using System.Linq;
using System.Text;

namespace WebExpress.Html
{
    /// <summary>
    /// Markiert das Ergebnis einer Berechnung.
    /// </summary>
    public class HtmlElementFormOutput : HtmlElement, IHtmlFormularItem
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementFormOutput()
            : base("output")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementFormOutput(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// In String konvertieren unter Zuhilfenahme eines StringBuilder
        /// </summary>
        /// <param name="builder">Der StringBuilder</param>
        /// <param name="deep">Die Aufrufstiefe</param>
        public override void ToString(StringBuilder builder, int deep)
        {
            base.ToString(builder, deep);
        }
    }
}
