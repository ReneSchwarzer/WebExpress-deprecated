using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Steht für eine Variable. Dies kann ein tatsächlicher mathematischer Ausdruck oder Programmierungskontext sein, ein Identifier für eine Konstante, ein Symbol für eine physikalische Größe, ein Funktionsparameter oder einfach ein Platzhalter.
    /// </summary>
    public class HtmlElementTextSemanticsVar : HtmlElement, IHtmlElementTextSemantics
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementTextSemanticsVar()
            : base("var")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTextSemanticsVar(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTextSemanticsVar(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
