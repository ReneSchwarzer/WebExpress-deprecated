using WebExpress.WebApp.SettingPage;
using WebExpress.WebApp.WebIdentity;
using WebExpress.WebApp.WebUser;
using WebExpress.WebAttribute;
using WebExpress.WebPlugin;

namespace WebExpress.WebApp
{
    [Id("webexpress.webapp")]
    [Name("WebExpress.WebApp")]
    [Description("plugin.description")]
    [Icon("/assets/img/Logo.png")]
    public sealed class Plugin : IPlugin
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Plugin()
        {
        }

        /// <summary>
        /// Initialization des Plugins. Hier können z.B. verwaltete Ressourcen geladen werden. 
        /// </summary>
        /// <param name="context">Der Kontext, welcher für die Ausführung des Plugins gilt</param>
        public void Initialization(IPluginContext context)
        {
            SettingPageManager.Initialization(context);
            UserManager.Initialization(context.Host);
            IdentityManager.Initialization(context);
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Plugin mit der Arbeit beginnt. Der Aufruf von Run erfolgt nebenläufig.
        /// </summary>
        public void Run()
        {

        }

        /// <summary>
        /// Freigeben von nicht verwalteten Ressourcen, welche während der Verwendung reserviert wurden.
        /// </summary>
        public void Dispose()
        {

        }
    }
}
