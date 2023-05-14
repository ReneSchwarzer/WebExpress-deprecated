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
        /// Liefert oder setzt den Headertext
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Liefert oder setzt den Headerbild
        /// </summary>
        public string HeaderImage { get; set; }

        /// <summary>
        /// Liefert oder setzt die Überschrift
        /// </summary>
        public string Headline { get; set; }

        /// <summary>
        /// Liefert oder setzt den Fußtext
        /// </summary>
        public string Footer { get; set; }

        /// <summary>
        /// Liefert oder setzt den Fußbild
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
        /// Fügt weitere Steuerelemente der Karte hinzu
        /// </summary>
        /// <param name="items">Die einzufügenden Steuerelemente</param>
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
                ID = Id,
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
