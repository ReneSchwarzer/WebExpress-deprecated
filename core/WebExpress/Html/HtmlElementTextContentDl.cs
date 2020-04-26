using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Kennzeichnet eine Definitionsliste aus Begriffen und den dazugehörigen Definitionen.
    /// </summary>
    public class HtmlElementTextContentDl : HtmlElement, IHtmlElementTextContent
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementTextContentDl()
            : base("dl")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTextContentDl(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTextContentDl(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
