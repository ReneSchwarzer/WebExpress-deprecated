using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebApp.WebScope;
using WebExpress.WebAttribute;
using WebExpress.WebResource;
using WebExpress.WebScope;

namespace WebExpress.WebApp.WebSettingPage
{
    /// <summary>
    /// Users settings page.
    /// </summary>
    [Title("webexpress.webapp:setting.usermanager.user.label")]
    [Segment("user", "webexpress.webapp:setting.usermanager.user.label")]
    [ContextPath("/setting")]
    [SettingSection(SettingSection.Primary)]
    [SettingIcon(TypeIcon.User)]
    [SettingGroup("webexpress.webapp:setting.usermanager.group.usermanagement.label")]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [Module<Module>]
    [Scope<ScopeAdmin>]
    [Optional]
    public sealed class PageWebAppSettingUserManagementUser : PageWebAppSetting, IScope
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PageWebAppSettingUserManagementUser()
        {
            Icon = new PropertyIcon(TypeIcon.User);
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// The processing of the request.
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);
        }
    }
}

