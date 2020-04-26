using System.Linq;
using System.Text;

namespace WebExpress.Html
{
    /// <summary>
    /// Kennzeichnet die Beschriftung für ein Formular-Kontrollelement (z.B. Texteingabefelder).
    /// <label for="vorname">Vorname:</label> 
    /// <input type="text" name="vorname" id="vorname" maxlength="30">
    /// </summary>
    public class HtmlElementFieldLabel : HtmlElement, IHtmlFormularItem
    {
        /// <summary>
        /// Liefert oder setzt den Namen des Eingabefeldes
        /// </summary>
        public string For
        {
            get => GetAttribute("for");
            set => SetAttribute("for", value);
        }

        /// <summary>
        /// Liefert oder setzt den Text
        /// </summary>
        public string Text
        {
            get => string.Join("", Elements.Where(x => x is HtmlText).Select(x => (x as HtmlText).Value));
            set { Elements.RemoveAll(x => x is HtmlText); Elements.Insert(0, new HtmlText(value)); }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementFieldLabel()
            : base("label")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="text">Der Inhalt</param>
        public HtmlElementFieldLabel(string text)
            : this()
        {
            Text = text;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementFieldLabel(params IHtmlNode[] nodes)
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
