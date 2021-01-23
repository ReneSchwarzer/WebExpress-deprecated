using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Definiert den Hauptinhalt der Seite. Es ist nur ein <main> Element pro Seite zulässig.
    /// </summary>
    public class HtmlElementSectionMain : HtmlElement, IHtmlElementSection
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementSectionMain()
            : base("main")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementSectionMain(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementSectionMain(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
