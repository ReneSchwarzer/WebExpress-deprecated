using System.Linq;
using System.Text;

namespace WebExpress.Html
{
    /// <summary>
    /// Beschreibt eine Überschrift der fünften Ebene
    /// </summary>
    public class HtmlElementSectionH5 : HtmlElement, IHtmlElementSection
    {
        /// <summary>
        /// Liefert oder setzt den Text
        /// </summary>
        public string Text
        {
            get => string.Join("", Elements.Where(x => x is HtmlText).Select(x => (x as HtmlText).Value));
            set { Elements.Clear(); Elements.Add(new HtmlText(value)); }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public HtmlElementSectionH5()
            : base("h5")
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">Der Inhalt</param>
        public HtmlElementSectionH5(string text)
            : this()
        {
            Text = text;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementSectionH5(params IHtmlNode[] nodes)
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
            base.ToString(builder, deep);
        }
    }
}
