using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Bezeichnet einen Textteil mit Ruby-Annotationen. Dies sind kurze Aussprachetipps und andere Hinweise, die hauptsächlich für ostasiatische Typografie verwendet werden.
    /// </summary>
    public class HtmlElementTextSemanticsRuby : HtmlElement, IHtmlElementTextSemantics
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementTextSemanticsRuby()
            : base("ruby")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTextSemanticsRuby(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTextSemanticsRuby(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
