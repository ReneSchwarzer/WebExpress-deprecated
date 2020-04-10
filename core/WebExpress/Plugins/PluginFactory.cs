namespace WebExpress.Plugins
{
    /// <summary>
    /// Factory-Klasse zum Erstellen von Plugins
    /// </summary>
    public abstract class PluginFactory : IPluginFactory
    {
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
        public abstract IPlugin Create(IPluginContext context, string configFileName);

        /// <summary>
        /// Erstellt eine neue Instanz eines Prozesszustandes
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="configFileName">Der Dateiname der Konfiguration oder null</param>
        /// <returns>Die Instanz des Plugins</returns>
        public IPlugin Create<T>(IPluginContext context, string configFileName) where T : IPlugin, new()
        {
            var import = new T() { Context = context };

            import.Init(configFileName);

            return import;
        }
    }
}
