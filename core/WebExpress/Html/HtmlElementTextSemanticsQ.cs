using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Bezeichnet ein Kurzzitat. Für längere Zitate sollte <blockquote> verwendet werden.
    /// </summary>
    public class HtmlElementTextSemanticsQ : HtmlElement, IHtmlElementTextSemantics
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementTextSemanticsQ()
            : base("q")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTextSemanticsQ(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTextSemanticsQ(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
