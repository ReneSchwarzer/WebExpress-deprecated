using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Das Element ul beschreibt eine Aufzählungsliste, also eine Liste, bei der die Reihenfolge der Elemente nur eine 
    /// untergeordnete oder keine Rolle spielt. ul steht dabei für unordered list, ungeordnete, unsortierte Liste. 
    /// </summary>
    public class HtmlElementTextContentUl : HtmlElement, IHtmlElementTextContent
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Constructor
        /// </summary>
        public HtmlElementTextContentUl()
            : base("ul")
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTextContentUl(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTextContentUl(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
