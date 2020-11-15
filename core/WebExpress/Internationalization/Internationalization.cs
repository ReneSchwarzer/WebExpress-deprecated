using System.IO;
using System.Linq;
using System.Reflection;
using WebExpress.Plugins;

namespace WebExpress.Internationalization
{
    /// <summary>
    /// Internationalisierung
    /// </summary>
    public class Internationalization
    {
        /// <summary>
        /// Fügt die Internationalisierungs-Schlüssel-Wert-Paare aus dem angegebenen Plugin hinzu
        /// </summary>
        /// <param name="plugin">Das Plugin, welches die einzufügenden Schlüssel-Wert-Paare enthällt</param>
        public static void Add(IPlugin plugin)
        {
            Add(plugin.GetType().Assembly);
        }

        /// <summary>
        /// Fügt die Internationalisierungs-Schlüssel-Wert-Paare aus dem angegebenen Plugin hinzu
        /// </summary>
        /// <param name="assembly">Das Assembly, welches die einzufügenden Schlüssel-Wert-Paare enthällt</param>
        internal static void Add(Assembly assembly)
        {
            var assemblyName = assembly.GetName().Name.ToLower();
            var name = assemblyName + ".internationalization.";
            var resources = assembly.GetManifestResourceNames().Where(x => x.ToLower().Contains(name));

            foreach (var languageResource in resources)
            {
                var language = languageResource.Split('.').LastOrDefault()?.ToLower();

                if (!InternationalizationDictionary.Instance.ContainsKey(language))
                {
                    InternationalizationDictionary.Instance.Add(language, new InternationalizationDictionaryItem());
                }

                var dictItem = InternationalizationDictionary.Instance[language];

                using (var stream = assembly.GetManifestResourceStream(languageResource))
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
        }

        /// <summary>
        /// Entfernt alle Internationalisierungs-Schlüssel-Wert-Paare, welche dem angegebenen Plugin zugeordnet sind
        /// </summary>
        /// <param name="plugin">Das Plugin, welches die zu entfernenden Schlüssel-Wert-Paare enthällt</param>
        public static void Remove(IPlugin plugin)
        {
            Remove(plugin.GetType().Assembly);
        }

        /// <summary>
        /// Entfernt alle Internationalisierungs-Schlüssel-Wert-Paare, welche dem angegebenen Plugin zugeordnet sind
        /// </summary>
        /// <param name="assembly">Das Assembly, welches die zu entfernenden Schlüssel-Wert-Paare enthällt</param>
        internal static void Remove(Assembly assembly)
        {
            var assemblyName = assembly.GetName().Name.ToLower();
            
            foreach (var language in InternationalizationDictionary.Instance.Keys)
            {
                var dictItem = InternationalizationDictionary.Instance[language];
                var items = dictItem.Keys.Where(x => x.StartsWith(assemblyName));

                foreach (var key in dictItem.Keys.Where(x => !x.StartsWith(assemblyName + ":")))
                {
                    dictItem.Remove(key);
                }
            }
        }
    }
}
