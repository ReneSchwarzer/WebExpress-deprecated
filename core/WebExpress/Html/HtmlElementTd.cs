using System.Collections.Generic;

namespace WebExpress.Html
{
    public class HtmlElementTd : HtmlElement
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementTd()
            : base("td")
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="node">Der Inhalt</param>
        public HtmlElementTd(IHtmlNode node)
            : this()
        {
            Elements.Add(node);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTd(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTd(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }
    }
}
