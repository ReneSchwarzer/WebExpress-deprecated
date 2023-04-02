using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebExpress.Internationalization;
using WebExpress.WebApplication;
using WebExpress.WebAttribute;
using WebExpress.WebJob;
using WebExpress.WebModule;
using WebExpress.WebPackage;
using WebExpress.WebPlugin;
using WebExpress.WebResource;
using WebExpress.WebResponse;
using WebExpress.WebSitemap;

namespace WebExpress.WebComponent
{
    /// <summary>
    /// Central management of components.
    /// </summary>
    public static class ComponentManager
    {
        /// <summary>
        /// An event that fires when an component is added.
        /// </summary>
        public static event EventHandler<IComponentPlugin> AddComponent;

        /// <summary>
        /// An event that fires when an component is removed.
        /// </summary>
        public static event EventHandler<IComponentPlugin> RemoveComponent;

        /// <summary>
        /// Returns the reference to the context of the host.
        /// </summary>
        public static IHttpServerContext HttpServerContext { get; private set; }

        /// <summary>
        /// Returns the directory where the components are listed.
        /// </summary>
        private static ComponentDictionary Dictionary { get; } = new ComponentDictionary();

        /// <summary>
        /// Returns all registered components.
        /// </summary>
        public static IEnumerable<IComponent> Components => new IComponent[]
            {
                PackageManager,
                PluginManager,
                ApplicationManager,
                ModuleManager,
                ScheduleManager,
                ResponseManager,
                SitemapManager,
                InternationalizationManager
            }.Concat(Dictionary.Values.SelectMany(x => x).Select(x => x.ComponentInstance));

        /// <summary>
        /// Returns the package manager.
        /// </summary>
        /// <returns>The instance of the package manager or null.</returns>
        public static PackageManager PackageManager { get; private set; }

        /// <summary>
        /// Returns the plugin manager.
        /// </summary>
        /// <returns>The instance of the plugin manager or null.</returns>
        public static PluginManager PluginManager { get; private set; }

        /// <summary>
        /// Returns the application manager.
        /// </summary>
        /// <returns>The instance of the application manager or null.</returns>
        public static ApplicationManager ApplicationManager { get; private set; }

        /// <summary>
        /// Returns the module manager.
        /// </summary>
        /// <returns>The instance of the module manager or null.</returns>
        public static ModuleManager ModuleManager { get; private set; }

        /// <summary>
        /// Returns the schedule manager.
        /// </summary>
        /// <returns>The instance of the schedule manager or null.</returns>
        public static ScheduleManager ScheduleManager { get; private set; }

        /// <summary>
        /// Returns the response manager.
        /// </summary>
        /// <returns>The instance of the response manager or null.</returns>
        public static ResponseManager ResponseManager { get; private set; }

        /// <summary>
        /// Returns the resource manager.
        /// </summary>
        /// <returns>The instance of the resource manager or null.</returns>
        public static ResourceManager ResourceManager { get; private set; }

        /// <summary>
        /// Returns the sitemap manager.
        /// </summary>
        /// <returns>The instance of the sitemap manager or null.</returns>
        public static SitemapManager SitemapManager { get; private set; }

        /// <summary>
        /// Returns the internationalization manager.
        /// </summary>
        /// <returns>The instance of the internationalization manager or null.</returns>
        public static InternationalizationManager InternationalizationManager { get; private set; }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="httpServerContext">The reference to the context of the host.</param>
        internal static void Initialization(IHttpServerContext httpServerContext)
        {
            HttpServerContext = httpServerContext;

            InternationalizationManager.Register(typeof(HttpServer).Assembly, "webexpress");

            HttpServerContext.Log.Debug
            (
                InternationalizationManager.I18N("webexpress:componentmanager.initialization")
            );

            // order is relevant
            PackageManager = CreateInstance(typeof(PackageManager)) as PackageManager;
            PluginManager = CreateInstance(typeof(PluginManager)) as PluginManager;
            InternationalizationManager = CreateInstance(typeof(InternationalizationManager)) as InternationalizationManager;
            ApplicationManager = CreateInstance(typeof(ApplicationManager)) as ApplicationManager;
            ModuleManager = CreateInstance(typeof(ModuleManager)) as ModuleManager;
            ResourceManager = CreateInstance(typeof(ResourceManager)) as ResourceManager;
            ResponseManager = CreateInstance(typeof(ResponseManager)) as ResponseManager;
            ScheduleManager = CreateInstance(typeof(ScheduleManager)) as ScheduleManager;
            SitemapManager = CreateInstance(typeof(SitemapManager)) as SitemapManager;

            ComponentManager.PluginManager.AddPlugin += (sender, pluginContext) =>
            {
                Register(pluginContext);
            };

            ComponentManager.PluginManager.RemovePlugin += (sender, pluginContext) =>
            {
                Remove(pluginContext);
            };
        }

        /// <summary>
        /// Creates and initializes a component.
        /// </summary>
        /// <param name="componentType">The component class.</param>
        /// <returns>The instance of the create and initialized component.</returns>
        private static IComponent CreateInstance(Type componentType)
        {
            if (componentType == null)
            {
                return null;
            }
            else if (!componentType.GetInterfaces().Where(x => x == typeof(IComponent)).Any())
            {
                HttpServerContext.Log.Warning
                (
                    InternationalizationManager.I18N
                    (
                        "webexpress:componentmanager.wrongtype",
                        componentType?.FullName, typeof(IComponent).FullName
                    )
                );

                return null;
            }

            try
            {
                var flags = BindingFlags.NonPublic | BindingFlags.Instance;
                var component = componentType?.Assembly.CreateInstance
                (
                    componentType?.FullName,
                    false,
                    flags,
                    null,
                    null,
                    null,
                    null
                ) as IComponent;

                component.Initialization(HttpServerContext);

                return component;
            }
            catch (Exception ex)
            {
                HttpServerContext.Log.Exception(ex);
            }

            return null;
        }

        /// <summary>
        /// Returns a component based on its id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The instance of the component or null.</returns>
        public static IComponentPlugin GetComponent(string id)
        {
            return Dictionary.Values
                .SelectMany(x => x)
                .Where(x => x.ComponentID.Equals(id, StringComparison.OrdinalIgnoreCase))
                .Select(x => x.ComponentInstance)
                .FirstOrDefault();
        }

        /// <summary>
        /// Returns a component based on its type.
        /// </summary>
        /// <typeparam name="T">The component class.</typeparam>
        /// <returns>The instance of the component or null.</returns>
        public static T GetComponent<T>() where T : IComponentPlugin
        {
            return (T)Dictionary.Values
                .SelectMany(x => x)
                .Where(x => x.ComponentClass == typeof(T))
                .Select(x => x.ComponentInstance)
                .FirstOrDefault();
        }

        /// <summary>
        /// Discovers and registers the components from the specified plugin.
        /// </summary>
        /// <param name="pluginContexts">A plugin context that contain the components.</param>
        public static void Register(IPluginContext pluginContext)
        {
            // the plugin has already been registered
            if (Dictionary.ContainsKey(pluginContext))
            {
                return;
            }

            var assembly = pluginContext.Assembly;

            Dictionary.Add(pluginContext, new List<ComponentItem>());
            var componentItems = Dictionary[pluginContext];

            foreach (var type in assembly.GetExportedTypes().Where(x => x.IsClass && x.IsSealed && x.GetInterface(typeof(IComponentPlugin).Name) != null))
            {
                var id = type.Name?.ToLower();

                // determining attributes
                foreach (var customAttribute in type.CustomAttributes.Where(x => x.AttributeType.GetInterfaces().Contains(typeof(IApplicationAttribute))))
                {
                    if (customAttribute.AttributeType == typeof(IdAttribute))
                    {
                        id = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString().ToLower();
                    }
                }

                var componentInstance = CreateInstance(type) as IComponentPlugin;

                if (!componentItems.Where(x => x.ComponentID.Equals(id, StringComparison.OrdinalIgnoreCase)).Any())
                {
                    componentItems.Add(new ComponentItem()
                    {
                        ComponentClass = type,
                        ComponentID = id,
                        ComponentInstance = componentInstance
                    });

                    HttpServerContext.Log.Debug
                    (
                        InternationalizationManager.I18N("webexpress:componentmanager.register", id)
                    );

                    // raises the AddApplication event
                    OnAddComponent(componentInstance);
                }
                else
                {
                    HttpServerContext.Log.Warning
                    (
                        InternationalizationManager.I18N("webexpress:componentmanager.duplicate", id)
                    );
                }
            }
        }

        /// <summary>
        /// Discovers and registers the components from the specified plugins.
        /// </summary>
        /// <param name="pluginContexts">A list with plugin contexts that contain the components.</param>
        public static void Register(IEnumerable<IPluginContext> pluginContexts)
        {
            foreach (var pluinContext in pluginContexts)
            {
                Register(pluinContext);
            }
        }

        /// <summary>
        /// Starts the component.
        /// </summary>
        internal static void Execute()
        {
            HttpServerContext.Log.Debug
            (
                InternationalizationManager.I18N("webexpress:componentmanager.execute")
            );

            PackageManager.Execute();
            ScheduleManager.Execute();
        }

        /// <summary>
        /// Shutting down the component.
        /// </summary>
        public static void ShutDown()
        {
            HttpServerContext.Log.Debug
            (
                InternationalizationManager.I18N("webexpress:componentmanager.shutdown")
            );
        }

        /// <summary>
        /// Removes all components associated with the specified plugin context.
        /// </summary>
        /// <param name="pluginContext">The context of the plugin that contains the applications to remove.</param>
        public static void Remove(IPluginContext pluginContext)
        {
            if (Dictionary.ContainsKey(pluginContext))
            {
                return;
            }

            var componentItems = Dictionary[pluginContext];

            if (!componentItems.Any())
            {
                return;
            }

            foreach (var componentItem in componentItems)
            {
                OnRemoveComponent(componentItem.ComponentInstance);

                HttpServerContext.Log.Debug
                (
                    InternationalizationManager.I18N("webexpress:componentmanager.remove")
                );
            }

            Dictionary.Remove(pluginContext);
        }

        /// <summary>
        /// Raises the AddComponent event.
        /// </summary>
        /// <param name="component">The component.</param>
        private static void OnAddComponent(IComponentPlugin component)
        {
            AddComponent?.Invoke(null, component);
        }

        /// <summary>
        /// Raises the RemoveComponent event.
        /// </summary>
        /// <param name="component">The component.</param>
        private static void OnRemoveComponent(IComponentPlugin component)
        {
            RemoveComponent?.Invoke(null, component);
        }

        /// <summary>
        /// Output of the components to the log.
        /// </summary>
        public static void LogStatus()
        {
            using var frame = new LogFrameSimple(HttpServerContext.Log);
            var output = new List<string>();

            output.Add
            (
                InternationalizationManager.I18N("webexpress:componentmanager.component")
            );

            foreach (var pluginContext in PluginManager.Plugins)
            {
                output.Add
                (
                   string.Empty.PadRight(2) +
                   InternationalizationManager.I18N("webexpress:pluginmanager.plugin", pluginContext.PluginID)
                );

                ApplicationManager.PrepareForLog(pluginContext, output, 4);
                ModuleManager.PrepareForLog(pluginContext, output, 4);
                ResourceManager.PrepareForLog(pluginContext, output, 4);
                ResponseManager.PrepareForLog(pluginContext, output, 4);
                ScheduleManager.PrepareForLog(pluginContext, output, 4);
            }

            HttpServerContext.Log.Info(string.Join(Environment.NewLine, output));
        }
    }
}
