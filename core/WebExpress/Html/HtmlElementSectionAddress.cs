using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Definiert einen Abschnitt mit Kontaktinformationen.
    /// </summary>
    public class HtmlElementSectionAddress : HtmlElement, IHtmlElementSection
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementSectionAddress()
            : base("address")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementSectionAddress(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementSectionAddress(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
