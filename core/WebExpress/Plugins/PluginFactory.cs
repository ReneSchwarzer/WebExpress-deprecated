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

            // Internationalisierung
            var assemblyName = typeof(T).Assembly.GetName().Name.ToLower();
            var name = assemblyName + ".internationalization.";
            var resources = typeof(T).Assembly.GetManifestResourceNames().Where(x => x.ToLower().Contains(name));

            foreach (var languageResource in resources)
            {
                var language = languageResource.Split('.').LastOrDefault()?.ToLower();

                if (!InternationalizationDictionary.Instance.ContainsKey(language))
                {
                    InternationalizationDictionary.Instance.Add(language, new InternationalizationDictionaryItem());
                }

                var dictItem = InternationalizationDictionary.Instance[language];

                using (var stream = typeof(T).Assembly.GetManifestResourceStream(languageResource))
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        while (!streamReader.EndOfStream)
                        {
                            var line = streamReader.ReadLine();
                            if (!line.StartsWith('#') && !string.IsNullOrWhiteSpace(line))
                            {
                                var split = line.Split('=');
                                var key = assemblyName + ":" + split[0]?.Trim().ToLower();

                                if (!dictItem.ContainsKey(key))
                                {
                                    dictItem.Add(key, string.Join("=", split.Skip(1)));
                                }
                            }
                        }
                    }
                }
            }

            return plugin;
        }
    }
}
