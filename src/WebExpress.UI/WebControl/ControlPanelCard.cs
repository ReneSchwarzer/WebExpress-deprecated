using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Box mit Rahmen
    /// </summary>
    public class ControlPanelCard : ControlPanel
    {
        /// <summary>
        /// Returns or sets the header text.
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Returns or sets the header image.
        /// </summary>
        public string HeaderImage { get; set; }

        /// <summary>
        /// Returns or sets the headline.
        /// </summary>
        public string Headline { get; set; }

        /// <summary>
        /// Returns or sets the footer.
        /// </summary>
        public string Footer { get; set; }

        /// <summary>
        /// Returns or sets the footer image.
        /// </summary>
        public string FooterImage { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlPanelCard(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlPanelCard(string id, params Control[] items)
            : base(id, items)
        {
            Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlPanelCard(params Control[] items)
            : base(items)
        {
            Init();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private void Init()
        {
            Border = new PropertyBorder(true);
        }

        /// <summary>
        /// Adds controls to the panel.
        /// </summary>
        /// <param name="items">The controls to insert.</param>
        public void Add(params Control[] items)
        {
            Content.AddRange(items);
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
                Class = Css.Concatenate("card", GetClasses()),
                Style = GetStyles(),
                Role = Role
            };

            if (!string.IsNullOrWhiteSpace(Header))
            {
                html.Elements.Add(new HtmlElementTextContentDiv(new HtmlText(InternationalizationManager.I18N(context, Header))) { Class = "card-header" });
            }

            if (!string.IsNullOrWhiteSpace(HeaderImage))
            {
                html.Elements.Add(new HtmlElementMultimediaImg()
                {
                    Src = HeaderImage,
                    Class = "card-img-top"
                });
            }

            if (!string.IsNullOrWhiteSpace(Headline))
            {
                Content.Insert(0, new ControlText()
                {
                    Text = InternationalizationManager.I18N(context, Headline),
                    Classes = new List<string>(new[] { "card-title" }),
                    Format = TypeFormatText.H4
                });
            }

            html.Elements.Add(new HtmlElementTextContentDiv(new HtmlElementTextContentDiv(Content.Select(x => x.Render(context))) { Class = "card-text" }) { Class = "card-body" });

            if (!string.IsNullOrWhiteSpace(FooterImage))
            {
                html.Elements.Add(new HtmlElementMultimediaImg()
                {
                    Src = FooterImage,
                    Class = "card-img-top"
                });
            }

            if (!string.IsNullOrWhiteSpace(Footer))
            {
                html.Elements.Add(new HtmlElementTextContentDiv(new HtmlText(Footer)) { Class = "card-footer" });
            }

            return html;
        }
    }
}
