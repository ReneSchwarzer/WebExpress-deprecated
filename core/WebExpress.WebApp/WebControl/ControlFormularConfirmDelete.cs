using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebControl
{
    public class ControlFormularConfirmDelete : ControlFormularConfirm
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularConfirmDelete(string id = null)
            : base(id)
        {
            ButtonIcon = new PropertyIcon(TypeIcon.TrashAlt);
            ButtonColor = new PropertyColorButton(TypeColorButton.Danger);
        }

        /// <summary>
        /// Initialisiert das Formular
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public override void Initialize(RenderContextFormular context)
        {
            base.Initialize(context);

            ButtonLabel = context.Page.I18N("webexpress.webapp", "delete.label");
            Content.Text = context.Page.I18N("webexpress.webapp", "delete.description");
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            ButtonLabel = context.Page.I18N("webexpress.webapp", "delete.label");

            return base.Render(context);
        }
    }
}
