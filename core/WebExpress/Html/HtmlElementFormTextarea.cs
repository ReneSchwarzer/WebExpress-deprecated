using System.Linq;
using System.Text;

namespace WebExpress.Html
{
    /// <summary>
    /// Markiert ein Element für mehrzeilige Texteingaben.
    /// </summary>
    public class HtmlElementFormTextarea : HtmlElement, IHtmlFormularItem
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
        /// Liefert oder setzt den Wert des Eingabefeldes
        /// </summary>
        public string Value
        {
            get => string.Join(string.Empty, Elements.Where(x => x is HtmlText).Select(x => (x as HtmlText).Value));
            set { Elements.Clear(); Elements.Add(new HtmlText(value)); }
        }

        /// <summary>
        /// Liefert oder setzt die Anzahl der anzegeigten Zeilen
        /// </summary>
        public string Rows
        {
            get => GetAttribute("rows");
            set => SetAttribute("rows", value);
        }

        /// <summary>
        /// Liefert oder setzt die Anzahl der anzegeigten Spalten
        /// </summary>
        public string Cols
        {
            get => GetAttribute("cols");
            set => SetAttribute("cols", value);
        }

        /// <summary>
        /// Liefert oder setzt ob der Text umgebrochen werden soll
        ///  Mögliche Werte sind: hard, soft
        /// </summary>
        public string Wrap
        {
            get => GetAttribute("wrap");
            set => SetAttribute("wrap", value);
        }

        /// <summary>
        /// Liefert oder setzt ob das Felf schreibgeschützt ist
        /// </summary>
        public string Readonly
        {
            get => GetAttribute("readonly");
            set => SetAttribute("readonly", value);
        }

        /// <summary>
        /// Liefert oder setzt die minimale Länge
        /// </summary>
        public string MinLength
        {
            get => GetAttribute("minlength");
            set => SetAttribute("minlength", value);
        }

        /// <summary>
        /// Liefert oder setzt die maximale Länge
        /// </summary>
        public string MaxLength
        {
            get => GetAttribute("maxlength");
            set => SetAttribute("maxlength", value);
        }

        /// <summary>
        /// Liefert oder setzt ob Eingaben erzwungen werden
        /// </summary>
        public bool Required
        {
            get => HasAttribute("required");
            set { if (value) { SetAttribute("required"); } else { RemoveAttribute("required"); } }
        }

        /// <summary>
        /// Liefert oder setzt ein Platzhaltertext
        /// </summary>
        public string Placeholder
        {
            get => GetAttribute("placeholder");
            set => SetAttribute("placeholder", value);
        }

        /// <summary>
        /// Liefert oder setzt ein Suchmuster, welches den Inhalt prüft
        /// </summary>
        public string Pattern
        {
            get => GetAttribute("pattern");
            set => SetAttribute("pattern", value);
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
        /// Konstruktor
        /// </summary>
        public HtmlElementFormTextarea()
            : base("textarea")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementFormTextarea(params IHtmlNode[] nodes)
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
