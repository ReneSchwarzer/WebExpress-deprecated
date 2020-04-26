using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Definiert den Kopfteil einer Seite oder eines Abschnitts. Er enthält oft ein Logo, den Titel der Website und die Seitennavigation.
    /// </summary>
    public class HtmlElementSectionHeader : HtmlElement, IHtmlElementSection
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementSectionHeader()
            : base("header")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementSectionHeader(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementSectionHeader(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
