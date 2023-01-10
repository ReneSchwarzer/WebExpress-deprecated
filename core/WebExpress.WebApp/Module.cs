using WebExpress.UI.WebComponent;
using WebExpress.WebAttribute;
using WebExpress.WebModule;

namespace WebExpress.WebApp
{
    [Id("webexpress.webapp")]
    [Application("*")]
    [Name("module.name")]
    [Description("module.description")]
    [Icon("/assets/img/Logo.png")]
    [AssetPath("/")]
    [ContextPath("/wxapp")]
    public sealed class Module : IModule
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Module()
        {
        }

        /// <summary>
        /// Initialization des Moduls. Hier können z.B. verwaltete Ressourcen geladen werden. 
        /// </summary>
        /// <param name="context">Der Kontext, welcher für die Ausführung des Plugins gilt</param>
        public void Initialization(IModuleContext context)
        {
            //ComponentManager.Register(new[] { context.PluginContext });
            //SettingPageManager.Register(context.PluginContext);
            //IdentityManager.Register(context);
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Modul mit der Arbeit beginnt. Der Aufruf erfolgt nebenläufig.
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
