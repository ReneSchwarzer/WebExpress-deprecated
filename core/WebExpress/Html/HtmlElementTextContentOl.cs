using System.Collections.Generic;
using System.Text;

namespace WebExpress.Html
{
    /// <summary>
    /// Das Element ul beschreibt eine Aufzählungsliste, also eine Liste, bei der die Reihenfolge der Elemente eine Rolle spielt. 
    /// ol steht dabei für ordered list, geordnete, sortierte Liste. 
    /// </summary>
    public class HtmlElementTextContentOl : HtmlElement, IHtmlElementTextContent
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<HtmlElementTextContentLi> Elements { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public HtmlElementTextContentOl()
            : base("ol")
        {
            Elements = new List<HtmlElementTextContentLi>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTextContentOl(params HtmlElementTextContentLi[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// In String konvertieren unter Zuhilfenahme eines StringBuilder
        /// </summary>
        /// <param name="builder">Der StringBuilder</param>
        /// <param name="deep">Die Aufrufstiefe</param>
        public override void ToString(StringBuilder builder, int deep)
        {
            base.Elements.Clear();
            base.Elements.AddRange(Elements);

            base.ToString(builder, deep);
        }
    }
}
