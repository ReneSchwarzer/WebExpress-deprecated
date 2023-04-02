using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebCondition;
using WebExpress.WebPage;
using WebExpress.WebPlugin;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.UI.WebFragment
{
    /// <summary>
    /// Fragment manager.
    /// </summary>
    [Id("webexpress.webui.fragmentmanager")]
    public sealed class FragmentManager : IComponentPlugin
    {
        /// <summary>
        /// Returns the reference to the context of the host.
        /// </summary>
        public IHttpServerContext HttpServerContext { get; private set; }

        /// <summary>
        /// Delivers or sets the directory where the components are listed.
        /// </summary>
        private FragmentDictionary Dictionary { get; set; } = new FragmentDictionary();

        /// <summary>
        /// Constructor
        /// </summary>
        internal FragmentManager()
        {
            ComponentManager.PluginManager.AddPlugin += (s, e) =>
            {
                //AssignToPlugin(e);
            };

            ComponentManager.PluginManager.RemovePlugin += (s, e) =>
            {
                //DetachFromPlugin(e);
            };
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The reference to the context of the host.</param>
        public void Initialization(IHttpServerContext context)
        {
            HttpServerContext = context;

            HttpServerContext.Log.Info(message: I18N("webexpress.ui:fragmentmanager.initialization"));
        }

        /// <summary>
        /// Discovers and registers entries from the specified plugin.
        /// </summary>
        /// <param name="pluginContext">A context of a plugin whose elements are to be registered.</param>
        public void Register(IPluginContext pluginContext)
        {
            // register plugin
            if (!Dictionary.ContainsKey(pluginContext))
            {
                Dictionary.Add(pluginContext, new Dictionary<string, FragmentDictionaryItem>());
            }

            var pluginDictionary = Dictionary[pluginContext];
            var assembly = pluginContext.Assembly;

            foreach (var component in assembly.GetTypes().Where(x => x.IsClass && x.IsSealed && (x.GetInterfaces().Contains(typeof(IFragment)) || x.GetInterfaces().Contains(typeof(IFragmentDynamic)))))
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
                            HttpServerContext.Log.Warning(message: I18N("webexpress.ui:fragmentmanager.wrongtype", condition.Name, typeof(ICondition).Name));
                        }
                    }
                    else if (customAttribute.AttributeType == typeof(CacheAttribute))
                    {
                        cache = true;
                    }
                }

                foreach (var customAttribute in component.CustomAttributes.Where(x => x.AttributeType.GetInterfaces().Contains(typeof(IFragmentAttribute))))
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
                        pluginDictionary.Add(appID, new FragmentDictionaryItem());
                    }

                    var dictItem = pluginDictionary[appID];

                    if (resourceContextFilter.Count > 0)
                    {
                        foreach (var context in resourceContextFilter)
                        {
                            var key = string.Join(":", section, context);

                            if (!dictItem.ContainsKey(key))
                            {
                                dictItem.Add(key, new List<FragmentItem>());
                            }

                            dictItem[key].Add(new FragmentItem()
                            {
                                Context = new FragmentContext()
                                {
                                    PluginContext = pluginContext,
                                    ApplicationID = appID,
                                    Conditions = conditions,
                                    Cache = cache,
                                    Log = HttpServerContext.Log
                                },
                                Fragment = component,
                                Order = order
                            });

                            HttpServerContext.Log.Info(message: I18N("webexpress.ui:fragmentmanager.register", component.Name, key, appID));
                        }
                    }
                    else
                    {
                        if (!dictItem.ContainsKey(section))
                        {
                            dictItem.Add(section, new List<FragmentItem>());
                        }

                        dictItem[section].Add(new FragmentItem()
                        {
                            Context = new FragmentContext()
                            {
                                PluginContext = pluginContext,
                                ApplicationID = appID,
                                Conditions = conditions,
                                Cache = cache,
                                Log = HttpServerContext.Log
                            },
                            Fragment = component,
                            Order = order
                        });

                        HttpServerContext.Log.Info(message: I18N("webexpress.ui:fragmentmanager.register", component.Name, section, appID));
                    }

                }
                else if (string.IsNullOrWhiteSpace(section))
                {
                    HttpServerContext.Log.Info(message: I18N("webexpress.ui:fragmentmanager.error.section"));
                }

            }

            foreach (var modules in pluginDictionary)
            {
                foreach (var section in modules.Value)
                {
                    section.Value.Sort(new FragmentItemComparer());
                }
            }
        }

        /// <summary>
        /// Discovers and registers entries from the specified plugin.
        /// </summary>
        /// <param name="pluginContexts">A list with plugin contexts that contain the components.</param>
        public void Register(IEnumerable<IPluginContext> pluginContexts)
        {
            foreach (var pluginContext in pluginContexts)
            {
                Register(pluginContext);
            }
        }

        /// <summary>
        /// Removes all components associated with the specified plugin context.
        /// </summary>
        /// <param name="pluginContext">The context of the plugin that contains the components to remove.</param>
        public void Remove(IPluginContext pluginContext)
        {
            Dictionary.Remove(pluginContext);
        }

        /// <summary>
        /// Determines all fragments that match the parameters.
        /// </summary>
        /// <param name="section">The section where the fragment is embedded.</param>
        /// <param name="page">The page that holds the fragments.</param>
        /// <param name="resourceContextFilter">The context where the fragments exists.</param>
        /// <returns>A list of components.</returns>
        public IEnumerable<FragmentCacheItem> CacheFragment<T>(string section, IPage page, IReadOnlyList<string> resourceContextFilter = null) where T : IControl
        {
            var list = new List<FragmentCacheItem>();
            var appID = page?.ApplicationContext?.ApplicationName?.ToLower();
            var pluginDictionary = Dictionary.ContainsKey(page.ResourceContext.PluginContext) ? Dictionary[page.ResourceContext.PluginContext] : null;

            if (pluginDictionary == null)
            {
                return list;
            }

            if (pluginDictionary.ContainsKey("*"))
            {
                var dictItem = pluginDictionary["*"];
                var sectionKey = section?.ToLower();

                if (dictItem.ContainsKey(sectionKey))
                {
                    var components = dictItem[sectionKey].Where(x => x.Fragment.GetInterfaces()
                        .Contains(typeof(T)))
                        .Select(x => new FragmentCacheItem(x.Context, x.Fragment)).ToList();

                    list.AddRange(components);
                }

                if (resourceContextFilter != null)
                {
                    foreach (var context in resourceContextFilter)
                    {
                        sectionKey = string.Join(":", section?.ToLower(), context?.ToLower());

                        if (dictItem.ContainsKey(sectionKey))
                        {
                            var components = dictItem[sectionKey].Where(x => x.Fragment.GetInterfaces()
                            .Contains(typeof(T)))
                            .Where(x => x.Context.ApplicationID == appID)
                            .Select(x => new FragmentCacheItem(x.Context, x.Fragment)).ToList();

                            list.AddRange(components);

                            components = dictItem[sectionKey].Where(x => x.Fragment.GetInterfaces()
                            .Contains(typeof(IFragmentDynamic)))
                            .Where(x => x.Context.ApplicationID == appID)
                            .Select(x => new FragmentCacheItem(x.Context, x.Fragment)).ToList();

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
                    var components = dictItem[sectionKey].Where(x => x.Fragment.GetInterfaces()
                        .Contains(typeof(T)))
                        .Where(x => x.Context.ApplicationID == appID)
                        .Select(x => new FragmentCacheItem(x.Context, x.Fragment)).ToList();

                    list.AddRange(components);
                }

                if (resourceContextFilter != null)
                {
                    foreach (var context in resourceContextFilter)
                    {
                        sectionKey = string.Join(":", section?.ToLower(), context?.ToLower());

                        if (dictItem.ContainsKey(sectionKey))
                        {
                            var components = dictItem[sectionKey].Where(x => x.Fragment.GetInterfaces()
                            .Contains(typeof(T)))
                            .Where(x => x.Context.ApplicationID == appID)
                            .Select(x => new FragmentCacheItem(x.Context, x.Fragment)).ToList();

                            list.AddRange(components);

                            components = dictItem[sectionKey].Where(x => x.Fragment.GetInterfaces()
                            .Contains(typeof(IFragmentDynamic)))
                            .Where(x => x.Context.ApplicationID == appID)
                            .Select(x => new FragmentCacheItem(x.Context, x.Fragment)).ToList();

                            list.AddRange(components);
                        }
                    }
                }
            }

            return list.Distinct();
        }

        /// <summary>
        /// Returns the fragment items.
        /// </summary>
        /// <param name="pluginContext">The context of the plugin.</param>
        /// <returns>A list with the fragment items.</returns>
        private IEnumerable<FragmentItem> GetFragmentItems(IPluginContext pluginContext)
        {
            if (!Dictionary.ContainsKey(pluginContext))
            {
                return new List<FragmentItem>();
            }

            return Dictionary[pluginContext].Values
                .SelectMany(x => x.Values)
                .SelectMany(x => x);
        }

        /// <summary>
        /// Information about the component is collected and prepared for output in the log.
        /// </summary>
        /// <param name="pluginContext">The context of the plugin.</param>
        /// <param name="output">A list of log entries.</param>
        /// <param name="deep">The shaft deep.</param>
        public void PrepareForLog(IPluginContext pluginContext, IList<string> output, int deep)
        {
            foreach (var fragmentItem in GetFragmentItems(pluginContext))
            {
                //output.Add
                //(
                //    string.Empty.PadRight(deep) +
                //    InternationalizationManager.I18N
                //    (
                //        "webexpress:schedulermanager.job",
                //        fragmentItem.,
                //        fragmentItem.
                //    )
                //);
            }
        }
    }
}
