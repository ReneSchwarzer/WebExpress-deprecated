using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Bezeichnet eine Abkürzung oder ein Akronym.
    /// </summary>
    public class HtmlElementTextSemanticsAbbr : HtmlElement, IHtmlElementTextSemantics
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementTextSemanticsAbbr()
            : base("abbr")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTextSemanticsAbbr(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTextSemanticsAbbr(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
