using System;
using System.Collections.Generic;
using System.Text;

namespace WebExpress.Plugins
{
    /// <summary>
    /// Kennzeichnet ein Factory-Objekt zum Erstellen von Plugins
    /// </summary>
    public interface IPluginFactory
    {
        /// <summary>
        /// Erstellt eine neue Instanz eines Prozesszustandes
        /// </summary>
        /// <param name="host">Verweis auf den Host</param>
        /// <param name="configFileName">Der Dateiname der Konfiguration oder null</param>
        /// <returns>Die Instanz des Prozesszustandes</returns>
        IPlugin Create(IHost host, string configFileName);
    }
}
