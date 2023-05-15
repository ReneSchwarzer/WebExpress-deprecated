using WebExpress.WebAttribute;
using WebExpress.WebPlugin;

[assembly: WebExSystemPlugin()]

namespace WebExpress.UI
{
    [WebExName("WebExpress.UI")]
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
