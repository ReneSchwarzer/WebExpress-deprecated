using System.Linq;
using System.Text;

namespace WebExpress.Html
{
    /// <summary>
    /// Wird zusammen mit dem Element <ruby> verwendet, um Ruby-Text mit Klammern zu umgeben, die angezeigt werden, wenn das Benutzerprogramm (Browser) keine Ruby-Annotationen unterstützt.
    /// </summary>
    public class HtmlElementTextSemanticsRp : HtmlElement, IHtmlElementTextSemantics
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
        /// Konstruktor
        /// </summary>
        public HtmlElementTextSemanticsRp()
            : base("rp")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="text">Der Inhalt</param>
        public HtmlElementTextSemanticsRp(string text)
            : this()
        {
            Text = text;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTextSemanticsRp(params IHtmlNode[] nodes)
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
