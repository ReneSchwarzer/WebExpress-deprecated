using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;

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
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularItemStaticText(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Initialisiert das Formularelement
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public override void Initialize(RenderContextFormular context)
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <param name="formular">Das Formular</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContextFormular context)
        {
            var c = new List<string>
            {
                "form-control-static"
            };

            var html = new HtmlElementTextContentP()
            {
                Text = Text,
                Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };

            return html;
        }
    }
}
