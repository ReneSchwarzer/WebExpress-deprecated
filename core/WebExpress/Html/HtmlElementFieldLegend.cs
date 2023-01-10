using System.Linq;

namespace WebExpress.Html
{
    /// <summary>
    /// Kennzeichnet eine Beschriftung für ein <fieldset>-Element.
    /// </summary>
    public class HtmlElementFieldLegend : HtmlElement
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
        public HtmlElementFieldLegend()
            : base("legend")
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">Der Inhalt</param>
        public HtmlElementFieldLegend(string text)
            : this()
        {
            Text = text;
        }
    }
}
