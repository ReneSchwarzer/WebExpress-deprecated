using System.Collections.Generic;
using System.Text;

namespace WebServer.Html
{
    /// <summary>
    /// Auswahlliste
    /// <select name="top5" size="2">
    ///  <option>Michael Jackson</option>
    ///  <option selected>Tom Waits</option>
    /// </select>
    /// </summary>
    public class HtmlElementSelect : HtmlElement, IHtmlFormularItem
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Liefert oder setzt den Namen des Eingabefeldes
        /// </summary>
        public string Name
        {
            get => GetAttribute("name");
            set => SetAttribute("name", value);
        }

        /// <summary>
        /// Liefert oder setzt die Anzahl der gleichzeitig sichtbaren Elemente der Auswahlliste
        /// </summary>
        public string Size
        {
            get => GetAttribute("size");
            set => SetAttribute("size", value);
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
        /// Liefert oder setzt ob das Eingabefeld verwendet werden kann
        /// </summary>
        public bool Disabled
        {
            get => HasAttribute("disabled");
            set { if (value) { SetAttribute("disabled"); } else { RemoveAttribute("disabled"); } }
        }

        /// <summary>
        /// Liefert oder setzt die OnChange-Attribut
        /// </summary>
        public string OnChange
        {
            get => GetAttribute("onchange");
            set => SetAttribute("onchange", value);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementSelect()
            : base("select")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementSelect(params IHtmlNode[] nodes)
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
