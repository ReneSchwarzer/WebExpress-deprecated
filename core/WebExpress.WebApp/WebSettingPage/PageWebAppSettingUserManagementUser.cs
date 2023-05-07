using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebSettingPage
{
    /// <summary>
    /// Einstellungsseiteder der Nutzer
    /// </summary>
    [WebExID("SettingUser")]
    [WebExTitle("webexpress.webapp:setting.usermanager.user.label")]
    [WebExSegment("user", "webexpress.webapp:setting.usermanager.user.label")]
    [WebExContextPath("/Setting")]
    [WebExSettingSection(WebExSettingSection.Primary)]
    [WebExSettingIcon(TypeIcon.User)]
    [WebExSettingGroup("webexpress.webapp:setting.usermanager.group.usermanagement.label")]
    [WebExSettingContext("webexpress.webapp:setting.tab.general.label")]
    [WebExModule("webexpress.webapp")]
    [WebExContext("admin")]
    [WebExContext("webexpress.webapp.usermanagement.user")]
    [WebExOptional]
    public sealed class PageWebAppSettingUserManagementUser : PageWebAppSetting
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
        /// Vorverarbeitung
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);
        }
    }
}

