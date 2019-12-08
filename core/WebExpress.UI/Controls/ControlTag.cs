using System.Collections.Generic;
using System.Linq;
using WebExpress.Pages;
using WebServer.Html;

namespace WebExpress.UI.Controls
{
    public class ControlTag : Control
    {
        /// <summary>
        /// Liefert oder setzt das Layout
        /// </summary>
        public TypesLayoutBadge Layout { get; set; }

        /// <summary>
        /// Liefert oder setzt ob abgerundete Ecken verwendet werden soll
        /// </summary>
        public bool Pill { get; set; }

        /// <summary>
        /// Liefert oder setzt den Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Liefert oder setzt die Hintergrundfarbe
        /// </summary>
        public string BackgroundColor { get; set; }

        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        protected List<Control> Items { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlTag(IPage page, string id = null)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="content">Der Inhalt</param>
        public ControlTag(IPage page, string id, params Control[] content)
            : this(page, id)
        {
            Items.AddRange(content);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="content">Der Inhalt</param>
        public ControlTag(IPage page, string id, IEnumerable<Control> content)
            : this(page, id)
        {
            Items.AddRange(content);
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Pill = true;
            Items = new List<Control>();
        }

        /// <summary>
        /// Fügt ein neues Item hinzu
        /// </summary>
        /// <param name="item"></param>
        public void Add(Control item)
        {
            Items.Add(item);
        }

        /// <summary>
        /// Fügt ein neuen Seterator hinzu
        /// </summary>
        public void AddSeperator()
        {
            Items.Add(null);
        }

        /// <summary>
        /// Fügt ein neuen Kopf hinzu
        /// </summary>
        /// <param name="text">Der Überschriftstext</param>
        public void AddHeader(string text)
        {
            Items.Add(new ControlDropdownMenuHeader(Page) { Text = text });
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

            var styles = new List<string>
            {
                Style
            };

            switch (Layout)
            {
                case TypesLayoutBadge.Primary:
                    classes.Add("badge-primary");
                    break;
                case TypesLayoutBadge.Success:
                    classes.Add("badge-success");
                    break;
                case TypesLayoutBadge.Info:
                    classes.Add("badge-info");
                    break;
                case TypesLayoutBadge.Warning:
                    classes.Add("badge-warning");
                    break;
                case TypesLayoutBadge.Danger:
                    classes.Add("badge-danger");
                    break;
                case TypesLayoutBadge.Light:
                    classes.Add("badge-light");
                    break;
                case TypesLayoutBadge.Dark:
                    classes.Add("badge-dark");
                    break;
                case TypesLayoutBadge.Color:
                    classes.Add("badge-dark");
                    styles.Add("background-color: " + BackgroundColor + ";");
                    break;
            }

            if (Pill)
            {
                classes.Add("badge-pill");
            }

            classes.Add("badge");

            if (Items.Count == 0)
            {
                return new HtmlElementSpan(new HtmlText(Text))
                {
                    Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                    Style = string.Join(" ", styles.Where(x => !string.IsNullOrWhiteSpace(x)))
                };
            }

            classes.Add("btn");

            var html = new HtmlElementSpan()
            {
                ID = ID,
                Class = "dropdown"
            };

            var tag = new HtmlElementSpan
            (
                new HtmlText(Text), new HtmlElementSpan()
                {
                    Class = "fas fa-caret-down"
                }
            )
            {
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join(" ", styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                DataToggle = "dropdown"
            };

            html.Elements.Add(tag);
            html.Elements.Add
            (
                new HtmlElementUl
                (
                    Items.Select
                    (
                        x =>
                        x == null ?
                        new HtmlElementLi() { Class = "dropdown-divider", Inline = true } :
                        x is ControlDropdownMenuHeader ?
                        x.ToHtml() :
                        new HtmlElementLi(x.ToHtml().AddClass("dropdown-item")) { }
                    )
                )
                {
                    Class = HorizontalAlignment == TypesHorizontalAlignment.Right ? "dropdown-menu dropdown-menu-right" : "dropdown-menu"
                }
            );

            return html;
        }
    }
}
