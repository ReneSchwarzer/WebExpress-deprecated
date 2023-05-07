using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebAttribute;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebApplication;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebModule;
using WebExpress.WebPlugin;
using WebExpress.WebResource;

namespace WebExpress.WebApp.SettingPage
{
    /// <summary>
    /// Management of settings pages.
    /// </summary>
    [WebExID("webexpress.webapp.settingpagemanager")]
    public sealed class SettingPageManager : IComponentPlugin
    {
        /// <summary>
        /// An event that fires when an setting page is added.
        /// </summary>
        public event EventHandler<IResourceContext> AddSettingPage;

        /// <summary>
        /// An event that fires when an setting page is removed.
        /// </summary>
        public event EventHandler<IResourceContext> RemoveSettingPage;

        /// <summary>
        /// Returns the reference to the context of the host.
        /// </summary>
        public IHttpServerContext HttpServerContext { get; private set; }

        /// <summary>
        /// Returns the directory where the components are listed.
        /// </summary>
        private SettingPageDictionary Dictionary { get; } = new SettingPageDictionary();

        /// <summary>
        /// Constructor
        /// </summary>
        internal SettingPageManager()
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

            HttpServerContext.Log.Info(InternationalizationManager.I18N("webexpress.webapp:pagesettingmanager.initialization"));
        }

        /// <summary>
        /// Discovers and registers entries from the specified plugin.
        /// </summary>
        /// <param name="pluginContext">A context of a plugin whose elements are to be registered.</param>
        public void Register(IPluginContext pluginContext)
        {
            foreach (var settingPageType in pluginContext.Assembly.GetTypes()
                    .Where(x => x.IsClass && x.IsSealed && (x.GetInterfaces().Contains(typeof(IPageSetting)))))
            {
                var id = settingPageType.Name?.ToLower();
                var context = null as string;
                var group = null as string;
                var section = SettingSection.Primary;
                var moduleID = null as string;
                var hide = false;
                var icon = null as PropertyIcon;

                // determining Attributes
                foreach (var customAttribute in settingPageType.CustomAttributes
                    .Where(x => x.AttributeType.GetInterfaces().Contains(typeof(IResourceAttribute))))
                {
                    if (customAttribute.AttributeType == typeof(WebExIDAttribute))
                    {
                        id = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString();
                    }
                    else if (customAttribute.AttributeType == typeof(SettingContextAttribute))
                    {
                        context = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString();
                    }
                    else if (customAttribute.AttributeType == typeof(SettingGroupAttribute))
                    {
                        group = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString();
                    }
                    else if (customAttribute.AttributeType == typeof(SettingSectionAttribute))
                    {
                        section = (SettingSection)Enum.Parse(typeof(SettingSection), customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString());
                    }
                    else if (customAttribute.AttributeType == typeof(SettingHideAttribute))
                    {
                        hide = true;
                    }
                    else if (customAttribute.AttributeType == typeof(WebExModuleAttribute))
                    {
                        moduleID = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString();
                    }
                    else if (customAttribute.AttributeType == typeof(SettingIconAttribute))
                    {
                        var iconAttribute = customAttribute.ConstructorArguments.FirstOrDefault().Value;
                        icon = iconAttribute?.GetType() == typeof(int) ?
                            new PropertyIcon((TypeIcon)Enum.Parse(typeof(TypeIcon), iconAttribute?.ToString())) :
                            new PropertyIcon(iconAttribute?.ToString());
                    }
                }

                if (string.IsNullOrEmpty(id))
                {
                    HttpServerContext.Log.Warning(InternationalizationManager.I18N
                    (
                        "webexpress.webapp:pagesettingmanager.idless",
                        pluginContext.PluginID
                    ));

                    continue;
                }

                if (string.IsNullOrEmpty(moduleID))
                {
                    HttpServerContext.Log.Warning(InternationalizationManager.I18N
                    (
                        "webexpress.webapp:pagesettingmanager.moduleless",
                        id,
                        pluginContext.PluginID
                    ));

                    continue;
                }

                var resource = ComponentManager.ResourceManager.GetResorces(pluginContext)
                    .Where
                    (
                        x => x.ModuleContext.ModuleID.Equals(moduleID, StringComparison.OrdinalIgnoreCase) &&
                        x.ResourceID.Equals(id, StringComparison.OrdinalIgnoreCase)
                    )
                    .FirstOrDefault();

                if (resource == null)
                {
                    HttpServerContext.Log.Warning(InternationalizationManager.I18N
                    (
                        "webexpress.webapp:pagesettingmanager.resourceless",
                        id,
                        pluginContext.PluginID
                    ));

                    continue;
                }

                //// Check if an optional resource
                //if (resource.Optional)
                //{
                //    //continue;
                //}

                // Create meta information of the setting page
                var page = new SettingPageDictionaryItem()
                {
                    ID = id,
                    ModuleContext = resource?.ModuleContext,
                    Resource = resource,
                    Icon = icon,
                    Hide = hide,
                    Context = context,
                    Section = section,
                    Group = group
                };

                // Insert the settings page into the dictionary
                Dictionary.AddPage(pluginContext, page);

                // Logging
                var log = new List<string>
                {
                        InternationalizationManager.I18N("webexpress.webapp:pagesettingmanager.register"),
                        "    ModuleID             = " + moduleID,
                        "    SettingContext       = " + context ?? "null",
                        "    SettingSection       = " + section.ToString(),
                        "    SettingGroup         = " + group ?? "null",
                        "    SettingPage.ID       = " + page?.ID ?? "null",
                        "    SettingPage.Resource = " + (page?.Resource.ToString()),
                        "    SettingPage.Hide     = " + (page?.Hide != null ? page?.Hide.ToString() : "null")
                };

                HttpServerContext.Log.Debug(string.Join(Environment.NewLine, log));

                // assign the fragments to existing modules.
                foreach (var moduleContext in ComponentManager.ModuleManager.GetModules(pluginContext, moduleID))
                {
                    AssignToModule(moduleContext);
                }
            }
        }

        /// <summary>
        /// Adds the component-key-value pairs from the specified plugin.
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
        /// Removes all elemets associated with the specified plugin context.
        /// </summary>
        /// <param name="pluginContext">The context of the plugin that contains the elemets to remove.</param>
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
            //foreach (var resourceItem in Dictionary.Values
            //    .SelectMany(x => x.Values)
            //    .SelectMany(x => x)
            //    .Where(x => x.ModuleID.Equals(moduleContext?.ModuleID, StringComparison.OrdinalIgnoreCase))
            //    .Where(x => !x.IsAssociatedWithModule(moduleContext)))
            //{
            //    resourceItem.AddModule(moduleContext);
            //}
        }

        /// <summary>
        /// Remove an existing modules to the application.
        /// </summary>
        /// <param name="moduleContext">The context of the module.</param>
        private void DetachFromModule(IModuleContext moduleContext)
        {
            //foreach (var resourceItem in Dictionary.Values
            //    .SelectMany(x => x.Values)
            //    .SelectMany(x => x)
            //    .Where(x => !x.IsAssociatedWithModule(moduleContext)))
            //{
            //    resourceItem.DetachModule(moduleContext);
            //}
        }

        /// <summary>
        /// Raises the AddSettingPage event.
        /// </summary>
        /// <param name="resourceContext">The resource context.</param>
        private void OnAddSettingPage(IResourceContext resourceContext)
        {
            AddSettingPage?.Invoke(this, resourceContext);
        }

        /// <summary>
        /// Raises the RemoveSettingPage event.
        /// </summary>
        /// <param name="resourceContext">The resource context.</param>
        private void OnRemoveSettingPage(IResourceContext resourceContext)
        {
            RemoveSettingPage?.Invoke(this, resourceContext);
        }

        /// <summary>
        /// Searches for a page by its id.
        /// </summary>
        /// <param name="applicationContext">The application context.</param>
        /// <param name="moduleContext">the module context.</param>
        /// <param name="pageID">The page.</param>
        /// <returns>The page found, or null.</returns>
        /// <summary>
        public SettingPageSearchResult FindPage(IApplicationContext applicationContext, IModuleContext moduleContext, string pageID)
        {
            return Dictionary.FindPage(applicationContext, moduleContext, pageID);
        }

        /// <summary>
        /// Returns all contexts.
        /// </summary>
        /// <param name="applicationID">The application id.</param>
        /// <returns>A listing of all contexts.</returns>
        /// <summary>
        public SettingPageDictionaryItemContext GetContexts(string applicationID)
        {
            return Dictionary.GetContexts(applicationID);
        }

        /// <summary>
        /// Returns all sections that have the same setting context.
        /// </summary>
        /// <param name="applicationID">The application id.</param>
        /// <param name="context">The context.</param>
        /// <returns>A listing of all sections of the same context.</returns>
        /// <summary>
        public SettingPageDictionaryItemSection GetSections(string applicationID, string context)
        {
            return Dictionary.GetSections(applicationID, context);
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
                InternationalizationManager.I18N("webexpress.ui:settingpagemanager.titel")
            );

            //foreach (var fragmentItem in GetFragmentItems(pluginContext))
            //{
            //    output.Add
            //    (
            //        string.Empty.PadRight(deep + 2) +
            //        InternationalizationManager.I18N
            //        (
            //            "webexpress.ui:settingpagemanager.settingpage",
            //            fragmentItem.FragmentClass.Name
            //        )
            //    );
            //}
        }
    }
}