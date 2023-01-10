using WebExpress.UI.WebComponent;
using WebExpress.WebAttribute;
using WebExpress.WebModule;
using WebExpress.WebPlugin;

namespace WebExpress.UI
{
    [Id("webexpress.ui")]
    [Name("WebExpress.UI")]
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
            ComponentManager.Initialization(context.Host);
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Plugin mit der Arbeit beginnt. Der Aufruf von Run erfolgt nebenläufig.
        /// </summary>
        public void Run()
        {
            ComponentManager.Register(PluginManager.Plugins);
        }

        /// <summary>
        /// Freigeben von nicht verwalteten Ressourcen, welche während der Verwendung reserviert wurden.
        /// </summary>
        public void Dispose()
        {

        }
    }
}
