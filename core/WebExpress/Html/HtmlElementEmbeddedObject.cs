using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Steht für allgemeinen externen Inhalt, der je nach Kontext als Bild, 
    /// "verschachtelter Browsing-Kontext" (s. iframe), oder externer Inhalt 
    /// (der mit Hilfe eines Plugins darsgestellt wird) betrachtet wird.
    /// </summary>
    public class HtmlElementEmbeddedObject : HtmlElement, IHtmlElementEmbedded
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementEmbeddedObject()
            : base("object")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementEmbeddedObject(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementEmbeddedObject(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
