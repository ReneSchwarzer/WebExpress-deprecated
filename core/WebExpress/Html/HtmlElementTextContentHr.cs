using System.Text;

namespace WebExpress.Html
{
    /// <summary>
    /// Bezeichnet einen thematischen Bruch zwischen Absätzen eines Abschnitts, Artikels oder anderem längeren Inhalt.
    /// </summary>
    public class HtmlElementTextContentHr : HtmlElement, IHtmlElementTextContent
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementTextContentHr()
            : base("hr", false)
        {

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
