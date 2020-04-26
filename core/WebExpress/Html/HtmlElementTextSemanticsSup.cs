using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Markiert hochgestellten Text.
    /// </summary>
    public class HtmlElementTextSemanticsSup : HtmlElement, IHtmlElementTextSemantics
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementTextSemanticsSup()
            : base("sup")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTextSemanticsSup(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTextSemanticsSup(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
