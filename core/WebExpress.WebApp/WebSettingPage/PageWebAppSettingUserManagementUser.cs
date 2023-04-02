using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebAttribute;
using WebExpress.WebApp.WebPage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebSettingPage
{
    /// <summary>
    /// Einstellungsseiteder der Nutzer
    /// </summary>
    [Id("SettingUser")]
    [Title("webexpress.webapp:setting.usermanager.user.label")]
    [Segment("user", "webexpress.webapp:setting.usermanager.user.label")]
    [ContextPath("/Setting")]
    [SettingSection(SettingSection.Primary)]
    [SettingIcon(TypeIcon.User)]
    [SettingGroup("webexpress.webapp:setting.usermanager.group.usermanagement.label")]
    [SettingContext("webexpress.webapp:setting.tab.general.label")]
    [Module("webexpress.webapp")]
    [Context("admin")]
    [Context("webexpress.webapp.usermanagement.user")]
    [Optional]
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

