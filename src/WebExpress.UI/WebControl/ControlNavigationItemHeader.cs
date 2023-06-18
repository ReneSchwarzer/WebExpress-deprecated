using WebExpress.WebHtml;
using WebExpress.WebPage;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.UI.WebControl
{
    public class ControlNavigationItemHeader : Control, IControlNavigationItem
    {
        /// <summary>
        /// Liefert oder setzt die Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlNavigationItemHeader(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="text">The text.</param>
        public ControlNavigationItemHeader(string id, string text)
            : base(id)
        {
            Text = text;
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            return new HtmlElementTextContentLi(new HtmlText(I18N(context.Culture, Text)))
            {
                Id = Id,
                Class = Css.Concatenate("dropdown-header", GetClasses()),
                Style = GetStyles(),
                Role = Role
            };
        }
    }
}
