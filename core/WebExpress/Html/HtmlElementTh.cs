using System.Collections.Generic;

namespace WebExpress.Html
{
    public class HtmlElementTh : HtmlElement
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementTh()
            : base("th")
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTh(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTh(List<IHtmlNode> nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }
    }
}
