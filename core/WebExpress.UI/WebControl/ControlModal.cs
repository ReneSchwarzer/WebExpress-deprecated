﻿using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.WebPage;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.UI.WebControl
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
        /// Bestimmt, ob das Modal beim Laden des Steuerelementes angezeigt werden soll oder erst nach Nutzeraufforderung
        /// </summary>
        public bool ShowIfCreated { get; set; }

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
        /// <param name="id">Die ID</param>
        public ControlModal(string id)
            : base(!string.IsNullOrWhiteSpace(id) ? id : "modal")
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="header">Die Überschrift</param>
        public ControlModal(string id, string header)
            : this(id)
        {
            Header = header;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="header">Die Überschrift</param>
        /// <param name="content">Der Inhalt</param>
        public ControlModal(string id, string header, params Control[] content)
            : this(id, header)
        {
            if (content != null)
            {
                Content.AddRange(content);
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="text">Der Text</param>
        /// <param name="content">Der Inhalt</param>
        public ControlModal(string id, string text, IEnumerable<Control> content)
            : this(id, text)
        {
            Content.AddRange(content);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="text">Der Text</param>
        /// <param name="content">Der Inhalt</param>
        public ControlModal(string id = null, params Control[] content)
            : this(id, string.Empty)
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
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var classes = Classes.ToList();
            classes.Add("modal");

            if (Fade)
            {
                classes.Add("fade");
            }

            var headerText = new HtmlElementSectionH4(I18N(context.Culture, Header))
            {
                Class = "modal-title"
            };

            var headerButton = new HtmlElementFieldButton()
            {
                Class = "btn-close"
            };
            headerButton.AddUserAttribute("aria-label", "close");
            headerButton.AddUserAttribute("data-bs-dismiss", "modal");

            var header = new HtmlElementTextContentDiv(headerText, headerButton)
            {
                Class = "modal-header"
            };

            var body = new HtmlElementTextContentDiv(from x in Content select x.Render(context))
            {
                Class = "modal-body"
            };

            var footer = null as HtmlElementTextContentDiv;

            var footerButton = new HtmlElementFieldButton(new HtmlText(I18N(context.Culture, "webexpress.ui:modal.close.label")))
            {
                Type = "button",
                Class = Css.Concatenate("btn", new PropertyColorButton(TypeColorButton.Primary).ToStyle())
            };
            footerButton.AddUserAttribute("data-bs-dismiss", "modal");

            footer = new HtmlElementTextContentDiv(footerButton)
            {
                Class = "modal-footer"
            };

            var content = new HtmlElementTextContentDiv(header, body, footer)
            {
                Class = "modal-content"
            };

            var dialog = new HtmlElementTextContentDiv(content)
            {
                Class = "modal-dialog",
                Role = "document"
            };

            var html = new HtmlElementTextContentDiv(dialog)
            {
                ID = ID,
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = "dialog"
            };

            if (!string.IsNullOrWhiteSpace(OnShownCode))
            {
                var shown = "$('#" + ID + "').on('shown.bs.modal', function(e) { " + OnShownCode + " });";
                context.VisualTree.AddScript(ID + "_shown", shown);
            }

            if (!string.IsNullOrWhiteSpace(OnHiddenCode))
            {
                var hidden = "$('#" + ID + "').on('hidden.bs.modal', function() { " + OnHiddenCode + " });";
                context.VisualTree.AddScript(ID + "_hidden", hidden);
            }

            if (ShowIfCreated)
            {
                var show = "$('#" + ID + "').modal('show');";
                context.VisualTree.AddScript(ID + "_showifcreated", show);
            }

            return html;
        }
    }
}
