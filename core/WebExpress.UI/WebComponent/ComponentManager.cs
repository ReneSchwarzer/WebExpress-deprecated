﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebExpress.Application;
using WebExpress.Attribute;
using WebExpress.Module;
using WebExpress.Plugin;
using WebExpress.UI.Attribute;
using WebExpress.UI.WebControl;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.UI.WebComponent
{
    /// <summary>
    /// Komponentenverwaltung
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

            Context.Log.Info(message: I18N("webexpress.ui:componentmanager.initialization"));
        }

        /// <summary>
        /// Fügt Anwendungs-Einträge aus allen geladenen Plugins hinzu
        /// </summary>
        /// <param name="application">Die Anwendung, welche die Komponenten zugewiesen werden</param>
        public static void Register(IApplicationContext application)
        {
            foreach (var module in ModuleManager.Modules)
            {
                Register(module);
            }
        }

        /// <summary>
        /// Fügt die Komponenten-Schlüssel-Wert-Paare aus dem angegebenen Modul hinzu
        /// </summary>
        /// <param name="module">Das Modul, welches die einzufügenden Schlüssel-Wert-Paare enthällt</param>
        internal static void Register(IModuleContext module)
        {
            var assemblyName = module.Assembly.GetName().Name.ToLower();

            foreach (var component in module.Assembly.GetTypes().Where(x => x.IsClass && x.IsSealed && (x.GetInterfaces().Contains(typeof(IComponent)) || x.GetInterfaces().Contains(typeof(IComponentDynamic)))))
            {
                var moduleID = string.Empty;
                var pluginContext = new List<string>();
                var section = string.Empty;

                // Attribute ermitteln
                foreach (var customAttribute in component.CustomAttributes.Where(x => x.AttributeType.GetInterfaces().Contains(typeof(IModuleAttribute))))
                {
                    if (customAttribute.AttributeType == typeof(ModuleAttribute))
                    {
                        moduleID = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString().ToLower();
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
                if (string.IsNullOrWhiteSpace(moduleID))
                {
                    moduleID = "*";
                }

                if (!string.IsNullOrWhiteSpace(section))
                {
                    // Komponenten registrieren
                    if (!Dictionary.ContainsKey(moduleID))
                    {
                        Dictionary.Add(moduleID, new ComponentDictionaryItem());
                    }

                    var dictItem = Dictionary[moduleID];

                    if (pluginContext.Count > 0)
                    {
                        foreach (var context in pluginContext)
                        {
                            var key = string.Join(":", section, context);

                            if (!dictItem.ContainsKey(key))
                            {
                                dictItem.Add(key, new List<ComponentItem>());
                            }

                            dictItem[key].Add(new ComponentItem()
                            {
                                Context = new ComponentContext()
                                {
                                    Application = module.Application,
                                    Assembly = module.Assembly,
                                    Plugin = module.Plugin,
                                    Module = module,
                                    Log = Context.Log
                                },
                                Component = component
                            });

                            Context.Log.Info(message: I18N("webexpress.ui:componentmanager.register"), args: new object[] { component.Name, key, moduleID, module.Application.ApplicationID });
                        }
                    }
                    else
                    {
                        if (!dictItem.ContainsKey(section))
                        {
                            dictItem.Add(section, new List<ComponentItem>());
                        }

                        dictItem[section].Add(new ComponentItem()
                        {
                            Context = new ComponentContext()
                            {
                                Application = module.Application,
                                Assembly = module.Assembly,
                                Plugin = module.Plugin,
                                Module = module,
                                Log = Context.Log
                            },
                            Component = component
                        });

                        Context.Log.Info(message: I18N("webexpress.ui:componentmanager.register"), args: new object[] { component.Name, section, moduleID, module.Application.ApplicationID });
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
            throw new NotImplementedException("todo");
        }

        /// <summary>
        /// Erstellt alle Komponenten, die den Parametern entsprechen
        /// </summary>
        /// <param name="section">Die Sektion, indem die Komponente eingebettet wird</param>
        /// <param name="application">Die ArtefactID der Anwendung (z.B. Webexpress)</param>
        /// <param name="resourceContext">Der Kontext der Ressource</param>
        /// <returns>Eine Liste mit Komponenten</returns>
        public static IEnumerable<T> CreateComponent<T>(IApplicationContext application, string section, IReadOnlyList<string> resourceContext = null) where T : IControl
        {
            var list = new List<T>();
            var app = application?.ApplicationID?.ToLower();

            if (Dictionary.ContainsKey("*"))
            {
                var dictItem = Dictionary["*"];
                var sectionKey = section?.ToLower();

                if (dictItem.ContainsKey(sectionKey))
                {
                    var components = dictItem[sectionKey].Where(x => x.Component.GetInterfaces()
                        .Contains(typeof(T)))
                        .Select(x => new
                        {
                            Instance = (T)x.Component.Assembly.CreateInstance(x.Component.FullName),
                            Context = x.Context
                        }).ToList();

                    components.ForEach(x => (x.Instance as IComponent)?.Initialization(x.Context));

                    list.AddRange(components.Select(x => x.Instance));
                }

                if (resourceContext != null)
                {
                    foreach (var context in resourceContext)
                    {
                        sectionKey = string.Join(":", section?.ToLower(), context?.ToLower());

                        if (dictItem.ContainsKey(sectionKey))
                        {
                            var components = dictItem[sectionKey].Where(x => x.Component.GetInterfaces()
                            .Contains(typeof(T)))
                            .Select(x => new
                            {
                                Instance = (T)x.Component.Assembly.CreateInstance(x.Component.FullName),
                                Context = x.Context
                            }).ToList();

                            components.ForEach(x => (x.Instance as IComponent)?.Initialization(x.Context));

                            list.AddRange(components.Select(x => x.Instance));

                            components = dictItem[sectionKey].Where(x => x.Component.GetInterfaces()
                            .Contains(typeof(IComponentDynamic)))
                            .Select(x => new
                            {
                                Instance = (T)(x.Component.Assembly.CreateInstance(x.Component.FullName) as IComponentDynamic).Create<T>(),
                                Context = x.Context
                            }).ToList();

                            components.ForEach(x => (x.Instance as IComponent)?.Initialization(x.Context));

                            list.AddRange(components.Select(x => x.Instance));
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
                    var components = dictItem[sectionKey].Where(x => x.Component.GetInterfaces()
                        .Contains(typeof(T)))
                        .Select(x => new
                        {
                            Instance = (T)x.Component.Assembly.CreateInstance(x.Component.FullName),
                            Context = x.Context
                        }).ToList();

                    components.ForEach(x => (x.Instance as IComponent)?.Initialization(x.Context));

                    list.AddRange(components.Select(x => x.Instance));
                }

                if (resourceContext != null)
                {
                    foreach (var context in resourceContext)
                    {
                        sectionKey = string.Join(":", section?.ToLower(), context?.ToLower());

                        if (dictItem.ContainsKey(sectionKey))
                        {
                            var components = dictItem[sectionKey].Where(x => x.Component.GetInterfaces()
                            .Contains(typeof(T)))
                            .Select(x => new
                            {
                                Instance = (T)x.Component.Assembly.CreateInstance(x.Component.FullName),
                                Context = x.Context
                            }).ToList();

                            components.ForEach(x => (x.Instance as IComponent)?.Initialization(x.Context));

                            list.AddRange(components.Select(x => x.Instance));

                            components = dictItem[sectionKey].Where(x => x.Component.GetInterfaces()
                            .Contains(typeof(IComponentDynamic)))
                            .Select(x => new
                            {
                                Instance = (T)(x.Component.Assembly.CreateInstance(x.Component.FullName) as IComponentDynamic).Create<T>(),
                                Context = x.Context
                            }).ToList();

                            components.ForEach(x => (x.Instance as IComponent)?.Initialization(x.Context));

                            list.AddRange(components.Select(x => x.Instance));
                        }
                    }
                }
            }

            return list.Distinct(new ComponentComparer<T>());
        }
    }
}
