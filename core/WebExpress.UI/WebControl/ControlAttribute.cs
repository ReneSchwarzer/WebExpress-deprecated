using System.Linq;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.Uri;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Anzeige eines Namen-Wert-Paares
    /// </summary>
    public class ControlAttribute : Control
    {
        /// <summary>
        /// Liefert oder setzt die Textfarbe des Namens
        /// </summary>
        public PropertyColorText NameColor { get; set; }

        /// <summary>
        /// Liefert oder setzt das Icon
        /// </summary>
        public PropertyIcon Icon { get; set; }

        /// <summary>
        /// Liefert oder setzt den Namen
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Liefert oder setzt den Wert
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Liefert oder setzt ein Link
        /// </summary>
        public IUri Uri { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlAttribute(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            if (!Enable)
            {
                return null;
            }

            var icon = new HtmlElementTextSemanticsSpan()
            {
                Class = Icon?.ToClass()
            };

            var name = new HtmlElementTextSemanticsSpan(new HtmlText(context.I18N(Name)))
            {
                ID = string.IsNullOrWhiteSpace(ID) ? string.Empty : $"{ID}_name",
                Class = NameColor?.ToClass()
            };

            var value = new HtmlElementTextSemanticsSpan(new HtmlText(context.I18N(Value)))
            {
                ID = string.IsNullOrWhiteSpace(ID) ? string.Empty : $"{ID}_value",
                Class = NameColor?.ToClass()
            };

            var html = new HtmlElementTextContentDiv
            (
                Icon != null && Icon.HasIcon ? icon : null,
                name,
                Uri != null ? new HtmlElementTextSemanticsA(value) { Href = Uri.ToString() } : value
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
