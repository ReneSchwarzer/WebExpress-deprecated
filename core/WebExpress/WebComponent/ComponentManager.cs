using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.WebApplication;
using WebExpress.WebJob;
using WebExpress.WebModule;
using WebExpress.WebPackage;
using WebExpress.WebPlugin;
using WebExpress.WebResource;
using WebExpress.WebSitemap;

namespace WebExpress.WebComponent
{
    /// <summary>
    /// Central management of components.
    /// </summary>
    public static class ComponentManager
    {
        /// <summary>
        /// Returns the reference to the context of the host.
        /// </summary>
        public static IHttpServerContext Context { get; private set; }

        /// <summary>
        /// Returns the directory where the components are listed.
        /// </summary>
        private static ComponentDictionary Dictionary { get; } = new ComponentDictionary();

        /// <summary>
        /// Returns all registered managers.
        /// </summary>
        public static IEnumerable<IComponent> Components => Dictionary.Values;

        /// <summary>
        /// Returns all registered systemically important managers.
        /// </summary>
        public static IEnumerable<IComponent> SystemComponents => Dictionary.Values.Where(x => x is ISystemComponent);

        /// <summary>
        /// Returns the package manager.
        /// </summary>
        /// <returns>The instance of the package manager or null.</returns>
        public static PackageManager PackageManager
        {
            get
            {
                if (Dictionary.ContainsKey(typeof(PackageManager)))
                {
                    return Dictionary[typeof(PackageManager)] as PackageManager;
                }

                return null;
            }
        }

        /// <summary>
        /// Returns the plugin manager.
        /// </summary>
        /// <returns>The instance of the plugin manager or null.</returns>
        public static PluginManager PluginManager
        {
            get
            {
                if (Dictionary.ContainsKey(typeof(PluginManager)))
                {
                    return Dictionary[typeof(PluginManager)] as PluginManager;
                }

                return null;
            }
        }

        /// <summary>
        /// Returns the application manager.
        /// </summary>
        /// <returns>The instance of the application manager or null.</returns>
        public static ApplicationManager ApplicationManager
        {
            get
            {
                if (Dictionary.ContainsKey(typeof(ApplicationManager)))
                {
                    return Dictionary[typeof(ApplicationManager)] as ApplicationManager;
                }

                return null;
            }
        }

        /// <summary>
        /// Returns the module manager.
        /// </summary>
        /// <returns>The instance of the module manager or null.</returns>
        public static ModuleManager ModuleManager
        {
            get
            {
                if (Dictionary.ContainsKey(typeof(ModuleManager)))
                {
                    return Dictionary[typeof(ModuleManager)] as ModuleManager;
                }

                return null;
            }
        }

        /// <summary>
        /// Returns the schedule manager.
        /// </summary>
        /// <returns>The instance of the schedule manager or null.</returns>
        public static ScheduleManager ScheduleManager
        {
            get
            {
                if (Dictionary.ContainsKey(typeof(ScheduleManager)))
                {
                    return Dictionary[typeof(ScheduleManager)] as ScheduleManager;
                }

                return null;
            }
        }

        /// <summary>
        /// Returns the response manager.
        /// </summary>
        /// <returns>The instance of the response manager or null.</returns>
        public static ResponseManager ResponseManager
        {
            get
            {
                if (Dictionary.ContainsKey(typeof(ResponseManager)))
                {
                    return Dictionary[typeof(ResponseManager)] as ResponseManager;
                }

                return null;
            }
        }

        /// <summary>
        /// Returns the resource manager.
        /// </summary>
        /// <returns>The instance of the resource manager or null.</returns>
        public static ResourceManager ResourceManager
        {
            get
            {
                if (Dictionary.ContainsKey(typeof(ResourceManager)))
                {
                    return Dictionary[typeof(ResourceManager)] as ResourceManager;
                }

                return null;
            }
        }

        /// <summary>
        /// Returns the sitemap manager.
        /// </summary>
        /// <returns>The instance of the sitemap manager or null.</returns>
        public static SitemapManager SitemapManager
        {
            get
            {
                if (Dictionary.ContainsKey(typeof(SitemapManager)))
                {
                    return Dictionary[typeof(SitemapManager)] as SitemapManager;
                }

                return null;
            }
        }

        /// <summary>
        /// Returns the internationalization manager.
        /// </summary>
        /// <returns>The instance of the internationalization manager or null.</returns>
        public static InternationalizationManager InternationalizationManager
        {
            get
            {
                if (Dictionary.ContainsKey(typeof(InternationalizationManager)))
                {
                    return Dictionary[typeof(InternationalizationManager)] as InternationalizationManager;
                }

                return null;
            }
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The reference to the context of the host.</param>
        internal static void Initialization(IHttpServerContext context)
        {
            Context = context;

            InternationalizationManager.Register(typeof(HttpServer).Assembly, "webexpress");

            Context.Log.Info(message: InternationalizationManager.I18N("webexpress:componentmanager.initialization"));

            // order is relevant
            Register(typeof(PackageManager));
            Register(typeof(PluginManager));
            Register(typeof(InternationalizationManager));
            Register(typeof(ApplicationManager));
            Register(typeof(ModuleManager));
            Register(typeof(ResourceManager));
            Register(typeof(ResponseManager));
            Register(typeof(ScheduleManager));
            Register(typeof(SitemapManager));
        }

        /// <summary>
        /// Creates, registers, and initializes a manager.
        /// </summary>
        /// <param name="managerType">The manager class.</param>
        /// <returns>The instance of the create and initialized manager.</returns>
        public static IComponent Register(Type managerType)
        {
            if (managerType == null)
            {
                return null;
            }
            else if (!managerType.GetInterfaces().Where(x => x == typeof(IComponent)).Any())
            {
                Context.Log.Warning(message: InternationalizationManager.I18N("webexpress:componentmanager.wrongtype", managerType?.FullName, typeof(IComponent).FullName));

                return null;
            }

            try
            {

                var flags = BindingFlags.NonPublic | BindingFlags.Instance;
                var manager = managerType?.Assembly.CreateInstance
                (
                    managerType?.FullName,
                    false,
                    flags,
                    null,
                    null,
                    null,
                    null
                ) as IComponent;

                if (!Dictionary.ContainsKey(managerType))
                {
                    Dictionary.Add(managerType, manager);

                    manager.Initialization(Context);

                    Context.Log.Info(message: InternationalizationManager.I18N("webexpress:componentmanager.register", managerType?.FullName));

                    return manager;
                }

            }
            catch(Exception ex)
            {
                Context.Log.Exception(ex);
                
                return null;
            }

            return Dictionary[managerType];
        }

        /// <summary>
        /// Returns a manager.
        /// </summary>
        /// <typeparam name="T">The manager class.</typeparam>
        /// <returns>The instance of the manager or null.</returns>
        public static T GetManager<T>() where T : IComponent
        {
            //if (Dictionary.ContainsKey(typeof(T)))
            //{
            return (T)Dictionary[typeof(T)];
            //}

            //return null;
        }

        /// <summary>
        /// Discovers and registers the components from the specified plugins.
        /// </summary>
        /// <param name="pluginContexts">A list with plugin contexts that contain the components.</param>
        public static void Register(IEnumerable<IPluginContext> pluginContexts)
        {
            foreach (var manager in Dictionary.Values.Where(x => x is IComponentPlugin).Select(x => x as IComponentPlugin))
            {
                manager.Register(pluginContexts);
            }
        }

        /// <summary>
        /// Starts the component manager.
        /// </summary>
        internal static void Execute()
        {
            Context.Log.Info(message: InternationalizationManager.I18N("webexpress:componentmanager.execute"));

            PackageManager.Execute();
            ScheduleManager.Execute();
        }

        /// <summary>
        /// Shutting down the component manager.
        /// </summary>
        public static void ShutDown()
        {
            Context.Log.Info(message: InternationalizationManager.I18N("webexpress:componentmanager.shutdown"));
        }
    }
}
