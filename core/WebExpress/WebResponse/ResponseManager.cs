using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Internationalization;
using WebExpress.WebApplication;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebModule;
using WebExpress.WebPlugin;
using WebExpress.WebResource;

namespace WebExpress.WebResponse
{
    /// <summary>
    /// Management of status pages
    /// </summary>
    public class ResponseManager : IComponentPlugin, ISystemComponent
    {
        /// <summary>
        /// An event that fires when an status page is added.
        /// </summary>
        public event EventHandler<IResourceContext> AddStatusPage;

        /// <summary>
        /// An event that fires when an status page is removed.
        /// </summary>
        public event EventHandler<IResourceContext> RemoveStatusPage;

        /// <summary>
        /// Returns or sets the reference to the context of the host
        /// </summary>
        public IHttpServerContext HttpServerContext { get; private set; }

        /// <summary>
        /// Returns the directory where the status pages are listed
        /// </summary>
        private ResponseDictionary Dictionary { get; } = new ResponseDictionary();

        /// <summary>
        /// Constructor
        /// </summary>
        internal ResponseManager()
        {
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
        /// Initialization
        /// </summary>
        /// <param name="context">The reference to the context of the host.</param>
        public void Initialization(IHttpServerContext context)
        {
            HttpServerContext = context;

            HttpServerContext.Log.Debug
            (
                InternationalizationManager.I18N("webexpress:responsemanager.initialization")
            );
        }

        /// <summary>
        /// Discovers and registers status pages from the specified plugin.
        /// </summary>
        /// <param name="pluginContext">A context of a plugin whose status pages are to be registered.</param>
        public void Register(IPluginContext pluginContext)
        {
            var assembly = pluginContext?.Assembly;

            foreach (var resource in assembly.GetTypes()
                .Where(x => x.IsClass == true && x.IsSealed)
                .Where(x => x.GetInterface(typeof(IStatusPage).Name) != null))
            {
                var statusCode = -1;
                var moduleID = string.Empty;

                foreach (var customAttribute in resource.CustomAttributes
                    .Where(x => x.AttributeType.GetInterfaces().Contains(typeof(IApplicationAttribute))))
                {
                    if (customAttribute.AttributeType == typeof(StatusCodeAttribute))
                    {
                        statusCode = Convert.ToInt32(customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString());
                    }

                    if (customAttribute.AttributeType == typeof(ModuleAttribute))
                    {
                        moduleID = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString().ToLower();
                    }
                }

                if (statusCode > 0)
                {
                    if (!Dictionary.ContainsKey(pluginContext))
                    {
                        Dictionary.Add(pluginContext, new Dictionary<int, Type>());
                    }

                    var item = Dictionary[pluginContext];
                    if (!item.ContainsKey(statusCode))
                    {
                        item.Add(statusCode, resource);
                        HttpServerContext.Log.Debug
                        (
                            InternationalizationManager.I18N
                            (
                                "webexpress:responsemanager.register",
                                statusCode,
                                moduleID,
                                resource.Name
                            )
                        );
                    }
                    else
                    {
                        HttpServerContext.Log.Debug
                        (
                            InternationalizationManager.I18N
                            (
                                "webexpress:responsemanager.duplicat",
                                statusCode,
                                moduleID,
                                resource.Name
                            )
                        );
                    }
                }
                else
                {
                    HttpServerContext.Log.Debug
                    (
                        InternationalizationManager.I18N
                        (
                            "webexpress:responsemanager.statuscode",
                            moduleID,
                            resource.Name
                        )
                    );
                }
            }
        }

        /// <summary>
        /// Discovers and registers entries from the specified plugin.
        /// </summary>
        /// <param name="pluginContexts">A list with plugin contexts that contain the status pages.</param>
        public void Register(IEnumerable<IPluginContext> pluginContexts)
        {
            foreach (var pluginContext in pluginContexts)
            {
                Register(pluginContext);
            }
        }

        /// <summary>
        /// Returns the status codes for a given plugin.
        /// </summary>
        /// <param name="pluginContext">The context of the plugin.</param>
        /// <returns>An enumeration of the status codes for the given plugin.</returns>
        internal IEnumerable<int> GetStatusCodes(IPluginContext pluginContext)
        {
            if (pluginContext == null)
            {
                return Enumerable.Empty<int>();
            }

            if (Dictionary.ContainsKey(pluginContext))
            {
                return Dictionary[pluginContext].Keys;
            }

            return Enumerable.Empty<int>();
        }

        /// <summary>
        /// Returns the class for an status page.
        /// </summary>
        /// <param name="status">The status code.</param>
        /// <param name="pluginContext">The plugin context where the status pages are located.</param>
        /// <returns>The first status page found to the given states or null</returns>
        private Type GetStatusPage(int status, IPluginContext pluginContext)
        {
            if (!Dictionary.ContainsKey(pluginContext))
            {
                return null;
            }

            if (!Dictionary[pluginContext].ContainsKey(status))
            {
                return null;
            }

            return Dictionary[pluginContext][status];
        }

        /// <summary>
        /// Creates a status page.
        /// </summary>
        /// <param name="massage">The status message.</param>
        /// <param name="status">The status code.</param>
        /// <param name="applicationContext">The context of the applicationwhere the status pages are located or null for an undefined page (may be from another module) that matches the status code.</param>
        /// <param name="moduleContext">The module context where the status pages are located or null for an undefined page (may be from another module) that matches the status code.</param>
        /// <returns>The created status page or null</returns>
        public IStatusPage CreateStatusPage(string massage, int status, IApplicationContext applicationContext, IModuleContext moduleContext)
        {
            var type = GetStatusPage(status, applicationContext, moduleContext);

            if (type == null)
            {
                return null;
            }

            var statusPage = type.Assembly.CreateInstance(type?.FullName) as IStatusPage;
            statusPage.StatusMessage = massage;
            statusPage.StatusCode = status;

            if (statusPage is Resource resource)
            {
                resource.ApplicationContext = applicationContext;
                resource.ModuleContext = moduleContext;
            }

            return statusPage;
        }

        /// <summary>
        /// Returns the first available status page.
        /// 1. Search in the plugin of the module of the accessed resource.
        /// 2. Search in the plugin of the application of the accessed resource.
        /// 3. Use the status pages from the plugin 'webexpress.webapp'.
        /// 4. Use the system status pages.
        /// </summary>
        /// <param name="status">The status code.</param>
        /// <param name="applicationContext">The context of the applicationwhere the status pages are located or null for an undefined page (may be from another module) that matches the status code.</param>
        /// <param name="moduleContext">The module context where the status pages are located or null for an undefined page (may be from another module) that matches the status code.</param>
        /// <returns>The first status page found to the given states or null</returns>
        private Type GetStatusPage(int status, IApplicationContext applicationContext, IModuleContext moduleContext)
        {
            var type = null as Type;

            // 1. Search in the plugin of the module of the accessed resource.
            if (moduleContext != null)
            {
                type = GetStatusPage(status, moduleContext.PluginContext);

                if (type != null)
                {
                    return type;
                }
            }

            // 2. Search in the plugin of the application of the accessed resource.
            if (applicationContext != null)
            {
                type = GetStatusPage(status, applicationContext.PluginContext);

                if (type != null)
                {
                    return type;
                }
            }

            // 3.Use the status pages from the plugin 'webexpress.webapp'.
            var wexExModuleContext = ComponentManager.PluginManager.GetPlugin("webexpress.webapp");
            if (wexExModuleContext != null)
            {
                type = GetStatusPage(status, wexExModuleContext);

                if (type != null)
                {
                    return type;
                }
            }

            // 4. Use the system status pages.
            return null;
        }

        /// <summary>
        /// Removes all status pages associated with the specified plugin context.
        /// </summary>
        /// <param name="pluginContext">The context of the plugin that contains the status pages to remove.</param>
        public void Remove(IPluginContext pluginContext)
        {

        }

        /// <summary>
        /// Raises the AddStatusPage event.
        /// </summary>
        /// <param name="statusPage">The status page.</param>
        private void OnAddStatusPage(IResourceContext statusPage)
        {
            AddStatusPage?.Invoke(this, statusPage);
        }

        /// <summary>
        /// Raises the RemoveComponent event.
        /// </summary>
        /// <param name="statusPage">The status page.</param>
        private void OnRemoveStatusPage(IResourceContext statusPage)
        {
            RemoveStatusPage?.Invoke(this, statusPage);
        }

        /// <summary>
        /// Information about the component is collected and prepared for output in the log.
        /// </summary>
        /// <param name="pluginContext">The context of the plugin.</param>
        /// <param name="output">A list of log entries.</param>
        /// <param name="deep">The shaft deep.</param>
        public void PrepareForLog(IPluginContext pluginContext, IList<string> output, int deep)
        {
            foreach (var statusCode in GetStatusCodes(pluginContext))
            {
                output.Add
                (
                    string.Empty.PadRight(4) +
                    InternationalizationManager.I18N
                    (
                        "webexpress:responsemanager.statuspage",
                        statusCode
                    )
                );
            }
        }
    }
}
