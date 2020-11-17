using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WebExpress.Plugins
{
    /// <summary>
    /// Internationalisierung
    /// </summary>
    public class PluginComponentManager
    {
        /// <summary>
        /// Liefert oder setzt das Verzeichnis, indem die Kompomenten gelistet sind
        /// </summary>
        private static PluginComponentDictionary Dictionary { get; set; } = new PluginComponentDictionary();

        /// <summary>
        /// Fügt die Komponenten-Schlüssel-Wert-Paare aus dem angegebenen Plugin hinzu
        /// </summary>
        /// <param name="plugin">Das Plugin, welches die einzufügenden Schlüssel-Wert-Paare enthällt</param>
        /// <param name="application">Die Anwendung, welche die Komponenten zugewiesen werden</param>
        public static void Add(IPlugin plugin, string application)
        {
            Add(plugin.GetType().Assembly, application);
        }

        /// <summary>
        /// Fügt die Komponenten-Schlüssel-Wert-Paare aus dem angegebenen Plugin hinzu
        /// </summary>
        /// <param name="assembly">Das Assembly, welches die einzufügenden Schlüssel-Wert-Paare enthällt</param>
        /// <param name="application">Die Anwendung, welche die Komponenten zugewiesen werden</param>
        internal static void Add(Assembly assembly, string application)
        {
            var assemblyName = assembly.GetName().Name.ToLower();

            foreach (var component in assembly.GetTypes().Where(x => x.IsClass == true && x.CustomAttributes.Where(y => y.AttributeType.GetInterfaces().Contains(typeof(IPluginComponentAttribute))).Count() > 0))
            {
                foreach (var customAttribute in component.CustomAttributes.Where(x => x.AttributeType.GetInterfaces().Contains(typeof(IPluginComponentAttribute))))
                {
                    var arguments = customAttribute.ConstructorArguments;

                    var section = arguments.FirstOrDefault().Value?.ToString().ToLower();
                    var key = string.Join(":", application.ToLower(), string.Join(":", arguments.Skip(1).Select(x => x.Value?.ToString().ToLower())));

                    if (!Dictionary.ContainsKey(section))
                    {
                        Dictionary.Add(section, new PluginComponentDictionaryItem());
                    }

                    var dictItem = Dictionary[section];

                    if (!dictItem.ContainsKey(key))
                    {
                        dictItem.Add(key, new List<Type>());
                    }

                    dictItem[key].Add(component);
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

            //foreach (var language in PluginComponentDictionary.Instance.Keys)
            //{
            //    var dictItem = PluginComponentDictionary.Instance[language];
            //    var items = dictItem.Keys.Where(x => x.StartsWith(assemblyName));

            //    foreach (var key in dictItem.Keys.Where(x => !x.StartsWith(assemblyName + ":")))
            //    {
            //        dictItem.Remove(key);
            //    }
            //}

            throw new NotImplementedException("todo");
        }

        /// <summary>
        /// Erstellt alle Komponenten, die den Parametern entsprechen
        /// </summary>
        /// <param name="section">Die Sektion, indem die Komponente eingebettet wird</param>
        /// <param name="application">Die ArtefactID der Anwendung (z.B. Webexpress)</param>
        /// <param name="keys">Die Schlüssel</param>
        /// <returns>Eine Liste mit Komponenten</returns>
        public static IEnumerable<T> CreatePluginComponents<T>(string section, string application, params string[] keys) 
        {
            var list = new List<T>();
            var sectionKey = section?.ToLower();

            if (Dictionary.ContainsKey(sectionKey))
            {
                var dictItem = Dictionary[sectionKey];
                var key = string.Join(":", application, string.Join(":", keys)).ToLower();

                if (dictItem.ContainsKey(key))
                {
                    list.AddRange(dictItem[key].Where(x => x.GetInterfaces().Contains(typeof(T))).Select(x => (T)x.Assembly.CreateInstance(x.FullName)));
                }
            }

            return list;
        }
    }
}
