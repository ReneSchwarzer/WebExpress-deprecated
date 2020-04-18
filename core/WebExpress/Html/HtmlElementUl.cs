using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Das Element ul beschreibt eine Aufzählungsliste, also eine Liste, bei der die Reihenfolge der Elemente nur eine 
    /// untergeordnete oder keine Rolle spielt. ul steht dabei für unordered list, ungeordnete, unsortierte Liste. 
    /// </summary>
    public class HtmlElementUl : HtmlElement
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementUl()
            : base("ul")
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementUl(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementUl(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
