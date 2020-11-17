using System;
using System.IO;
using System.Linq;
using System.Reflection;
using WebExpress.Pages;
using WebExpress.Plugins;

namespace WebExpress.Internationalization
{
    /// <summary>
    /// Internationalisierung
    /// </summary>
    public class InternationalizationManager
    {
        /// <summary>
        /// Liefert oder setzt das Verzeichnis, indem die Internationalisierungs-Schlüssel-Wert-Paare gelistet sind
        /// </summary>
        private static InternationalizationDictionary Dictionary { get; set; } = new InternationalizationDictionary();

        /// <summary>
        /// Fügt die Internationalisierungs-Schlüssel-Wert-Paare aus dem angegebenen Plugin hinzu
        /// </summary>
        /// <param name="plugin">Das Plugin, welches die einzufügenden Schlüssel-Wert-Paare enthällt</param>
        /// <param name="application">Die Anwendung, welche die Internationalisierungsdaten zugewiesen werden</param>
        public static void Add(IPlugin plugin, string application)
        {
            Add(plugin.GetType().Assembly, application);
        }

        /// <summary>
        /// Fügt die Internationalisierungs-Schlüssel-Wert-Paare aus dem angegebenen Plugin hinzu
        /// </summary>
        /// <param name="assembly">Das Assembly, welches die einzufügenden Schlüssel-Wert-Paare enthällt</param>
        /// <param name="application">Die Anwendung, welche die Internationalisierungsdaten zugewiesen werden</param>
        internal static void Add(Assembly assembly, string application)
        {
            var assemblyName = assembly.GetName().Name.ToLower();
            var name = assemblyName + ".internationalization.";
            var resources = assembly.GetManifestResourceNames().Where(x => x.ToLower().Contains(name));

            foreach (var languageResource in resources)
            {
                var language = languageResource.Split('.').LastOrDefault()?.ToLower();

                if (!Dictionary.ContainsKey(language))
                {
                    Dictionary.Add(language, new InternationalizationDictionaryItem());
                }

                var dictItem = Dictionary[language];

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
                                var key = application?.ToLower() + ":" + split[0]?.Trim().ToLower();

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
            Remove(plugin.GetType().Assembly, plugin.Context.AppArtifactID);
        }

        /// <summary>
        /// Entfernt alle Internationalisierungs-Schlüssel-Wert-Paare, welche dem angegebenen Plugin zugeordnet sind
        /// </summary>
        /// <param name="assembly">Das Assembly, welches die zu entfernenden Schlüssel-Wert-Paare enthällt</param>
        /// <param name="application">Die Anwendung, welche die Internationalisierungsdaten zugewiesen werden</param>
        internal static void Remove(Assembly assembly, string application)
        {
            //var assemblyName = assembly.GetName().Name.ToLower();

            //foreach (var language in InternationalizationDictionary.Instance.Keys)
            //{
            //    var dictItem = InternationalizationDictionary.Instance[language];
            //    var items = dictItem.Keys.Where(x => x.StartsWith(assemblyName));

            //    foreach (var key in dictItem.Keys.Where(x => !x.StartsWith(application + ":")))
            //    {
            //        dictItem.Remove(key);
            //    }
            //}

            throw new NotImplementedException("todo");
        }

        /// <summary>
        /// Internationalisierung
        /// </summary>
        /// <param name="page">Das Objekt, welches erweitert wird</param>
        /// <param name="application">Die Anwendung, welche die Internationalisierungsdaten zugewiesen werden</param>
        /// <param name="key">Der Schlüssel</param>
        /// <returns>Der Wert des Schlüssels in der aktuellen Sprache</returns>
        public static string I18N(IPage page, string application, string key)
        {
            var language = page?.Request?.HeaderFields?.AcceptLanguage?.TrimStart().Substring(0, 2).ToLower();

            if (string.IsNullOrWhiteSpace(key))
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(language) || language == "*")
            {
                language = "en";
            }

            var item = Dictionary[language];
            var k = application?.ToLower() + ":" + key.ToLower();

            if (item.ContainsKey(k))
            {
                return item[k];
            }

            return key;
        }
    }
}
