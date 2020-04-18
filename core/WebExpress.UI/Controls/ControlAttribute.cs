using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlAttribute : Control
    {
        /// <summary>
        /// Liefert oder setzt das Format des Textes
        /// </summary>
        public TypesTextColor Color { get; set; }

        /// <summary>
        /// Liefert oder setzt das Icon
        /// </summary>
        public Icon Icon { get; set; }

        /// <summary>
        /// Liefert oder setzt den Namen
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Liefert oder setzt den Wert
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlAttribute(IPage page, string id = null)
            : base(page, id)
        {
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

            var icon = new HtmlElementSpan()
            {
                Class = Icon.ToClass()
            };

            var name = new HtmlElementSpan(new HtmlText(Name))
            {

            };

            var value = new HtmlElementSpan(new HtmlText(Value))
            {
                Class = "text-primary"
            };

            var html = new HtmlElementDiv(icon, name, value)
            {
                ID = ID,
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = Style,
                Role = Role
            };

            return html;
        }
    }
}
