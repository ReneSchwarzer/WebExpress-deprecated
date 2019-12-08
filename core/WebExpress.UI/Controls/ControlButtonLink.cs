using System.Collections.Generic;
using System.Linq;
using WebExpress.Pages;
using WebServer.Html;

namespace WebExpress.UI.Controls
{
    public class ControlButtonLink : ControlButton
    {
        /// <summary>
        /// Liefert oder setzt die Ziel-Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Liefert oder setzt das Format des Textes
        /// </summary>
        public TypesTextColor Color { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlButtonLink(IPage page, string id = null)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Disabled = false;
            Size = TypesSize.Default;
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
                "btn"
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

            if (Outline)
            {
                switch (Layout)
                {
                    case TypesLayoutButton.Primary:
                        classes.Add("btn-outline-primary");
                        break;
                    case TypesLayoutButton.Success:
                        classes.Add("btn-outline-success");
                        break;
                    case TypesLayoutButton.Info:
                        classes.Add("btn-outline-info");
                        break;
                    case TypesLayoutButton.Warning:
                        classes.Add("btn-outline-warning");
                        break;
                    case TypesLayoutButton.Danger:
                        classes.Add("btn-outline-danger");
                        break;
                    case TypesLayoutButton.Dark:
                        classes.Add("btn-outline-dark");
                        break;
                }
            }
            else
            {
                switch (Layout)
                {
                    case TypesLayoutButton.Primary:
                        classes.Add("btn-primary");
                        break;
                    case TypesLayoutButton.Success:
                        classes.Add("btn-success");
                        break;
                    case TypesLayoutButton.Info:
                        classes.Add("btn-info");
                        break;
                    case TypesLayoutButton.Warning:
                        classes.Add("btn-warning");
                        break;
                    case TypesLayoutButton.Danger:
                        classes.Add("btn-danger");
                        break;
                    case TypesLayoutButton.Light:
                        classes.Add("btn-light");
                        break;
                    case TypesLayoutButton.Dark:
                        classes.Add("btn-dark");
                        break;
                }
            }

            switch (Size)
            {
                case TypesSize.Large:
                    classes.Add("btn-lg");
                    break;
                case TypesSize.Small:
                    classes.Add("btn-sm");
                    break;
            }

            if (Block)
            {
                classes.Add("btn-block");
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

            var html = new HtmlElementA()
            {
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role,
                Href = Url
            };

            if (!string.IsNullOrWhiteSpace(Icon) && !string.IsNullOrWhiteSpace(Text))
            {
                html.Elements.Add(new HtmlElementSpan() { Class = Icon });

                html.Elements.Add(new HtmlNbsp());
                html.Elements.Add(new HtmlNbsp());
                html.Elements.Add(new HtmlNbsp());
            }
            else if (!string.IsNullOrWhiteSpace(Icon) && string.IsNullOrWhiteSpace(Text))
            {
                html.AddClass(Icon);
            }

            if (!string.IsNullOrWhiteSpace(Text))
            {
                html.Elements.Add(new HtmlText(Text));
            }

            if (Content.Count > 0)
            {
                html.Elements.AddRange(Content.Select(x => x.ToHtml()));
            }

            if (Modal != null)
            {
                html.AddUserAttribute("data-toggle", "modal");
                html.AddUserAttribute("data-target", "#" + Modal.ID);

                return new HtmlList(html, Modal.ToHtml());
            }

            return html;
        }
    }
}
