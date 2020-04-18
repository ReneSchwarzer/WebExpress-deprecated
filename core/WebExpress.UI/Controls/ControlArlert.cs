using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlAlert : Control
    {
        /// <summary>
        /// Liefert oder setzt das Layout des Textes
        /// </summary>
        public TypesLayoutAlert Layout { get; set; }

        /// <summary>
        /// Liefert oder setzt ob das Control geschlossen werden kann
        /// </summary>
        public bool Dismissible { get; set; }

        /// <summary>
        /// Liefert oder setzt die Überschrift
        /// </summary>
        public string Head { get; set; }

        /// <summary>
        /// Liefert oder setzt die Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Liefert oder setzt ob der Fadereffekt verwendet werden soll
        /// </summary>
        public bool Fade { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlAlert(IPage page, string id = null)
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
                Class,
                "alert"
            };

            if (Dismissible)
            {
                classes.Add("alert-dismissible");
            }

            if (Fade)
            {
                classes.Add("fade show");
            }

            switch (Layout)
            {
                case TypesLayoutAlert.Success:
                    classes.Add("alert-success");
                    break;
                case TypesLayoutAlert.Info:
                    classes.Add("alert-info");
                    break;
                case TypesLayoutAlert.Warning:
                    classes.Add("alert-warning");
                    break;
                case TypesLayoutAlert.Danger:
                    classes.Add("alert-danger");
                    break;
                case TypesLayoutAlert.Light:
                    classes.Add("alert-light");
                    break;
                case TypesLayoutAlert.Dark:
                    classes.Add("alert-dark");
                    break;
            }

            var head = new HtmlElementStrong(new HtmlText(Head), new HtmlNbsp());

            var button = new HtmlElementButton("&times;")
            {
                Class = "close"
            };
            button.AddUserAttribute("data-dismiss", "alert");
            button.AddUserAttribute("aria-label", "close");
            button.AddUserAttribute("aria-hidden", "true");

            return new HtmlElementDiv(!string.IsNullOrWhiteSpace(Head) ? head : null, new HtmlText(Text), Dismissible ? button : null)
            {
                ID = ID,
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = Style,
                Role = "alert"
            };
        }
    }
}
