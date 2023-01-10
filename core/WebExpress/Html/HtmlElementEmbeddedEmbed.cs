using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Steht für einen Einbindungspunkt für externe Ressourcen. Dies sind typischerweise keine 
    /// HTML-Inhalte, sondern beispielsweise eine Applikation oder interaktiver Inhalt, 
    /// der mit Hilfe eines Plugins (anstatt nativ durch das Benutzerprogramms) dargestellt wird.
    /// </summary>
    public class HtmlElementEmbeddedEmbed : HtmlElement, IHtmlElementEmbedded
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Constructor
        /// </summary>
        public HtmlElementEmbeddedEmbed()
            : base("embed")
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementEmbeddedEmbed(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementEmbeddedEmbed(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
