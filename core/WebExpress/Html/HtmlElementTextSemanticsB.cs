using System.Linq;
using System.Text;

namespace WebExpress.Html
{
    /// <summary>
    /// Steht für einen Textabschnitt, der vom übrigen Inhalt abgesetzt und üblicherweise fettgedruckt dargestellt wird, ohne für eine spezielle Betonung oder Wichtigkeit zu stehen. Dies kann beispielsweise ein Schlüsselwort oder ein Produktname in einer Produktbewertung sein.
    /// </summary>
    public class HtmlElementTextSemanticsB : HtmlElement, IHtmlElementTextSemantics
    {
        /// <summary>
        /// Liefert oder setzt den Text
        /// </summary>
        public string Text
        {
            get => string.Join("", Elements.Where(x => x is HtmlText).Select(x => (x as HtmlText).Value));
            set { Elements.Clear(); Elements.Add(new HtmlText(value)); }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementTextSemanticsB()
            : base("b")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="text">Der Inhalt</param>
        public HtmlElementTextSemanticsB(string text)
            : this()
        {
            Text = text;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTextSemanticsB(params IHtmlNode[] nodes)
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
