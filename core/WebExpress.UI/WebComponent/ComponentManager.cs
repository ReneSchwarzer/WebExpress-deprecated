using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.WebAttribute;
using WebExpress.WebCondition;
using WebExpress.WebPage;
using WebExpress.WebPlugin;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.UI.WebComponent
{
    /// <summary>
    /// Component Management
    /// </summary>
    public class ComponentManager
    {
        /// <summary>
        /// Returns the reference to the context of the host.
        /// </summary>
        private static IHttpServerContext Context { get; set; }

        /// <summary>
        /// Delivers or sets the directory where the components are listed.
        /// </summary>
        private static ComponentDictionary Dictionary { get; set; } = new ComponentDictionary();

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The reference to the context of the host.</param>
        internal static void Initialization(IHttpServerContext context)
        {
            Context = context;

            Context.Log.Info(message: I18N("webexpress.ui:componentmanager.initialization"));
        }

        /// <summary>
        /// Discovers and registers the components from the specified plugins.
        /// </summary>
        /// <param name="pluginContexts">A list with plugin contexts that contain the components.</param>
        internal static void Register(IEnumerable<IPluginContext> pluginContexts)
        {
            foreach (var pluginContext in pluginContexts)
            {
                // register plugin
                if (!Dictionary.ContainsKey(pluginContext))
                {
                    Dictionary.Add(pluginContext, new Dictionary<string, ComponentDictionaryItem>());
                }

                var pluginDictionary = Dictionary[pluginContext];
                var assembly = pluginContext.Assembly;

                foreach (var component in assembly.GetTypes().Where(x => x.IsClass && x.IsSealed && (x.GetInterfaces().Contains(typeof(IComponent)) || x.GetInterfaces().Contains(typeof(IComponentDynamic)))))
                {
                    var appID = string.Empty;
                    var resourceContextFilter = new List<string>();
                    var section = string.Empty;
                    var conditions = new List<ICondition>();
                    var cache = false;
                    var order = 0;

                    // determining attributes
                    foreach (var customAttribute in component.CustomAttributes.Where(x => x.AttributeType.GetInterfaces().Contains(typeof(IModuleAttribute))))
                    {
                        if (customAttribute.AttributeType == typeof(ApplicationAttribute))
                        {
                            appID = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString().ToLower();
                        }
                    }

                    foreach (var customAttribute in component.CustomAttributes.Where(x => x.AttributeType.GetInterfaces().Contains(typeof(IResourceAttribute))))
                    {
                        if (customAttribute.AttributeType == typeof(ContextAttribute))
                        {
                            resourceContextFilter.Add(customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString().ToLower());
                        }
                        else if (customAttribute.AttributeType == typeof(ConditionAttribute))
                        {
                            var condition = (Type)customAttribute.ConstructorArguments.FirstOrDefault().Value;

                            if (condition.GetInterfaces().Contains(typeof(ICondition)))
                            {
                                conditions.Add(condition?.Assembly.CreateInstance(condition?.FullName) as ICondition);
                            }
                            else
                            {
                                Context.Log.Warning(message: I18N("webexpress.ui:componentmanager.wrongtype", condition.Name, typeof(ICondition).Name));
                            }
                        }
                        else if (customAttribute.AttributeType == typeof(CacheAttribute))
                        {
                            cache = true;
                        }
                    }

                    foreach (var customAttribute in component.CustomAttributes.Where(x => x.AttributeType.GetInterfaces().Contains(typeof(IComponentAttribute))))
                    {
                        if (customAttribute.AttributeType == typeof(SectionAttribute))
                        {
                            section = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString().ToLower();
                        }
                        else if (customAttribute.AttributeType == typeof(OrderAttribute))
                        {
                            try
                            {
                                order = Convert.ToInt32(customAttribute.ConstructorArguments.FirstOrDefault().Value);
                            }
                            catch
                            {
                            }
                        }
                    }

                    // set default
                    if (string.IsNullOrWhiteSpace(appID))
                    {
                        appID = "*";
                    }

                    if (!string.IsNullOrWhiteSpace(section))
                    {
                        // register components
                        if (!pluginDictionary.ContainsKey(appID))
                        {
                            pluginDictionary.Add(appID, new ComponentDictionaryItem());
                        }

                        var dictItem = pluginDictionary[appID];

                        if (resourceContextFilter.Count > 0)
                        {
                            foreach (var context in resourceContextFilter)
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
                                        PluginContext = pluginContext,
                                        ApplicationID = appID,
                                        Conditions = conditions,
                                        Cache = cache,
                                        Log = Context.Log
                                    },
                                    Component = component,
                                    Order = order
                                });

                                Context.Log.Info(message: I18N("webexpress.ui:componentmanager.register", component.Name, key, appID));
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
                                    PluginContext = pluginContext,
                                    ApplicationID = appID,
                                    Conditions = conditions,
                                    Cache = cache,
                                    Log = Context.Log
                                },
                                Component = component,
                                Order = order
                            });

                            Context.Log.Info(message: I18N("webexpress.ui:componentmanager.register", component.Name, section, appID));
                        }

                    }
                    else if (string.IsNullOrWhiteSpace(section))
                    {
                        Context.Log.Info(message: I18N("componentmanager.error.section"));
                    }

                }

                foreach (var modules in pluginDictionary)
                {
                    foreach (var section in modules.Value)
                    {
                        section.Value.Sort(new ComponentItemComparer());
                    }
                }
            }
        }

        /// <summary>
        /// Removes all components associated with the specified plugin context.
        /// </summary>
        /// <param name="pluginContext">The context of the plugin that contains the components to remove.</param>
        public static void Remove(IPluginContext pluginContext)
        {
            Dictionary.Remove(pluginContext);
        }

        /// <summary>
        /// Determines all components that match the parameters.
        /// </summary>
        /// <param name="section">The section where the component is embedded.</param>
        /// <param name="page">The page that holds the components.</param>
        /// <param name="resourceContextFilter">The context where the components exists.</param>
        /// <returns>A list of components.</returns>
        public static IEnumerable<ComponentCacheItem> CacheComponent<T>(string section, IPage page, IReadOnlyList<string> resourceContextFilter = null) where T : IControl
        {
            var list = new List<ComponentCacheItem>();
            var appID = page?.ApplicationContext?.ApplicationName?.ToLower();
            var pluginDictionary = Dictionary.ContainsKey(page.ResourceContext.PluginContext) ? Dictionary[page.ResourceContext.PluginContext] : null;

            if (pluginDictionary.ContainsKey("*"))
            {
                var dictItem = pluginDictionary["*"];
                var sectionKey = section?.ToLower();

                if (dictItem.ContainsKey(sectionKey))
                {
                    var components = dictItem[sectionKey].Where(x => x.Component.GetInterfaces()
                        .Contains(typeof(T)))
                        .Select(x => new ComponentCacheItem(x.Context, x.Component)).ToList();

                    list.AddRange(components);
                }

                if (resourceContextFilter != null)
                {
                    foreach (var context in resourceContextFilter)
                    {
                        sectionKey = string.Join(":", section?.ToLower(), context?.ToLower());

                        if (dictItem.ContainsKey(sectionKey))
                        {
                            var components = dictItem[sectionKey].Where(x => x.Component.GetInterfaces()
                            .Contains(typeof(T)))
                            .Where(x => x.Context.ApplicationID == appID)
                            .Select(x => new ComponentCacheItem(x.Context, x.Component)).ToList();

                            list.AddRange(components);

                            components = dictItem[sectionKey].Where(x => x.Component.GetInterfaces()
                            .Contains(typeof(IComponentDynamic)))
                            .Where(x => x.Context.ApplicationID == appID)
                            .Select(x => new ComponentCacheItem(x.Context, x.Component)).ToList();

                            list.AddRange(components);
                        }
                    }
                }

                if (appID == "*") return list;
            }

            if (pluginDictionary.ContainsKey(appID))
            {
                var dictItem = pluginDictionary[appID];
                var sectionKey = section?.ToLower();

                if (dictItem.ContainsKey(sectionKey))
                {
                    var components = dictItem[sectionKey].Where(x => x.Component.GetInterfaces()
                        .Contains(typeof(T)))
                        .Where(x => x.Context.ApplicationID == appID)
                        .Select(x => new ComponentCacheItem(x.Context, x.Component)).ToList();

                    list.AddRange(components);
                }

                if (resourceContextFilter != null)
                {
                    foreach (var context in resourceContextFilter)
                    {
                        sectionKey = string.Join(":", section?.ToLower(), context?.ToLower());

                        if (dictItem.ContainsKey(sectionKey))
                        {
                            var components = dictItem[sectionKey].Where(x => x.Component.GetInterfaces()
                            .Contains(typeof(T)))
                            .Where(x => x.Context.ApplicationID == appID)
                            .Select(x => new ComponentCacheItem(x.Context, x.Component)).ToList();

                            list.AddRange(components);

                            components = dictItem[sectionKey].Where(x => x.Component.GetInterfaces()
                            .Contains(typeof(IComponentDynamic)))
                            .Where(x => x.Context.ApplicationID == appID)
                            .Select(x => new ComponentCacheItem(x.Context, x.Component)).ToList();

                            list.AddRange(components);
                        }
                    }
                }
            }

            return list.Distinct();
        }
    }
}
