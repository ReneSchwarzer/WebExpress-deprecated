using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebControl
{
    public class ControlModalFormularConfirmDelete : ControlModalFormularConfirm
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
        public ControlModalFormularConfirmDelete(string id = null)
            : this(id, null)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
        /// <param name="content">Die Formularsteuerelemente</param>
        public ControlModalFormularConfirmDelete(string id, params ControlFormItem[] content)
            : base(id, content)
        {
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
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
                Content = new ControlFormItemStaticText() { Text = context.Page.I18N("webexpress.webapp", "delete.description") };
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
