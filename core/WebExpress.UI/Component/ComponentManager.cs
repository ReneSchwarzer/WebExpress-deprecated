using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebExpress.Attribute;
using WebExpress.Plugin;
using WebExpress.UI.Attribute;
using WebExpress.UI.Component;
using WebExpress.UI.WebControl;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.UI.Components
{
    /// <summary>
    /// Internationalisierung
    /// </summary>
    public class ComponentManager
    {
        /// <summary>
        /// Liefert oder setzt den Verweis auf Kontext des Plugins
        /// </summary>
        private static IPluginContext Context { get; set; }

        /// <summary>
        /// Liefert oder setzt das Verzeichnis, indem die Kompomenten gelistet sind
        /// </summary>
        private static ComponentDictionary Dictionary { get; set; } = new ComponentDictionary();

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Verweis auf den Kontext des Plugins</param>
        internal static void Initialization(IPluginContext context)
        {
            Context = context;

            Context.Log.Info(message: I18N("webexpress:componentmanager.initialization"));
        }

        /// <summary>
        /// Fügt Anwendungs-Einträge aus allen geladenen Plugins hinzu
        /// </summary>
        /// <param name="application">Die Anwendung, welche die Komponenten zugewiesen werden</param>
        public static void Register(string application)
        {
            foreach (var plugin in PluginManager.Plugins)
            {
                Register(plugin.Assembly, application);
            }
        }

        /// <summary>
        /// Fügt die Komponenten-Schlüssel-Wert-Paare aus dem angegebenen Plugin hinzu
        /// </summary>
        /// <param name="assembly">Das Assembly, welches die einzufügenden Schlüssel-Wert-Paare enthällt</param>
        /// <param name="application">Die Anwendung, welche die Komponenten zugewiesen werden</param>
        internal static void Register(Assembly assembly, string application)
        {
            var assemblyName = assembly.GetName().Name.ToLower();

            foreach (var component in assembly.GetTypes().Where(x => x.IsClass && x.IsSealed && (x.GetInterfaces().Contains(typeof(IComponent)) || x.GetInterfaces().Contains(typeof(IComponentDynamic)))))
            {
                var applicationID = string.Empty;
                var pluginContext = new List<string>();
                var section = string.Empty;

                // Attribute ermitteln
                foreach (var customAttribute in component.CustomAttributes.Where(x => x.AttributeType.GetInterfaces().Contains(typeof(IModuleAttribute))))
                {
                    if (customAttribute.AttributeType == typeof(ApplicationAttribute))
                    {
                        applicationID = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString().ToLower();
                    }
                }
                
                foreach (var customAttribute in component.CustomAttributes.Where(x => x.AttributeType.GetInterfaces().Contains(typeof(IResourceAttribute))))
                {
                    if (customAttribute.AttributeType == typeof(ContextAttribute))
                    {
                        pluginContext.Add(customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString().ToLower());
                    }
                }

                foreach (var customAttribute in component.CustomAttributes.Where(x => x.AttributeType.GetInterfaces().Contains(typeof(IComponentAttribute))))
                {
                    if (customAttribute.AttributeType == typeof(SectionAttribute))
                    {
                        section = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString().ToLower();
                    }
                }

                // Standard für Anwendung festlegen
                if (string.IsNullOrWhiteSpace(applicationID))
                {
                    applicationID = "*";
                }

                if (!string.IsNullOrWhiteSpace(section))
                {
                    // Komponenten registrieren
                    if (!Dictionary.ContainsKey(applicationID))
                    {
                        Dictionary.Add(applicationID, new ComponentDictionaryItem());
                    }

                    var dictItem = Dictionary[applicationID];

                    if (pluginContext.Count > 0)
                    {
                        foreach (var context in pluginContext)
                        {
                            var key = string.Join(":", section, context);

                            if (!dictItem.ContainsKey(key))
                            {
                                dictItem.Add(key, new List<Type>());
                            }

                            dictItem[key].Add(component);

                            Context.Log.Info(message: I18N("webexpress:componentmanager.register"), args: new object[] { component.Name, key, applicationID });
                        }
                    }
                    else
                    {
                        if (!dictItem.ContainsKey(section))
                        {
                            dictItem.Add(section, new List<Type>());
                        }

                        dictItem[section].Add(component);

                        Context.Log.Info(message: I18N("webexpress:componentmanager.register"), args: new object[] { component.Name, section, applicationID });
                    }

                }
                else if (string.IsNullOrWhiteSpace(section))
                {
                    Context.Log.Info(message: I18N("componentmanager.error.section"));
                }

            }
        }

        /// <summary>
        /// Entfernt alle Internationalisierungs-Schlüssel-Wert-Paare, welche dem angegebenen Plugin zugeordnet sind
        /// </summary>
        /// <param name="plugin">Das Plugin, welches die zu entfernenden Schlüssel-Wert-Paare enthällt</param>
        public static void Remove(IPlugin plugin)
        {
            //Remove(plugin.GetType().Assembly, plugin.Context.ApplicationID);
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
        /// <param name="resourceContext">Der Kontext der Ressource</param>
        /// <returns>Eine Liste mit Komponenten</returns>
        public static IEnumerable<T> CreateComponent<T>(string application, string section, IReadOnlyList<string> resourceContext = null) where T : IControl
        {
            var list = new List<T>();
            var app = application?.ToLower();

            if (Dictionary.ContainsKey("*"))
            {
                var dictItem = Dictionary["*"];
                var sectionKey = section?.ToLower();

                if (dictItem.ContainsKey(sectionKey))
                {
                    list.AddRange(dictItem[sectionKey].Where(x => x.GetInterfaces().Contains(typeof(T))).Select(x => (T)x.Assembly.CreateInstance(x.FullName)));
                }

                if (resourceContext != null)
                {
                    foreach (var context in resourceContext)
                    {
                        sectionKey = string.Join(":", section?.ToLower(), context?.ToLower());

                        if (dictItem.ContainsKey(sectionKey))
                        {
                            list.AddRange
                            (
                                dictItem[sectionKey].Where(x => x.GetInterfaces().Contains(typeof(T)))
                                                    .Select(x => (T)x.Assembly.CreateInstance(x.FullName))
                            );

                            list.AddRange
                            (
                                dictItem[sectionKey].Where(x => x.GetInterfaces().Contains(typeof(IComponentDynamic)))
                                                    .Select(x => (x.Assembly.CreateInstance(x.FullName) as IComponentDynamic).Create<T>())
                                                    .SelectMany(x => x)
                            );
                        }
                    }
                }

                if (app == "*") return list;
            }

            if (Dictionary.ContainsKey(app))
            {
                var dictItem = Dictionary[app];
                var sectionKey = section?.ToLower();

                if (dictItem.ContainsKey(sectionKey))
                {
                    list.AddRange(dictItem[sectionKey].Where(x => x.GetInterfaces().Contains(typeof(T))).Select(x => (T)x.Assembly.CreateInstance(x.FullName)));
                }

                if (resourceContext != null)
                {
                    foreach (var context in resourceContext)
                    {
                        sectionKey = string.Join(":", section?.ToLower(), context?.ToLower());

                        if (dictItem.ContainsKey(sectionKey))
                        {
                            list.AddRange
                            (
                                dictItem[sectionKey].Where(x => x.GetInterfaces().Contains(typeof(T)))
                                                    .Select(x => (T)x.Assembly.CreateInstance(x.FullName))
                            );

                            list.AddRange
                            (
                                dictItem[sectionKey].Where(x => x.GetInterfaces().Contains(typeof(IComponentDynamic)))
                                                    .Select(x => (x.Assembly.CreateInstance(x.FullName) as IComponentDynamic).Create<T>())
                                                    .SelectMany(x => x)
                            );
                        }
                    }
                }
            }

            return list;
        }
    }
}
