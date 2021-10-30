using WebExpress.Html;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.UI.WebControl
{
    public class ControlFormularItemLabel : ControlFormularItem
    {
        /// <summary>
        /// Liefert oder setzt den Text des Labels
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Liefert oder setzt das zugehörige Formularfeld
        /// </summary>
        public ControlFormularItem FormularItem { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularItemLabel(string id)
            : base(id)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="text">Der Text</param>
        public ControlFormularItemLabel(string id, string text)
            : this(id)
        {
            Text = text;
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
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContextFormular context)
        {
            return new HtmlElementFieldLabel()
            {
                Text = I18N(context.Culture, Text),
                Class = GetClasses(),
                Style = GetStyles(),
                Role = Role,
                For = FormularItem != null ?
                    string.IsNullOrWhiteSpace(FormularItem.ID) ?
                    FormularItem.Name :
                    FormularItem.ID :
                    null
            };
        }
    }
}
