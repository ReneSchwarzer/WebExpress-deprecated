using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Markiert die Definition des oder der Begriffe, die in den direkt vorangehenden <dt>-Element angegeben wurden.
    /// </summary>
    public class HtmlElementTextContentDd : HtmlElement, IHtmlElementTextContent
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Constructor
        /// </summary>
        public HtmlElementTextContentDd()
            : base("dd")
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTextContentDd(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTextContentDd(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
