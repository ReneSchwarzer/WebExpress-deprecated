using System.Reflection;
using WebExpress;
using WebExpress.Plugins;
using WebExpress.UI;

namespace Education
{
    public class WebExpressUIFactory : PluginFactory
    {
        /// <summary>
        /// Liefert den Anwendungsnamen indem das Plugin aktiv ist. 
        /// </summary>
        public override string AppArtifactID => "org.WebExpress";

        /// <summary>
        /// Liefert oder setzt die ID
        /// </summary>
        public override string ArtifactID => "UI";

        /// <summary>
        /// Liefert oder setzt die HerstellerID
        /// </summary>
        public override string ManufacturerID => "org.WebExpress";

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        public override string Description => "Benutzersteuerelemente für die Entwicklung";

        /// <summary>
        /// Liefert oder setzt den Namen
        /// </summary>
        public override string Version => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>
        /// Liefert das Icon des Plugins
        /// </summary>
        public override string Icon => "/Asserts/img/WebExpress.UI.svg";

        /// <summary>
        /// Liefert den Dateinamen der Konfigurationsdatei
        /// </summary>
        public override string ConfigFileName => "webexpress.ui.config.xml";

        /// <summary>
        /// Erstellt eine neue Instanz eines Prozesszustandes
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="configFileName">Der Dateiname der Konfiguration oder null</param>
        /// <returns>Die Instanz des Plugins</returns>
        public override IPlugin Create(HttpServerContext context, string configFileName)
        {
            var plugin = Create<WebExpressUIPlugin>(context, configFileName);

            return plugin;
        }
    }
}
