using System.Reflection;
using WebExpress.Workers;

namespace WebExpress.UI
{
    public class WebExpressUIPlugin : WebExpress.Plugins.Plugin
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public WebExpressUIPlugin()
            : base("WebExpress.UI", "/Asserts/img/WebExpress.UI.svg")
        {
        }

        /// <summary>
        /// Initialisierung des Prozesszustandes. Hier können z.B. verwaltete Ressourcen geladen werden. 
        /// </summary>
        /// <param name="configFileName">Der Dateiname der Konfiguration oder null</param>
        public override void Init(string configFileName = null)
        {
            base.Init(configFileName);

            Context.Log.Info(MethodBase.GetCurrentMethod(), "Initialisierung WebExpress.UI-Plugin");

            // Ressourcen
            SiteMap.AddPage("Assets", "Assets", (x) => new WorkerRessource(x, Assembly.GetExecutingAssembly(), "WebExpress.UI"));
            SiteMap.AddPath("Assets", true);
        }
    }
}
