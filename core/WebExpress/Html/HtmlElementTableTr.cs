using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Steht für eine Zeile mit Tabellenzellen.
    /// </summary>
    public class HtmlElementTableTr : HtmlElement, IHtmlElementTable
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementTableTr()
            : base("tr")
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTableTr(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTableTr(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }
    }
}
