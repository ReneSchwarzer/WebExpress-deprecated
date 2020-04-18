using System.Linq;

namespace WebExpress.Html
{
    /// <summary>
    /// Das Element legend uist dem fieldset untergeoordnet
    /// </summary>
    public class HtmlElementLegend : HtmlElement
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
        public HtmlElementLegend()
            : base("legend")
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="text">Der Inhalt</param>
        public HtmlElementLegend(string text)
            : this()
        {
            Text = text;
        }
    }
}
