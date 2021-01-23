using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Kennzeichnet einenBefehl, welcher vom Benutzer aufgerufen werden kann.
    /// </summary>
    public class HtmlElementInteractiveCommand : HtmlElement, IHtmlElementInteractive
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementInteractiveCommand()
            : base("command")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementInteractiveCommand(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementInteractiveCommand(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
