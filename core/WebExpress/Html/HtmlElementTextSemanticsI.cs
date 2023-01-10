using System.Linq;
using System.Text;

namespace WebExpress.Html
{
    /// <summary>
    /// Steht für einen Textabschnitt, der vom übrigen Inhalt abgesetzt und üblicherweise kursiv dargestellt wird, ohne für eine spezielle Betonung oder Wichtigkeit zu stehen. Dies kann beispielsweise eine taxonomische Bezeichnung, ein technischer Begriff, ein idiomatischer Ausdruck, ein Gedanke oder der Name eines Schiffes sein.
    /// </summary>
    public class HtmlElementTextSemanticsI : HtmlElement, IHtmlElementTextSemantics
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
        /// Constructor
        /// </summary>
        public HtmlElementTextSemanticsI()
            : base("i")
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">Der Inhalt</param>
        public HtmlElementTextSemanticsI(string text)
            : this()
        {
            Text = text;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTextSemanticsI(params IHtmlNode[] nodes)
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
