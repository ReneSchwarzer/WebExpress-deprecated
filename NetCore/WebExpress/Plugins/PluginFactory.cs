using System;
using System.Collections.Generic;
using System.Text;

namespace WebExpress.Plugins
{
    /// <summary>
    /// Factory-Klasse zum Erstellen von Plugins
    /// </summary>
    public abstract class PluginFactory : IPluginFactory
    {
        /// <summary>
        /// Erstellt eine neue Instanz eines Prozesszustandes
        /// </summary>
        /// <param name="host">Der Benutzer</param>
        /// <param name="configFileName">Der Dateiname der Konfiguration oder null</param>
        /// <returns>Die Instanz des Prozesszustandes</returns>
        public abstract IPlugin Create(IHost host, string configFileName);

        /// <summary>
        /// Erstellt eine neue Instanz eines Prozesszustandes
        /// </summary>
        /// <param name="host">Der Benutzer</param>
        /// <param name="configFileName">Der Dateiname der Konfiguration oder null</param>
        /// <returns>Die Instanz des Prozesszustandes</returns>
        public IPlugin Create<T>(IHost host, string configFileName) where T : IPlugin, new()
        {
            var import = new T() { Host = host };

            import.Init(configFileName);

            return import;
        }
    }
}
