using System.Linq;
using WebExpress.Html;
using WebExpress.Uri;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlButtonLink : ControlButton
    {
        /// <summary>
        /// Liefert oder setzt die Ziel-Uri
        /// </summary>
        public IUri Uri { get; set; }

        /// <summary>
        /// Liefert oder setzt den Tooltip
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlButtonLink(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Size = TypeSizeButton.Default;
            Classes.Add("btn");
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Classes.Add(Size.ToClass());

            var html = new HtmlElementTextSemanticsA()
            {
                ID = ID,
                Class = GetClasses(),
                Style = GetStyles(),
                Role = Role,
                Href = Uri?.ToString(),
                Title = Title
            };

            if (Icon != null && Icon.HasIcon)
            {
                html.Elements.Add(new ControlIcon()
                {
                    Icon = Icon,
                    Margin = !string.IsNullOrWhiteSpace(Text) ? new PropertySpacingMargin
                    (
                        PropertySpacing.Space.None,
                        PropertySpacing.Space.Two,
                        PropertySpacing.Space.None,
                        PropertySpacing.Space.None
                    ) : new PropertySpacingMargin(PropertySpacing.Space.None),
                    VerticalAlignment = Icon.IsUserIcon ? TypeVerticalAlignment.TextBottom : TypeVerticalAlignment.Default
                }.Render(context));
            }

            if (!string.IsNullOrWhiteSpace(Text))
            {
                html.Elements.Add(new HtmlText(Text));
            }

            if (Content.Count > 0)
            {
                html.Elements.AddRange(Content.Select(x => x.Render(context)));
            }

            if (Modal != null)
            {
                html.AddUserAttribute("data-toggle", "modal");
                html.AddUserAttribute("data-target", "#" + Modal.ID);

                return new HtmlList(html, Modal.Render(context));
            }

            return html;
        }
    }
}
