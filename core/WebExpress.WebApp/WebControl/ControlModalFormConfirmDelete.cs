using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;

namespace WebExpress.WebApp.WebControl
{
    public class ControlModalFormConfirmDelete : ControlModalFormConfirm
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlModalFormConfirmDelete(string id = null)
            : this(id, null)
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="content">Die Formularsteuerelemente</param>
        public ControlModalFormConfirmDelete(string id, params ControlFormularItem[] content)
            : base(id, content)
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
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            if (string.IsNullOrWhiteSpace(Header))
            {
                Header = context.Page.I18N("webexpress.webapp", "delete.header");
            }

            if (string.IsNullOrWhiteSpace(ButtonLabel))
            {
                ButtonLabel = context.Page.I18N("webexpress.webapp", "delete.label");
            }

            if (Content == null)
            {
                Content = new ControlFormularItemStaticText() { Text = context.Page.I18N("webexpress.webapp", "delete.description") };
            }

            if (ButtonIcon == null)
            {
                ButtonIcon = new PropertyIcon(TypeIcon.TrashAlt);
            }

            if (ButtonColor == null)
            {
                ButtonColor = new PropertyColorButton(TypeColorButton.Danger);
            }

            return base.Render(context);
        }
    }
}
