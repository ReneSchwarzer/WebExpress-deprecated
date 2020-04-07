using System.Collections.Generic;

namespace WebServer.Html
{
    public class HtmlElementCanvas : HtmlElement
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementCanvas()
            : base("canvas")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementCanvas(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementCanvas(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
