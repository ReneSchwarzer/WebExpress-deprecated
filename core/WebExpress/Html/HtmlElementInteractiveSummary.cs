using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Kennzeichnet eineZusammenfassung oder eineLegende für ein bestimmte <details>-Element.
    /// </summary>
    public class HtmlElementInteractiveSummary : HtmlElement, IHtmlElementInteractive
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementInteractiveSummary()
            : base("summary")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementInteractiveSummary(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementInteractiveSummary(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
