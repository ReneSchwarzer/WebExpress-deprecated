using System.Collections.Generic;
using System.Linq;

namespace WebExpress.Html
{
    /// <summary>
    /// Verbindet seinen Inhalt mit einem maschinenlesbaren Equivalent, angegeben im value-Attribut. (Dieses Element wird nur in der WHATWG-Version des HTML-Standards definiert, nicht aber in der W3C-Version von HTML5).
    /// </summary>
    public class HtmlElementTextSemanticsData : HtmlElement, IHtmlElementTextSemantics
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;
        
        /// <summary>
        /// Liefert oder setzt den Wert
        /// </summary>
        public string Value
        {
            get => string.Join("", Elements.Where(x => x is HtmlText).Select(x => (x as HtmlText).Value));
            set { Elements.Clear(); Elements.Add(new HtmlText(value)); }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementTextSemanticsData()
            : base("data")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTextSemanticsData(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTextSemanticsData(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
