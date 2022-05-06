using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebExpress.Html
{
    /// <summary>
    /// Markiert einen Button.
    /// </summary>
    public class HtmlElementFieldButton : HtmlElement, IHtmlFormularItem
    {
        /// <summary>
        /// Liefert oder setzt den Namen des Eingabefeldes
        /// </summary>
        public string Name
        {
            get => GetAttribute("name");
            set => SetAttribute("name", value);
        }

        /// <summary>
        /// Liefert oder setzt den Text
        /// </summary>
        public string Text
        {
            get => string.Join("", Elements.Where(x => x is HtmlText).Select(x => (x as HtmlText).Value));
            set { Elements.Clear(); Elements.Add(new HtmlText(value)); }
        }

        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Liefert oder setzt einen Wert
        /// </summary>
        public string Value
        {
            get => GetAttribute("value");
            set => SetAttribute("value", value);
        }

        /// <summary>
        /// Liefert oder setzt ob die Schaltfläche deaktiviert ist
        /// </summary>
        public bool Disabled
        {
            get => HasAttribute("disabled");
            set { if (value) { SetAttribute("disabled"); } else { RemoveAttribute("disabled"); } }
        }

        /// <summary>
        /// Liefert oder setzt den Identifizierungsname des form-Elementes mit dem es in Verbindung steht
        /// </summary>
        public string Form
        {
            get => GetAttribute("form");
            set => SetAttribute("form", value);
        }

        /// <summary>
        /// Liefert oder setzt den Typ der Schaltfläche (button, submit, reset)
        /// </summary>
        public string Type
        {
            get => GetAttribute("type");
            set => SetAttribute("type", value);
        }

        /// <summary>
        /// Liefert oder setzt den Tooltip
        /// </summary>
        public string Title
        {
            get => GetAttribute("title");
            set => SetAttribute("title", value);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementFieldButton()
            : base("button")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="text">Der Inhalt</param>
        public HtmlElementFieldButton(string text)
            : this()
        {
            Text = text;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementFieldButton(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementFieldButton(IEnumerable<IHtmlNode> nodes)
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
            //if (Type == "submit" || Type == "reset")
            //{
            //    ElementName = "input";
            //}

            base.ToString(builder, deep);
        }
    }
}
