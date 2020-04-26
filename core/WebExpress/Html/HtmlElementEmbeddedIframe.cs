using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Definiert einen Rahmen, mit dem ein HTML-Dokument in seinem eigenen Kontext in das aktuelle Dokument eingebettet werden kann.
    /// </summary>
    public class HtmlElementEmbeddedIframe : HtmlElement, IHtmlElementEmbedded
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementEmbeddedIframe()
            : base("iframe")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementEmbeddedIframe(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementEmbeddedIframe(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
