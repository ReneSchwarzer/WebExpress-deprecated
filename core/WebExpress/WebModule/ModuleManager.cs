using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebExpress.Uri;
using WebExpress.WebApplication;
using WebExpress.WebAttribute;
using WebExpress.WebPlugin;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.WebModule
{
    /// <summary>
    /// The module manager manages the WebExpress modules.
    /// </summary>
    public static class ModuleManager
    {
        /// <summary>
        /// Returns or sets the reference to the context of the host.
        /// </summary>
        private static IHttpServerContext Context { get; set; }

        /// <summary>
        /// Returns the directory where the modules are listed.
        /// </summary>
        private static ModuleDictionary Dictionary { get; } = new ModuleDictionary();

        /// <summary>
        /// Delivers all stored modules.
        /// </summary>
        public static IEnumerable<IModuleContext> Modules => Dictionary.Values.SelectMany(x => x.Values).Select(x => x.Context).ToList();

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The reference to the context of the host.</param>
        internal static void Initialization(IHttpServerContext context)
        {
            Context = context;

            Context.Log.Info(message: I18N("webexpress:modulemanager.initialization"));
        }

        /// <summary>
        /// Adds module entries from the specified plugin.
        /// </summary>
        /// <param name="pluginContexts">A list of contexts of plugins containing the modules to be registered.</param>
        /// <returns>The contexts of the modules created.</returns>
        public static IEnumerable<IModuleContext> Register(IEnumerable<IPluginContext> pluginContexts)
        {
            var list = new List<IModuleContext>();

            foreach (var pluginContext in pluginContexts)
            {
                var assembly = pluginContext.Assembly;

                foreach (var type in assembly.GetExportedTypes().Where(x => x.IsClass && x.IsSealed && x.GetInterface(typeof(IModule).Name) != null))
                {
                    var id = type.Name?.ToLower();
                    var name = type.Name;
                    var icon = string.Empty;
                    var description = string.Empty;
                    var contextPath = string.Empty;
                    var assetPath = string.Empty;
                    var dataPath = string.Empty;
                    var applicationIDs = new List<string>();

                    foreach (var customAttribute in type.CustomAttributes.Where(x => x.AttributeType.GetInterfaces().Contains(typeof(IModuleAttribute))))
                    {
                        if (customAttribute.AttributeType == typeof(IdAttribute))
                        {
                            id = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString().ToLower();
                        }

                        if (customAttribute.AttributeType == typeof(NameAttribute))
                        {
                            name = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString();
                        }

                        if (customAttribute.AttributeType == typeof(IconAttribute))
                        {
                            icon = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString();
                        }

                        if (customAttribute.AttributeType == typeof(DescriptionAttribute))
                        {
                            description = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString();
                        }

                        if (customAttribute.AttributeType == typeof(ContextPathAttribute))
                        {
                            contextPath = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString();
                        }

                        if (customAttribute.AttributeType == typeof(AssetPathAttribute))
                        {
                            assetPath = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString();
                        }

                        if (customAttribute.AttributeType == typeof(DataPathAttribute))
                        {
                            dataPath = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString();
                        }

                        if (customAttribute.AttributeType == typeof(ApplicationAttribute))
                        {
                            applicationIDs.Add(customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString().ToLower());
                        }
                    }

                    if (!applicationIDs.Any())
                    {
                        // no application specified
                        Context.Log.Warning(message: I18N("webexpress:modulemanager.applicationless", id));
                    }

                    // create context
                    var context = new ModuleContext()
                    {
                        Assembly = assembly,
                        PluginContext = pluginContext,
                        Applications = applicationIDs,
                        ModuleID = id,
                        ModuleName = name,
                        Description = description,
                        Icon = UriRelative.Combine(icon),
                        AssetPath = assetPath,
                        ContextPath = UriRelative.Combine(contextPath),
                        DataPath = dataPath,
                        Log = Context.Log
                    };

                    // create module
                    var module = (IModule)type.Assembly.CreateInstance(type.FullName);

                    if (!Dictionary.ContainsKey(pluginContext))
                    {
                        Dictionary.Add(pluginContext, new Dictionary<string, ModuleItem>());
                    }

                    var item = Dictionary[pluginContext];

                    if (!item.ContainsKey(id))
                    {
                        item.Add(id, new ModuleItem()
                        {
                            Context = context,
                            Module = module
                        });

                        Context.Log.Info(message: I18N("webexpress:modulemanager.register", id, string.Join(", ", applicationIDs)));
                    }
                    else
                    {
                        Context.Log.Warning(message: I18N("webexpress:modulemanager.duplicate", id, string.Join(", ", applicationIDs)));
                    }

                    list.Add(context);
                }
            }

            return list;
        }

        /// <summary>
        /// Determines the module for a given application context and module id.
        /// </summary>
        /// <param name="applicationContext">The context of the application.</param>
        /// <param name="moduleID">The modul id.</param>
        /// <returns>The context of the module or null.</returns>
        public static IModuleContext GetModule(IApplicationContext applicationContext, string moduleID)
        {
            var item = Dictionary.Values
                .SelectMany(x => x.Values)
                .Where(x => x.Context.LinkedWithApplication(applicationContext) && x.Context.ModuleID.Equals(moduleID, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            if (item != null)
            {
                return item.Context;
            }

            return null;
        }

        /// <summary>
        /// Determines the module for a given plugin context and module id.
        /// </summary>
        /// <param name="pluginContext">The context of the plugin.</param>
        /// <param name="moduleID">The modul id.</param>
        /// <returns>The context of the module or null.</returns>
        public static IModuleContext GetModule(IPluginContext pluginContext, string moduleID)
        {
            return GetModules(pluginContext)
                .Where(x => x.Context.ModuleID.Equals(moduleID, StringComparison.OrdinalIgnoreCase))
                .Select(x => x.Context)
                .FirstOrDefault();
        }

        /// <summary>
        /// Determines the context of the module for a given application and module id.
        /// </summary>
        /// <param name="applicationID">The application id.</param>
        /// <param name="moduleID">The modul id.</param>
        /// <returns>The context of the module or null.</returns>
        public static IModuleContext GetModule(string applicationID, string moduleID)
        {
            if (string.IsNullOrWhiteSpace(applicationID) || string.IsNullOrWhiteSpace(moduleID))
            {
                return null;
            }

            var application = ApplicationManager.GetApplcation(applicationID);

            return GetModule(application, moduleID);
        }

        /// <summary>
        /// Determines the modules for a given plugin ID.
        /// </summary>
        /// <param name="pluginContext">The context of the plugin.</param>
        /// <returns>An enumeration of the module contexts for the given plugin.</returns>
        internal static IEnumerable<ModuleItem> GetModules(IPluginContext pluginContext)
        {
            if (pluginContext == null)
            {
                return Enumerable.Empty<ModuleItem>();
            }

            if (Dictionary.ContainsKey(pluginContext))
            {
                return Dictionary[pluginContext].Values;
            }

            return Enumerable.Empty<ModuleItem>();
        }

        /// <summary>
        /// Boots the modules of a plugin.
        /// </summary>
        /// <param name="pluginContext">The context of the plugin containing the modules.</param>
        internal static void Boot(IPluginContext pluginContext)
        {
            foreach (var moduleItem in GetModules(pluginContext))
            {
                // initialize module
                moduleItem.Module.Initialization(moduleItem.Context);
                Context.Log.Info(message: I18N("webexpress:modulemanager.module.initialization", string.Join(", ", moduleItem.Context.Applications), moduleItem.Context.PluginContext.PluginID));

                // execute modules concurrently
                Task.Run(() =>
                {
                    Context.Log.Info(message: I18N("webexpress:modulemanager.module.processing.start", string.Join(", ", moduleItem.Context.Applications), moduleItem.Context.PluginContext.PluginID));

                    moduleItem.Module.Run();

                    Context.Log.Info(message: I18N("webexpress:modulemanager.module.processing.end", string.Join(", ", moduleItem.Context.Applications), moduleItem.Context.PluginContext.PluginID));
                });
            }
        }

        /// <summary>
        /// Terminate modules of a plugin.
        /// </summary>
        /// <param name="pluginContext">The context of the plugin containing the modules.</param>
        public static void ShutDown(IPluginContext pluginContext)
        {

        }
    }
}
