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
        /// Liefert oder setzt die Farbe
        /// </summary>
        public PropertyColorCallout Color
        {
            get => (PropertyColorCallout)GetPropertyObject();
            set => SetProperty(value, () => value?.ToClass(), () => value?.ToStyle());
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlPanelCallout(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content">Der Inhalt</param>
        public ControlPanelCallout(params Control[] content)
            : this()
        {
            Content.AddRange(content);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="content">Der Inhalt</param>
        public ControlPanelCallout(string id, params Control[] content)
            : this(id)
        {
            Content.AddRange(content);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var html = new HtmlElementTextContentDiv()
            {
                ID = Id,
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
