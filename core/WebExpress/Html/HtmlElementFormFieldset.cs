using System.Collections.Generic;
using System.Text;

namespace WebExpress.Html
{
    /// <summary>
    /// Steht für eine Gruppe von Kontrollelementen.
    /// </summary>
    public class HtmlElementFormFieldset : HtmlElement, IHtmlFormularItem
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
        /// Liefert oder setzt die Label-Eigenschaft
        /// </summary>
        public bool Disable
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
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementFormFieldset()
            : base("fieldset")
        {
            CloseTag = false;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementFormFieldset(params IHtmlNode[] nodes)
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
