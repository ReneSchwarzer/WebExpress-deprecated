using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Kennzeichnet einen Platzhalter
    /// </summary>
    public class HtmlElementWebFragmentsSlot : HtmlElement, IHtmlElementWebFragments
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Constructor
        /// </summary>
        public HtmlElementWebFragmentsSlot()
            : base("slot")
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementWebFragmentsSlot(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementWebFragmentsSlot(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
