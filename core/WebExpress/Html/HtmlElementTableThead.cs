using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Markiert die Gruppe der Tabellenzeilen, die die Beschriftungen der Tabellenspalten enthalten.
    /// </summary>
    public class HtmlElementTableThead : HtmlElement,IHtmlElementTable
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementTableThead()
            : base("thead")
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTableThead(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTableThead(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }
    }
}
