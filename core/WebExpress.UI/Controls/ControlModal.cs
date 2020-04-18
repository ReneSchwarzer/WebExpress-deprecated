using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlModal : Control
    {
        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        public List<Control> Content { get; private set; }

        /// <summary>
        /// Liefert oder setzt ob der Fadereffekt verwendet werden soll
        /// </summary>
        public bool Fade { get; set; }

        /// <summary>
        /// Liefert oder setzt den Header
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Liefert oder setzt den JQuerryCode, welcher beim anzeigen des Modal-Dialoges ausgefürt werden soll
        /// </summary>
        public string OnShownCode { get; set; }

        /// <summary>
        /// Liefert oder setzt den JQuerryCode, welcher beim ausblenden des Modal-Dialoges ausgefürt werden soll
        /// </summary>
        public string OnHiddenCode { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlModal(IPage page, string id)
            : base(page, !string.IsNullOrWhiteSpace(id) ? id : "modal")
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="header">Die Überschrift</param>
        public ControlModal(IPage page, string id, string header)
            : this(page, id)
        {
            Header = header;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="header">Die Überschrift</param>
        /// <param name="content">Der Inhalt</param>
        public ControlModal(IPage page, string id, string header, params Control[] content)
            : this(page, id, header)
        {
            Content.AddRange(content);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="text">Der Text</param>
        /// <param name="content">Der Inhalt</param>
        public ControlModal(IPage page, string id, string text, IEnumerable<Control> content)
            : this(page, id, text)
        {
            Content.AddRange(content);
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            //ID = !string.IsNullOrWhiteSpace(ID) ? ID : "modal";
            Content = new List<Control>();
            Fade = true;
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
                "modal"
            };

            if (Fade)
            {
                classes.Add("fade");
            }

            var headerText = new HtmlElementH4(Header)
            {
                Class = "modal-title"
            };

            var headerButtonLabel = new HtmlElementSpan(new HtmlText("&times;"))
            {
            };
            headerButtonLabel.AddUserAttribute("aria-hidden", "true");

            var headerButton = new HtmlElementButton(headerButtonLabel)
            {
                Class = "close"
            };
            headerButton.AddUserAttribute("aria-label", "close");
            headerButton.AddUserAttribute("data-dismiss", "modal");

            var header = new HtmlElementDiv(headerText, headerButton)
            {
                Class = "modal-header"
            };

            var body = new HtmlElementDiv(from x in Content select x.ToHtml())
            {
                Class = "modal-body"
            };

            var footerButton = new HtmlElementButton(new HtmlText("Schließen"))
            {
                Type = "button",
                Class = "btn btn-primary"
            };
            footerButton.AddUserAttribute("data-dismiss", "modal");

            var footer = new HtmlElementDiv(footerButton)
            {
                Class = "modal-footer"
            };

            var content = new HtmlElementDiv(header, body, footer)
            {
                Class = "modal-content"
            };

            var dialog = new HtmlElementDiv(content)
            {
                Class = "modal-dialog",
                Role = "document"
            };

            var html = new HtmlElementDiv(dialog)
            {
                ID = ID,
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = Style,
                Role = "dialog"
            };

            if (!string.IsNullOrWhiteSpace(OnShownCode))
            {
                var shown = "$('#" + ID + "').on('shown.bs.modal', function(e) { " + OnShownCode + " });";
                Page.AddScript(ID + "_shown", shown);
            }

            if (!string.IsNullOrWhiteSpace(OnHiddenCode))
            {
                var hidden = "$('#" + ID + "').on('hidden.bs.modal', function() { " + OnHiddenCode + " });";
                Page.AddScript(ID + "_hidden", hidden);
            }

            return html;
        }
    }
}
