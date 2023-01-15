using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Kennzeichnet Html, welches nicht gerendert wird
    /// </summary>
    public class HtmlElementWebFragmentsTemplate : HtmlElement, IHtmlElementWebFragments
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Constructor
        /// </summary>
        public HtmlElementWebFragmentsTemplate()
            : base("template")
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementWebFragmentsTemplate(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementWebFragmentsTemplate(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
