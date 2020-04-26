using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Markiert ein Kontrollelement, mit dem der Benutzerzusätzliche Informationen oder Kontrolle erhalten kann.
    /// </summary>
    public class HtmlElementInteractiveDetails : HtmlElement, IHtmlElementInteractive
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementInteractiveDetails()
            : base("details")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementInteractiveDetails(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementInteractiveDetails(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
