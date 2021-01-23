using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Markiert die Gruppe der Tabellenzeilen, die die Zusammenfassungen der Tabellenspalten enthalten.
    /// </summary>
    public class HtmlElementTableTfoot : HtmlElement, IHtmlElementTable
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementTableTfoot()
            : base("tfoot")
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTableTfoot(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTableTfoot(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }
    }
}
