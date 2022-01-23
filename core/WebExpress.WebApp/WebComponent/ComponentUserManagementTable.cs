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
        /// Konstruktor
        /// </summary>
        public ComponentUserManagementTable()
            : base("4fd155dd-f6e2-4411-b6ed-14ee78713272")
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public override void Initialization(IComponentContext context, IPage page)
        {
            base.Initialization(context, page);

            var module = ModuleManager.GetModule(context.Application, "webexpress.webapp");

            RestApiUri = module.ContextPath.Append("/api/v1/user");

            Editors.Add(new ComponentCrudTableEditorLinkItem("webexpress.webapp:setting.usermanager.group.edit.label") { Icon = new PropertyIcon(TypeIcon.Edit) });
            Editors.Add(new ComponentCrudTableEditorSeperatorItem());
            Editors.Add(new ComponentCrudTableEditorLinkItem("webexpress.webapp:setting.usermanager.group.delete.label") { Icon = new PropertyIcon(TypeIcon.TrashAlt), Color = new PropertyColorText(TypeColorText.Danger) });
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            return base.Render(context);
        }
    }
}
