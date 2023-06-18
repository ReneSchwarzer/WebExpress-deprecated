using System;
using System.Linq;
using WebExpress.WebHtml;
using WebExpress.Internationalization;
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
        /// Returns or sets the icon.
        /// </summary>
        public PropertyIcon Icon { get; set; }

        /// <summary>
        /// Returns or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Returns or sets the value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Liefert oder setzt ein Link
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlAttribute(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
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
                Id = string.IsNullOrWhiteSpace(Id) ? string.Empty : $"{Id}_name",
                Class = NameColor?.ToClass()
            };

            var value = new HtmlElementTextSemanticsSpan(new HtmlText(context.I18N(Value)))
            {
                Id = string.IsNullOrWhiteSpace(Id) ? string.Empty : $"{Id}_value",
                Class = NameColor?.ToClass()
            };

            var html = new HtmlElementTextContentDiv
            (
                Icon != null && Icon.HasIcon ? icon : null,
                name,
                Uri != null ? new HtmlElementTextSemanticsA(value) { Href = Uri.ToString() } : value
            )
            {
                Id = Id,
                Class = GetClasses(),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };

            return html;
        }
    }
}
