using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Box mit Rahmen
    /// </summary>
    public class ControlPanelCard : ControlPanel
    {
        /// <summary>
        /// Liefert oder setzt das Layout
        /// </summary>
        public TypesLayoutCard Layout { get; set; }

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
        /// Zeigt einen Rahmen an oder keinen
        /// </summary>
        public bool ShowBorder { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlPanelCard(IPage page, string id = null)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlPanelCard(IPage page, string id, params Control[] items)
            : base(page, id, items)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlPanelCard(IPage page, params Control[] items)
            : base(page, items)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            ShowBorder = true;
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            var classes = new List<string>
            {
                Class,
                "card",
                Layout.ToClass()
            };

            switch (HorizontalAlignment)
            {
                case TypesHorizontalAlignment.Left:
                    classes.Add("float-left");
                    break;
                case TypesHorizontalAlignment.Right:
                    classes.Add("float-right");
                    break;
            }

            if (!ShowBorder)
            {
                classes.Add("border-0");
            }

            var html = new HtmlElementDiv()
            {
                ID = ID,
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = Style,
                Role = Role
            };

            if (!string.IsNullOrWhiteSpace(Header))
            {
                html.Elements.Add(new HtmlElementDiv(new HtmlText(Header)) { Class = "card-header" });
            }

            if (!string.IsNullOrWhiteSpace(HeaderImage))
            {
                html.Elements.Add(new HtmlElementImg() { Src = HeaderImage, Class = "card-img-top" });
            }

            if (!string.IsNullOrWhiteSpace(Headline))
            {
                Content.Insert(0, new ControlText(Page) { Text = Headline, Class = "card-title", Format = TypesTextFormat.H4 });
            }

            html.Elements.Add(new HtmlElementDiv(Content.Select(x => x.ToHtml())) { Class = "card-body" });

            if (!string.IsNullOrWhiteSpace(FooterImage))
            {
                html.Elements.Add(new HtmlElementImg() { Src = FooterImage, Class = "card-img-top" });
            }

            if (!string.IsNullOrWhiteSpace(Footer))
            {
                html.Elements.Add(new HtmlElementDiv(new HtmlText(Footer)) { Class = "card-footer" });
            }

            return html;
        }
    }
}
