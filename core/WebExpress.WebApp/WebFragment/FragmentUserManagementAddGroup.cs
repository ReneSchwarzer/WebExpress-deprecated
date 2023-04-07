using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebFragment;
using WebExpress.WebApp.WebControl;
using WebExpress.WebAttribute;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebFragment
{
    [Section(Section.HeadlineSecondary)]
    [Module("webexpress.webapp")]
    [Context("webexpress.webapp.usermanagement.group")]
    public sealed class FragmentUserManagementAddGroup : FragmentControlButtonLink
    {
        /// <summary>
        /// Provides the modal dialog for adding a group.
        /// </summary>
        private ControlModalFormularGoupNew ModalDlg = new ControlModalFormularGoupNew("add_group")
        {

        };

        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentUserManagementAddGroup()
            : base("add_group")
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">The page where the fragment is active.</param>
        public override void Initialization(IFragmentContext context, IPage page)
        {
            base.Initialization(context, page);

            Text = "webexpress.webapp:setting.usermanager.group.add.label";
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);
            BackgroundColor = new PropertyColorButton(TypeColorButton.Primary);
            Icon = new PropertyIcon(TypeIcon.Plus);
            Modal = new PropertyModal(TypeModal.Modal, ModalDlg);
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            return base.Render(context);
        }
    }
}
