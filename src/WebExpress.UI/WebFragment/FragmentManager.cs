using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Internationalization;
using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;
using WebExpress.WebApplication;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebCondition;
using WebExpress.WebModule;
using WebExpress.WebPage;
using WebExpress.WebPlugin;

namespace WebExpress.UI.WebFragment
{
    /// <summary>
    /// Fragment manager.
    /// </summary>
    public sealed class FragmentManager : IComponentPlugin
    {
        /// <summary>
        /// An event that fires when an fragment is added.
        /// </summary>
        public event EventHandler<IFragmentContext> AddFragment;

        /// <summary>
        /// An event that fires when an fragment is removed.
        /// </summary>
        public event EventHandler<IFragmentContext> RemoveFragment;

        /// <summary>
        /// Returns the reference to the context of the host.
        /// </summary>
        public IHttpServerContext HttpServerContext { get; private set; }

        /// <summary>
        /// Returns or sets the directory where the components are listed.
        /// </summary>
        private FragmentDictionary Dictionary { get; set; } = new FragmentDictionary();

        /// <summary>
        /// Constructor
        /// </summary>
        internal FragmentManager()
        {
            ComponentManager.PluginManager.AddPlugin += (s, pluginContext) =>
            {
                Register(pluginContext);
            };

            ComponentManager.PluginManager.RemovePlugin += (s, pluginContext) =>
            {
                Remove(pluginContext);
            };

            ComponentManager.ModuleManager.AddModule += (sender, moduleContext) =>
            {
                AssignToModule(moduleContext);
            };

            ComponentManager.ModuleManager.RemoveModule += (sender, moduleContext) =>
            {
                DetachFromModule(moduleContext);
            };
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The reference to the context of the host.</param>
        public void Initialization(IHttpServerContext context)
        {
            HttpServerContext = context;

            HttpServerContext.Log.Debug
            (
                InternationalizationManager.I18N("webexpress.ui:fragmentmanager.initialization")
            );
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
                Dictionary.Add(pluginContext, new FragmentDictionaryItem());
            }

            var pluginDictionary = Dictionary[pluginContext];
            var assembly = pluginContext.Assembly;

            foreach (var fragment in assembly.GetTypes().Where
                (
                    x => x.IsClass &&
                    x.IsSealed &&
                    (
                        x.GetInterfaces().Contains(typeof(IFragment)) ||
                        x.GetInterfaces().Contains(typeof(IFragmentDynamic))
                    )
                ))
            {
                var moduleId = string.Empty;
                var scopes = new List<string>();
                var section = string.Empty;
                var conditions = new List<ICondition>();
                var cache = false;
                var order = 0;

                // determining attributes
                foreach (var customAttribute in fragment.CustomAttributes.Where
                (
                    x => x.AttributeType.GetInterfaces()
                            .Contains(typeof(IResourceAttribute))
                ))
                {
                    if (customAttribute.AttributeType.Name == typeof(WebExModuleAttribute<>).Name && customAttribute.AttributeType.Namespace == typeof(WebExModuleAttribute<>).Namespace)
                    {
                        moduleId = customAttribute.AttributeType.GenericTypeArguments.FirstOrDefault()?.FullName?.ToLower();
                    }
                    else if (customAttribute.AttributeType.Name == typeof(WebExScopeAttribute<>).Name && customAttribute.AttributeType.Namespace == typeof(WebExScopeAttribute<>).Namespace)
                    {
                        scopes.Add(customAttribute.AttributeType.GenericTypeArguments.FirstOrDefault()?.FullName?.ToLower());
                    }
                    else if (customAttribute.AttributeType.Name == typeof(WebExConditionAttribute<>).Name && customAttribute.AttributeType.Namespace == typeof(WebExConditionAttribute<>).Namespace)
                    {
                        var condition = customAttribute.AttributeType.GenericTypeArguments.FirstOrDefault();
                        conditions.Add(Activator.CreateInstance(condition) as ICondition);
                    }
                    else if (customAttribute.AttributeType == typeof(WebExCacheAttribute))
                    {
                        cache = true;
                    }
                }

                foreach (var customAttribute in fragment.CustomAttributes.Where
                (
                    x => x.AttributeType.GetInterfaces().Contains(typeof(IFragmentAttribute))
                ))
                {
                    if (customAttribute.AttributeType == typeof(WebExSectionAttribute))
                    {
                        section = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString().ToLower();
                    }
                    else if (customAttribute.AttributeType == typeof(WebExOrderAttribute))
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

                // check module
                if (string.IsNullOrWhiteSpace(moduleId))
                {
                    HttpServerContext.Log.Warning(InternationalizationManager.I18N
                    (
                        "webexpress.ui:fragmentmanager.moduleless",
                        fragment.Name,
                        pluginContext.PluginId
                    ));

                    continue;
                }

                // check section
                if (string.IsNullOrWhiteSpace(section))
                {
                    HttpServerContext.Log.Warning(InternationalizationManager.I18N
                    (
                        "webexpress.ui:fragmentmanager.error.section"
                    ));

                    continue;
                }

                // register fragment
                foreach (var context in scopes.Any() ? scopes : new List<string>(new[] { "" }))
                {
                    var key = string.Join(":", section, context);

                    if (!pluginDictionary.ContainsKey(key))
                    {
                        pluginDictionary.Add(key, new List<FragmentItem>());
                    }

                    var dictItem = pluginDictionary[key];

                    var fragmentItem = new FragmentItem()
                    {
                        PluginContext = pluginContext,
                        ModuleId = moduleId,
                        FragmentClass = fragment,
                        Order = order,
                        Cache = cache,
                        Conditions = conditions,
                        Section = section,
                        Scopes = scopes
                    };

                    dictItem.Add(fragmentItem);

                    fragmentItem.AddFragment += (s, e) =>
                    {
                        OnAddFragment(e);
                    };

                    fragmentItem.RemoveFragment += (s, e) =>
                    {
                        OnRemoveFragment(e);
                    };

                    HttpServerContext.Log.Debug(InternationalizationManager.I18N
                    (
                        "webexpress.ui:fragmentmanager.register",
                        fragment.Name,
                        section,
                        moduleId
                    ));
                }

                // assign the fragments to existing modules.
                foreach (var moduleContext in ComponentManager.ModuleManager.GetModules(pluginContext, moduleId))
                {
                    AssignToModule(moduleContext);
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
        /// Assign existing resources to the module.
        /// </summary>
        /// <param name="moduleContext">The context of the module.</param>
        private void AssignToModule(IModuleContext moduleContext)
        {
            foreach (var resourceItem in Dictionary.Values
                .SelectMany(x => x.Values)
                .SelectMany(x => x)
                .Where(x => x.ModuleId.Equals(moduleContext?.ModuleId, StringComparison.OrdinalIgnoreCase))
                .Where(x => !x.IsAssociatedWithModule(moduleContext)))
            {
                resourceItem.AddModule(moduleContext);
            }
        }

        /// <summary>
        /// Remove an existing modules to the application.
        /// </summary>
        /// <param name="moduleContext">The context of the module.</param>
        private void DetachFromModule(IModuleContext moduleContext)
        {
            foreach (var resourceItem in Dictionary.Values
                .SelectMany(x => x.Values)
                .SelectMany(x => x)
                .Where(x => !x.IsAssociatedWithModule(moduleContext)))
            {
                resourceItem.DetachModule(moduleContext);
            }
        }

        /// <summary>
        /// Raises the AddFragment event.
        /// </summary>
        /// <param name="fragmentContext">The fragment context.</param>
        private void OnAddFragment(IFragmentContext fragmentContext)
        {
            AddFragment?.Invoke(this, fragmentContext);
        }

        /// <summary>
        /// Raises the RemoveFragment event.
        /// </summary>
        /// <param name="fragmentContext">The fragment context.</param>
        private void OnRemoveFragment(IFragmentContext fragmentContext)
        {
            RemoveFragment?.Invoke(this, fragmentContext);
        }

        /// <summary>
        /// Returns all fragment contexts that belong to a given section.
        /// </summary>
        /// <param name="section">The section where the fragment is embedded.</param>
        /// <returns>An enumeration of the filtered fragment contexts.</returns>
        public IEnumerable<IFragmentContext> GetFragmentContexts(string section)
        {
            return GetFragmentItems(section)
                .SelectMany(x => x.FragmentContexts);
        }

        /// <summary>
        /// Returns all fragment contexts that belong to a given application.
        /// </summary>
        /// <param name="section">The section where the fragment is embedded.</param>
        /// <param name="applicationContext">The allpication context.</param>
        /// <returns>An enumeration of the filtered fragment contexts.</returns>
        public IEnumerable<IFragmentContext> GetFragmentContexts(string section, IApplicationContext applicationContext)
        {
            return GetFragmentItems(section)
                .SelectMany(x => x.FragmentContexts)
                .Where(x => x.ApplicationContext == applicationContext);
        }

        /// <summary>
        /// Returns all fragment contexts that belong to a given application.
        /// </summary>
        /// <param name="section">The section where the fragment is embedded.</param>
        /// <returns>An enumeration of the filtered fragment contexts.</returns>
        private IEnumerable<FragmentItem> GetFragmentItems(string section)
        {
            return Dictionary.Values
                .Where(x => x.ContainsKey(section?.ToLower()))
                .SelectMany(x => x[section?.ToLower()])
                .Select(x => x);
        }

        /// <summary>
        /// Determines all fragments that match the parameters.
        /// </summary>
        /// <param name="section">The section where the fragment is embedded.</param>
        /// <param name="page">The page that holds the fragments.</param>
        /// <returns>A list of fragments.</returns>
        public IEnumerable<FragmentCacheItem> GetCacheableFragments<T>
        (
            string section,
            IPage page
        ) where T : IControl
        {
            return GetCacheableFragments<T>(section, page, page?.ResourceContext?.Scopes);
        }

        /// <summary>
        /// Determines all fragments that match the parameters.
        /// </summary>
        /// <param name="section">The section where the fragment is embedded.</param>
        /// <param name="page">The page that holds the fragments.</param>
        /// <param name="scopes">The scopes where the fragments exists.</param>
        /// <returns>A list of fragments.</returns>
        public IEnumerable<FragmentCacheItem> GetCacheableFragments<T>
        (
            string section,
            IPage page,
            IEnumerable<string> scopes
        ) where T : IControl
        {
            var applicationContext = page?.ApplicationContext;
            scopes ??= Enumerable.Empty<string>();

            var fragmentItems = GetFragmentItems($"{section}:")
                .Union(scopes.SelectMany(x => GetFragmentItems
                (
                    string.Join(":", section?.ToLower(), x?.ToLower())
                )));

            var fragmentCacheItems = fragmentItems.Where
               (
                   x => x.FragmentClass.GetInterfaces().Contains(typeof(T)) ||
                   x.FragmentClass.GetInterfaces().Contains(typeof(IFragmentDynamic))
               )
               .Select(x => new
               {
                   x.FragmentClass,
                   FragmentContext = x.FragmentContexts
                       .Where(y => y.ModuleContext?.ApplicationContext == applicationContext)
                       .FirstOrDefault()
               })
               .Where(x => x.FragmentContext != null)
               .Select(x => new FragmentCacheItem(x.FragmentContext, x.FragmentClass));

            return fragmentCacheItems;
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
                return Enumerable.Empty<FragmentItem>();
            }

            return Dictionary[pluginContext].Values
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
            output.Add
            (
                string.Empty.PadRight(deep) +
                InternationalizationManager.I18N("webexpress.ui:fragmentmanager.titel")
            );

            foreach (var fragmentItem in GetFragmentItems(pluginContext))
            {
                output.Add
                (
                    string.Empty.PadRight(deep + 2) +
                    InternationalizationManager.I18N
                    (
                        "webexpress.ui:fragmentmanager.fragment",
                        fragmentItem.FragmentClass.Name
                    )
                );
            }
        }
    }
}
