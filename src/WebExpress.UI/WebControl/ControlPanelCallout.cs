using System.Linq;
using WebExpress.Html;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlPanelCallout : ControlPanel
    {
        /// <summary>
        /// Liefert oder sezt den Titel
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Returns or sets the color.
        /// </summary>
        public PropertyColorCallout Color
        {
            get => (PropertyColorCallout)GetPropertyObject();
            set => SetProperty(value, () => value?.ToClass(), () => value?.ToStyle());
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
        public ControlPanelCallout(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content">The content of the html element.</param>
        public ControlPanelCallout(params Control[] content)
            : this()
        {
            Content.AddRange(content);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
        /// <param name="content">The content of the html element.</param>
        public ControlPanelCallout(string id, params Control[] content)
            : this(id)
        {
            Content.AddRange(content);
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var html = new HtmlElementTextContentDiv()
            {
                Id = Id,
                Class = Css.Concatenate("callout", GetClasses()),
                Style = GetStyles(),
                Role = Role
            };

            if (Title != null)
            {
                html.Elements.Add(new HtmlElementTextSemanticsSpan(new HtmlText(Title))
                {
                    Class = "callout-title"
                });
            }

            html.Elements.Add(new HtmlElementTextContentDiv(from x in Content select x.Render(context))
            {
                Class = "callout-body"
            });

            return html;
        }
    }
}
