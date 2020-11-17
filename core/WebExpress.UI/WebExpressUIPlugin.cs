using System.Reflection;
using WebExpress.Plugins;
using WebExpress.Workers;

namespace WebExpress.UI
{
    public class WebExpressUIPlugin : WebExpress.Plugins.Plugin
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public WebExpressUIPlugin()
        {
        }

        /// <summary>
        /// Initialisierung des Plugins. Hier können z.B. verwaltete Ressourcen geladen werden. 
        /// </summary>
        /// <param name="context">Der Kontext, welcher für die Ausführung des Plugins gilt</param>
        public override void Init(IPluginContext context)
        {
            base.Init(context);

            // Ressourcen
            SiteMap.AddPage("Assets", "Assets", (x) => new WorkerRessource(x, Assembly.GetExecutingAssembly(), "WebExpress.UI"));
            SiteMap.AddPath("Assets", true);
        }
    }
}
