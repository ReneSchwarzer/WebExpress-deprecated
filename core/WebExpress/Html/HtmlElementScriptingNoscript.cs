using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Definiert alternative Inhalte, die angezeigt werden sollen, wenn der Browser kein Skripting unterstützt.
    /// </summary>
    public class HtmlElementScriptingNoscript : HtmlElement, IHtmlElementScripting
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementScriptingNoscript()
            : base("span")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementScriptingNoscript(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementScriptingNoscript(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
