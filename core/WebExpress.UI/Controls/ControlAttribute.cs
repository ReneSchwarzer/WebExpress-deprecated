using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Anzeige eines Namen-Wert-Paares
    /// </summary>
    public class ControlAttribute : Control
    {
        /// <summary>
        /// Liefert oder setzt die Textfarbe
        /// </summary>
        public TypeColorText Color
        {
            get => (TypeColorText)GetProperty(TypeColorText.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Liefert oder setzt die Textfarbe des Namens
        /// </summary>
        public TypeColorText NameColor { get; set; }

        /// <summary>
        /// Liefert oder setzt die Textfarbe des Textes
        /// </summary>
        public TypeColorText TextColor { get; set; }

        /// <summary>
        /// Liefert oder setzt das Icon
        /// </summary>
        public Icon Icon { get; set; }

        /// <summary>
        /// Liefert oder setzt den Namen
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Liefert oder setzt den Wert
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlAttribute(IPage page, string id = null)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            var icon = new HtmlElementTextSemanticsSpan()
            {
                Class = Icon.ToClass()
            };

            var name = new HtmlElementTextSemanticsSpan(new HtmlText(Name))
            {
                Class = NameColor != TypeColorText.Default ? NameColor.ToClass() : string.Empty
            };

            var value = new HtmlElementTextSemanticsSpan(new HtmlText(Value))
            {
                Class = TextColor != TypeColorText.Default ? NameColor.ToClass() : string.Empty
            };

            var html = new HtmlElementTextContentDiv
            (
                Icon != Icon.None ? icon : null,
                name,
                value
            )
            {
                ID = ID,
                Class = GetClasses(),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };

            return html;
        }
    }
}
