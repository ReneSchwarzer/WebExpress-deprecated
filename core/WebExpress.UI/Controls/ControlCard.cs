using System.Collections.Generic;
using System.Linq;
using WebExpress.Pages;
using WebServer.Html;

namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Box mit Rahmen
    /// </summary>
    public class ControlCard : Control
    {
        /// <summary>
        /// Liefert oder setzt das Layout
        /// </summary>
        public TypesLayoutCard Layout { get; set; }

        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        public List<Control> Content { get; protected set; }

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
        public ControlCard(IPage page, string id = null)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlCard(IPage page, string id, params Control[] items)
            : this(page, id)
        {
            Content.AddRange(items);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlCard(IPage page, params Control[] items)
            : this(page)
        {
            Content.AddRange(items);
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            ShowBorder = true;
            Content = new List<Control>();
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
                "card"
            };

            switch (Layout)
            {
                case TypesLayoutCard.Primary:
                    classes.Add("bg-primary");
                    break;
                case TypesLayoutCard.Secondary:
                    classes.Add("bg-secondary");
                    break;
                case TypesLayoutCard.Success:
                    classes.Add("bg-success");
                    break;
                case TypesLayoutCard.Info:
                    classes.Add("bg-info");
                    break;
                case TypesLayoutCard.Warning:
                    classes.Add("bg-warning");
                    break;
                case TypesLayoutCard.Danger:
                    classes.Add("bg-danger");
                    break;
                case TypesLayoutCard.Light:
                    classes.Add("bg-light");
                    break;
                case TypesLayoutCard.Dark:
                    classes.Add("bg-dark");
                    break;
            }

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
