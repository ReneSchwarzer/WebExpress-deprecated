using System.Linq;
using System.Text;

namespace WebExpress.Html
{
    public class HtmlElementProgress : HtmlElement
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
        /// Liefert oder setzt den Wert
        /// </summary>
        public string Value
        {
            get => GetAttribute("value");
            set => SetAttribute("value", value);
        }

        /// <summary>
        /// Liefert oder setzt die untere Grenze der Skala
        /// </summary>
        public string Min
        {
            get => GetAttribute("min");
            set => SetAttribute("min", value);
        }

        /// <summary>
        /// Liefert oder setzt die obere Grenze der Skala
        /// </summary>
        public string Max
        {
            get => GetAttribute("max");
            set => SetAttribute("max", value);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementProgress()
            : base("progress")
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="text">Der Inhalt</param>
        public HtmlElementProgress(string text)
            : this()
        {
            Text = text;
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
