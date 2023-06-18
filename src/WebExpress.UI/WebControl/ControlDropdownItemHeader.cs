using WebExpress.WebHtml;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlDropdownItemHeader : Control, IControlDropdownItem
    {
        /// <summary>
        /// Liefert oder setzt die Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlDropdownItemHeader(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="text">The text.</param>
        public ControlDropdownItemHeader(string id, string text)
            : base(id)
        {
            Text = text;

            Init();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private void Init()
        {
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            return new HtmlElementTextContentLi(new HtmlText(Text))
            {
                Id = Id,
                Class = Css.Concatenate("dropdown-header", GetClasses()),
                Style = GetStyles(),
                Role = Role
            };
        }
    }
}
