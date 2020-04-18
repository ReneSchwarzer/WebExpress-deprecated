using System.Collections.Generic;

namespace WebExpress.Html
{
    public class HtmlElementTHead : HtmlElement
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementTHead()
            : base("thead")
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTHead(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTHead(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }
    }
}
