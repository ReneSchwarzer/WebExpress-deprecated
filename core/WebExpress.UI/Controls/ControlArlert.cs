﻿using System;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Erstellt eine Box, welche die Aufmerksamkeit des Benutzers erlangen soll 
    /// </summary>
    public class ControlAlert : Control
    {
        /// <summary>
        /// Die Hintergrundfarbe
        /// </summary>
        public new PropertyColorBackgroundAlert BackgroundColor
        {
            get => (PropertyColorBackgroundAlert)GetPropertyObject();
            set => SetProperty(value, () => value?.ToClass(), () => value?.ToStyle());
        }

        /// <summary>
        /// Liefert oder setzt ob das Control geschlossen werden kann
        /// </summary>
        public TypesDismissibleAlert Dismissible
        {
            get => (TypesDismissibleAlert)GetProperty(TypesDismissibleAlert.Dismissible);
            set => SetProperty(value, () => value.ToClass());
        }
        
        /// <summary>
        /// Liefert oder setzt ob der Fadereffekt verwendet werden soll
        /// </summary>
        public TypesFade Fade 
        {
            get => (TypesFade)GetProperty(TypesFade.None);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Liefert oder setzt die Überschrift
        /// </summary>
        public string Head { get; set; }

        /// <summary>
        /// Liefert oder setzt die Text
        /// </summary>
        public string Text { get; set; }

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
            var head = new HtmlElementTextSemanticsStrong
            (
                new HtmlText(Head), 
                new HtmlNbsp()
            );
            
            var button = new HtmlElementFieldButton("&times;")
            {
                Class = "close"
            };
            button.AddUserAttribute("data-dismiss", "alert");
            button.AddUserAttribute("aria-label", "close");
            button.AddUserAttribute("aria-hidden", "true");

            return new HtmlElementTextContentDiv
            (
                !string.IsNullOrWhiteSpace(Head) ? head : null, 
                new HtmlText(Text), 
                Dismissible != TypesDismissibleAlert.None ? button : null
            )
            {
                ID = ID,
                Class = Css.Concatenate("alert", GetClasses()),
                Style = GetStyles(),
                Role = "alert"
            };
        }
    }
}
