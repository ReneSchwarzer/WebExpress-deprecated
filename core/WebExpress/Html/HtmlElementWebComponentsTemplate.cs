using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Kennzeichnet Html, welches nicht gerendert wird
    /// </summary>
    public class HtmlElementWebComponentsTemplate : HtmlElement, IHtmlElementWebComponents
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementWebComponentsTemplate()
            : base("template")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementWebComponentsTemplate(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementWebComponentsTemplate(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
