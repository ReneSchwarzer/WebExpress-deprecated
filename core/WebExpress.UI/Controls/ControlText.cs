using System.Collections.Generic;
using System.Linq;
using WebExpress.Pages;
using WebServer.Html;

namespace WebExpress.UI.Controls
{
    public class ControlText : Control
    {
        /// <summary>
        /// Liefert oder setzt das Format des Textes
        /// </summary>
        public TypesTextFormat Format { get; set; }

        /// <summary>
        /// Liefert oder setzt das Format des Textes
        /// </summary>
        public TypesTextColor Color { get; set; }

        /// <summary>
        /// Liefert oder setzt die Größe des Textes
        /// </summary>
        public TypesSize Size { get; set; }

        /// <summary>
        /// Liefert oder setzt das Format des Textes
        /// </summary>
        public TypesBackgroundColor BackgroundColor { get; set; }

        /// <summary>
        /// Liefert oder setzt die Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Liefert oder setzt einen Tooltiptext
        /// </summary>
        public string Tooltip { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlText(IPage page, string id = null)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="value">Der Text</param>
        public ControlText(IPage page, string id, int value)
            : base(page, id)
        {
            Text = value.ToString();

            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            var classes = new List<string>
            {
                Class
            };

            switch (Color)
            {
                case TypesTextColor.Muted:
                    classes.Add("text-muted");
                    break;
                case TypesTextColor.Primary:
                    classes.Add("text-primary");
                    break;
                case TypesTextColor.Success:
                    classes.Add("text-success");
                    break;
                case TypesTextColor.Info:
                    classes.Add("text-info");
                    break;
                case TypesTextColor.Warning:
                    classes.Add("text-warning");
                    break;
                case TypesTextColor.Danger:
                    classes.Add("text-danger");
                    break;
                case TypesTextColor.Light:
                    classes.Add("text-light");
                    break;
                case TypesTextColor.Dark:
                    classes.Add("text-dark");
                    break;
                case TypesTextColor.White:
                    classes.Add("text-white");
                    break;
            }

            switch (BackgroundColor)
            {
                case TypesBackgroundColor.Primary:
                    classes.Add("bg-primary");
                    break;
                case TypesBackgroundColor.Secondary:
                    classes.Add("bg-secondary");
                    break;
                case TypesBackgroundColor.Success:
                    classes.Add("bg-success");
                    break;
                case TypesBackgroundColor.Info:
                    classes.Add("bg-info");
                    break;
                case TypesBackgroundColor.Warning:
                    classes.Add("bg-warning");
                    break;
                case TypesBackgroundColor.Danger:
                    classes.Add("bg-danger");
                    break;
                case TypesBackgroundColor.Light:
                    classes.Add("bg-light");
                    break;
                case TypesBackgroundColor.Dark:
                    classes.Add("bg-dark");
                    break;
                case TypesBackgroundColor.White:
                    classes.Add("bg-white");
                    break;
                case TypesBackgroundColor.Transparent:
                    classes.Add("bg-transparent");
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

            switch (Size)
            {
                case TypesSize.Small:
                    classes.Add("small");
                    break;
                case TypesSize.Large:
                    classes.Add("large");
                    break;
            }

            var html = null as HtmlElement;

            switch (Format)
            {
                case TypesTextFormat.Paragraph:
                    html = new HtmlElementP(Text)
                    {
                        ID = ID,
                        Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                        Style = Style,
                        Role = Role
                    };
                    break;
                case TypesTextFormat.Italic:
                    html = new HtmlElementI(Text)
                    {
                        ID = ID,
                        Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                        Style = Style,
                        Role = Role
                    };
                    break;
                case TypesTextFormat.Bold:
                    html = new HtmlElementB(Text)
                    {
                        ID = ID,
                        Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                        Style = Style,
                        Role = Role
                    };
                    break;
                case TypesTextFormat.H1:
                    html = new HtmlElementH1(Text)
                    {
                        ID = ID,
                        Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                        Style = Style,
                        Role = Role
                    };
                    break;
                case TypesTextFormat.H4:
                    html = new HtmlElementH4(Text)
                    {
                        ID = ID,
                        Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                        Style = Style,
                        Role = Role
                    };
                    break;
                case TypesTextFormat.Span:
                    html = new HtmlElementSpan(new HtmlText(Text))
                    {
                        ID = ID,
                        Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                        Style = Style,
                        Role = Role
                    };
                    break;
                case TypesTextFormat.Small:
                    html = new HtmlElementSmall(new HtmlText(Text))
                    {
                        ID = ID,
                        Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                        Style = Style,
                        Role = Role
                    };
                    break;
                case TypesTextFormat.Center:
                    html = new HtmlElementCenter(new HtmlText(Text))
                    {
                        ID = ID,
                        Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                        Style = Style,
                        Role = Role
                    };
                    break;
                default:
                    html = new HtmlElementDiv(new HtmlText(Text))
                    {
                        ID = ID,
                        Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                        Style = Style,
                        Role = Role
                    };
                    break;
            }

            if (!string.IsNullOrWhiteSpace(Tooltip))
            {
                html.AddUserAttribute("data-toggle", "tooltip");
                html.AddUserAttribute("title", Tooltip);
            }

            return html;
        }
    }
}
