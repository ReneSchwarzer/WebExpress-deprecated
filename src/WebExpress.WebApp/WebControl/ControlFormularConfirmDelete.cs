using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebControl
{
    public class ControlFormularConfirmDelete : ControlFormularConfirm
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlFormularConfirmDelete(string id = null)
            : base(id)
        {
            ButtonIcon = new PropertyIcon(TypeIcon.TrashAlt);
            ButtonColor = new PropertyColorButton(TypeColorButton.Danger);
        }

        /// <summary>
        /// Initializes the form.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        public override void Initialize(RenderContextFormular context)
        {
            base.Initialize(context);

            ButtonLabel = context.Page.I18N("webexpress.webapp", "delete.label");
            Content.Text = context.Page.I18N("webexpress.webapp", "delete.description");
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            ButtonLabel = context.Page.I18N("webexpress.webapp", "delete.label");

            return base.Render(context);
        }
    }
}
