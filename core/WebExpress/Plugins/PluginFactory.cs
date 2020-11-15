using System.IO;
using System.Linq;
using WebExpress.Internationalization;

namespace WebExpress.Plugins
{
    /// <summary>
    /// Factory-Klasse zum Erstellen von Plugins
    /// </summary>
    public abstract class PluginFactory : IPluginFactory
    {
        /// <summary>
        /// Liefert oder setzt die ID
        /// </summary>
        public abstract string ArtifactID { get; }

        /// <summary>
        /// Liefert oder setzt die HerstellerID
        /// </summary>
        public abstract string ManufacturerID { get; }

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Liefert oder setzt den Namen
        /// </summary>
        public abstract string Version { get; }

        /// <summary>
        /// Liefert den Dateinamen der Konfigurationsdatei
        /// </summary>
        public abstract string ConfigFileName { get; }

        /// <summary>
        /// Erstellt eine neue Instanz eines Prozesszustandes
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="configFileName">Der Dateiname der Konfiguration oder null</param>
        /// <returns>Die Instanz des Plugins</returns>
        public abstract IPlugin Create(HttpServerContext context, string configFileName);

        /// <summary>
        /// Erstellt eine neue Instanz eines Prozesszustandes
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="configFileName">Der Dateiname der Konfiguration oder null</param>
        /// <returns>Die Instanz des Plugins</returns>
        public IPlugin Create<T>(HttpServerContext context, string configFileName) where T : IPlugin, new()
        {
            var plugin = new T() { };
            plugin.Context = new PluginContext(context, plugin);

            plugin.Init(configFileName);

            Internationalization.Internationalization.Add(plugin);

            return plugin;
        }
    }
}
