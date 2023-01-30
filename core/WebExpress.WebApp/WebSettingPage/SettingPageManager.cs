using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebAttribute;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebApplication;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebModule;
using WebExpress.WebPlugin;
using WebExpress.WebResource;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.WebApp.SettingPage
{
    /// <summary>
    /// Management of settings pages.
    /// </summary>
    public sealed class SettingPageManager : IComponentPlugin
    {
        /// <summary>
        /// Returns the reference to the context of the host.
        /// </summary>
        public static IHttpServerContext Context { get; private set; }

        /// <summary>
        /// Returns the directory where the components are listed.
        /// </summary>
        private static SettingPageDictionary Dictionary { get; } = new SettingPageDictionary();

        /// <summary>
        /// Constructor
        /// </summary>
        internal SettingPageManager()
        {

        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The reference to the context of the host.</param>
        public void Initialization(IHttpServerContext context)
        {
            Context = context;

            Context.Log.Info(message: I18N("webexpress.webapp:pagesettingmanager.initialization"));
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
                var module = null as IModuleContext;
                var hide = false;
                var icon = null as PropertyIcon;

                // determining Attributes
                foreach (var customAttribute in settingPageType.CustomAttributes.Where(x => x.AttributeType.GetInterfaces().Contains(typeof(IResourceAttribute))))
                {
                    if (customAttribute.AttributeType == typeof(IdAttribute))
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
                    else if (customAttribute.AttributeType == typeof(ModuleAttribute))
                    {
                        module = ModuleManager.GetModule(pluginContext, customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString());
                    }
                    else if (customAttribute.AttributeType == typeof(SettingIconAttribute))
                    {
                        var iconAttribute = customAttribute.ConstructorArguments.FirstOrDefault().Value;
                        icon = iconAttribute?.GetType() == typeof(int) ? new PropertyIcon((TypeIcon)Enum.Parse(typeof(TypeIcon), iconAttribute?.ToString())) : new PropertyIcon(new UriRelative(iconAttribute?.ToString()));
                    }
                }

                if (string.IsNullOrEmpty(id))
                {
                    Context.Log.Warning(message: I18N("webexpress.webapp:pagesettingmanager.idless", pluginContext.PluginID));
                    break;
                }

                if (module == null)
                {
                    Context.Log.Warning(message: I18N("webexpress.webapp:pagesettingmanager.moduleless", id, pluginContext.PluginID));
                    break;
                }

                var resource = ResourceManager.Resources
                    .Where
                    (
                        x => x.Context.ModuleContext.ModuleID.Equals(module.ModuleID, StringComparison.OrdinalIgnoreCase) &&
                        x.ID.Equals(id, StringComparison.OrdinalIgnoreCase)
                    )
                    .FirstOrDefault();

                if (resource == null)
                {
                    Context.Log.Warning(message: I18N("webexpress.webapp:pagesettingmanager.resourceless", id, pluginContext.PluginID));
                    break;
                }

                // Check if an optional resource
                if (resource.Optional)
                {
                    //continue;
                }

                // Create meta information of the setting page
                var page = new SettingPageDictionaryItem()
                {
                    ID = id,
                    ModuleContext = module,
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
                    I18N("webexpress.webapp:pagesettingmanager.register"),
                    "    ModuleID             = " + module?.ModuleID,
                    "    SettingContext       = " + context ?? "null",
                    "    SettingSection       = " + section.ToString(),
                    "    SettingGroup         = " + group ?? "null",
                    "    SettingPage.ID       = " + page?.ID ?? "null",
                    "    SettingPage.Resource = " + (page?.Resource.ToString()),
                    "    SettingPage.Hide     = " + (page?.Hide != null ? page?.Hide.ToString() : "null")
                };

                Context.Log.Info(string.Join(Environment.NewLine, log));
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

        }

        /// <summary>
        /// Searches for a page by its id.
        /// </summary>
        /// <param name="applicationContext">The application context.</param>
        /// <param name="moduleContext">the module context.</param>
        /// <param name="pageID">The page.</param>
        /// <returns>The page found, or null.</returns>
        /// <summary>
        public static SettingPageSearchResult FindPage(IApplicationContext applicationContext, IModuleContext moduleContext, string pageID)
        {
            return Dictionary.FindPage(applicationContext, moduleContext, pageID);
        }

        /// <summary>
        /// Returns all contexts.
        /// </summary>
        /// <param name="applicationID">The application id.</param>
        /// <returns>A listing of all contexts.</returns>
        /// <summary>
        public static SettingPageDictionaryItemContext GetContexts(string applicationID)
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
        public static SettingPageDictionaryItemSection GetSections(string applicationID, string context)
        {
            return Dictionary.GetSections(applicationID, context);
        }
    }
}