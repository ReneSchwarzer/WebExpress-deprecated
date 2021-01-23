using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Internationalization;

namespace WebExpress.UI.WebControl
{
    public class ControlFormularItemInputCheckbox : ControlFormularItemInput
    {
        /// <summary>
        /// Liefert oder setzt ob die Checkbox in einer neuen Zeile angezeigt werden soll
        /// </summary>
        public bool Inline { get; set; }

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Liefert oder setzt ein Suchmuster, welches den Inhalt prüft
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularItemInputCheckbox(string id = null)
            : base(id)
        {
            Value = "false";
        }

        /// <summary>
        /// Initialisiert das Formularelement
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public override void Initialize(RenderContextFormular context)
        {
            if (context.Page.HasParam(Name))
            {
                Value = context.Page.GetParamValue(Name).Equals("on", StringComparison.OrdinalIgnoreCase) ? "true" : "false";
            }
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContextFormular context)
        {
            var html = new HtmlElementTextContentDiv
            (
                new HtmlElementFieldLabel
                (
                    new HtmlElementFieldInput()
                    {
                        Name = Name,
                        Pattern = Pattern,
                        Type = "checkbox",
                        Disabled = Disabled,
                        //Role = Role,
                        Checked = Value.Equals("true") 
                    },
                    new HtmlText(string.IsNullOrWhiteSpace(Description) ? string.Empty : "&nbsp;" + context.I18N(Description))
                )
                {
                }
            )
            {
                Class = Css.Concatenate("checkbox", GetClasses()),
                Style = GetStyles(),
            };

            return html;
        }

        /// <summary>
        /// Prüft das Eingabeelement auf Korrektheit der Daten
        /// </summary>
        public override void Validate()
        {
            base.Validate();
        }
    }
}
