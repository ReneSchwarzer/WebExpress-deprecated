using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Steht für eine Randbemerkung. Der übrige Inhalt sollte auch verständlich sein, wenn dieses Element entfernt wird.
    /// </summary>
    public class HtmlElementSectionAside : HtmlElement, IHtmlElementSection
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementSectionAside()
            : base("aside")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementSectionAside(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementSectionAside(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
