namespace WebExpress.Plugins
{
    /// <summary>
    /// Kennzeichnet ein Factory-Objekt zum Erstellen von Plugins
    /// </summary>
    public interface IPluginFactory
    {
        /// <summary>
        /// Liefert den Dateinamen der Konfigurationsdatei
        /// </summary>
        string ConfigFileName { get; }

        /// <summary>
        /// Erstellt eine neue Instanz eines Prozesszustandes
        /// </summary>
        /// <param name="host">Der Kontext</param>
        /// <param name="configFileName">Der Dateiname der Konfiguration oder null</param>
        /// <returns>Die Instanz des Plugins</returns>
        IPlugin Create(IPluginContext host, string configFileName);
    }
}
