using WebExpress.UI.WebFragment;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebPlugin;

[assembly: WebExSystemPlugin()]

namespace WebExpress.WebApp
{
    [WebExID("webexpress.webapp")]
    [WebExName("WebExpress.WebApp")]
    [WebExDescription("plugin.description")]
    [WebExIcon("/assets/img/Logo.png")]
    public sealed class Plugin : IPlugin
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Plugin()
        {
        }

        /// <summary>
        /// Initialization of the plugin. Here, for example, managed resources can be loaded. 
        /// </summary>
        /// <param name="context">The context of the plugin that applies to the execution of the plugin.</param>
        public void Initialization(IPluginContext context)
        {
            //ComponentManager.Register(typeof(SettingPageManager));

            //UserManager.Initialization(context.Host);
            //IdentityManager.Initialization(context);

            var fragmentManager = ComponentManager.GetComponent<FragmentManager>();

            //fragmentManager.Register(context);
            ////IdentityManager.Register(context);
        }

        /// <summary>
        /// Called when the plugin starts working. Run is called concurrently.
        /// </summary>
        public void Run()
        {
        }

        /// <summary>
        /// Release of unmanaged resources reserved during use.
        /// </summary>
        public void Dispose()
        {

        }
    }
}
