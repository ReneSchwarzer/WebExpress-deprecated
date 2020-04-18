using System.Collections.Generic;

namespace WebExpress.Html
{
    public class HtmlElementTBody : HtmlElement
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementTBody()
            : base("tbody")
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTBody(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTBody(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }
    }
}
