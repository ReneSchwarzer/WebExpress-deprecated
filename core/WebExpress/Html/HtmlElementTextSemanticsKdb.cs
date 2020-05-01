using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Markiert eine Benutzereingabe, oftmals, aber nicht unbedingt, auf der Tastatur. Kann auch für andere Eingaben, beispielsweise transkribierte Sprachbefehle stehen.
    /// </summary>
    public class HtmlElementTextSemanticsKdb : HtmlElement, IHtmlElementTextSemantics
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementTextSemanticsKdb()
            : base("kdb")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTextSemanticsKdb(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTextSemanticsKdb(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
