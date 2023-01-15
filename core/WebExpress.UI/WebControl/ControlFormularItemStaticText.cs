﻿using System.Collections.Generic;
using WebExpress.Html;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.UI.WebControl
{
    public class ControlFormularItemStaticText : ControlFormularItem, IControlFormularLabel
    {
        /// <summary>
        /// Liefert oder setzt Beschriftung
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularItemStaticText(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Initialisiert das Formularelement
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        public override void Initialize(RenderContextFormular context)
        {
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <param name="formular">Das Formular</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContextFormular context)
        {
            var c = new List<string>
            {
                "form-control-static"
            };

            var html = new HtmlElementTextContentP()
            {
                Text = I18N(context.Culture, Text),
                Class = Css.Concatenate(GetClasses()),
                Style = Style.Concatenate(GetStyles()),
                Role = Role
            };

            return html;
        }
    }
}
