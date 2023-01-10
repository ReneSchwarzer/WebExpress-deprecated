using WebExpress.Html;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.WebAttribute;
using WebExpress.WebModule;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebComponent
{
    [Section(Section.ContentPrimary)]
    [Application("webexpress.webapp")]
    [Context("webexpress.webapp.usermanagement.user")]
    [Cache()]
    //[Condition(typeof(ConditionUnix))]
    public sealed class ComponentUserManagementTable : ComponentCrudTable
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ComponentUserManagementTable()
            : base("4fd155dd-f6e2-4411-b6ed-14ee78713272")
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context of the component.</param>
        /// <param name="page">The page to display the component.</param>
        public override void Initialization(IComponentContext context, IPage page)
        {
            base.Initialization(context, page);

            var module = ModuleManager.GetModule(page.ApplicationContext, "webexpress.webapp");

            RestApiUri = module.ContextPath.Append("/api/v1/user");

            Editors.Add(new ComponentCrudTableEditorLinkItem("webexpress.webapp:setting.usermanager.group.edit.label") { Icon = new PropertyIcon(TypeIcon.Edit) });
            Editors.Add(new ComponentCrudTableEditorSeperatorItem());
            Editors.Add(new ComponentCrudTableEditorLinkItem("webexpress.webapp:setting.usermanager.group.delete.label") { Icon = new PropertyIcon(TypeIcon.TrashAlt), Color = new PropertyColorText(TypeColorText.Danger) });
        }

        /// <summary>
        /// Convert to HTML.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as HTML.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            return base.Render(context);
        }
    }
}
