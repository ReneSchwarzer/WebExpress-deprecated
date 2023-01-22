using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Uri;
using WebExpress.WebApplication;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebModule;
using WebExpress.WebPage;
using WebExpress.WebPlugin;
using WebExpress.WebResource;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.Message
{
    /// <summary>
    /// Management of status pages
    /// </summary>
    public class ResponseManager : IComponentPlugin, ISystemComponent
    {
        /// <summary>
        /// Returns or sets the reference to the context of the host
        /// </summary>
        public IHttpServerContext Context { get; private set; }

        /// <summary>
        /// Returns the directory where the status pages are listed
        /// </summary>
        private static ResponseDictionary Dictionary { get; } = new ResponseDictionary();

        /// <summary>
        /// Constructor
        /// </summary>
        internal ResponseManager()
        {

        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The reference to the context of the host.</param>
        public void Initialization(IHttpServerContext context)
        {
            Context = context;

            Context.Log.Info(message: I18N("webexpress:responsemanager.initialization"));
        }

        /// <summary>
        /// Discovers and registers status pages from the specified plugin.
        /// </summary>
        /// <param name="pluginContext">A context of a plugin whose status pages are to be registered.</param>
        public void Register(IPluginContext pluginContext)
        {
            var assembly = pluginContext?.Assembly;

            foreach (var resource in assembly.GetTypes().Where(x => x.IsClass == true && x.IsSealed && x.GetInterface(typeof(IPageStatus).Name) != null))
            {
                var statusCode = -1;
                var moduleID = string.Empty;

                foreach (var customAttribute in resource.CustomAttributes.Where(x => x.AttributeType.GetInterfaces().Contains(typeof(IApplicationAttribute))))
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
                        Context.Log.Info(message: I18N("webexpress:responsemanager.register"), args: new object[] { statusCode, moduleID, resource.Name });
                    }
                    else
                    {
                        Context.Log.Info(message: I18N("webexpress:responsemanager.duplicat"), args: new object[] { statusCode, moduleID, resource.Name });
                    }
                }
                else
                {
                    Context.Log.Info(message: I18N("webexpress:responsemanager.statuscode"), args: new object[] { moduleID, resource.Name });
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
        /// Creates a status page
        /// </summary>
        /// <param name="massage">The status message.</param>
        /// <param name="status">The status code.</param>
        /// <param name="applicationContext">The context of the applicationwhere the status pages are located or null for an undefined page (may be from another module) that matches the status code.</param>
        /// <param name="moduleContext">The module context where the status pages are located or null for an undefined page (may be from another module) that matches the status code.</param>
        /// <param name="uri">The uri.</param>
        /// <returns>The created status page or null</returns>
        public IPageStatus Create(string massage, int status, IApplicationContext applicationContext, IModuleContext moduleContext, IUri uri)
        {
            try
            {
                if (moduleContext == null)
                {
                    moduleContext = GetDefaultModule(status, uri?.ToString(), moduleContext);
                }

                if (moduleContext != null && Dictionary.ContainsKey(moduleContext.PluginContext))
                {
                    var item = Dictionary[moduleContext.PluginContext];
                    if (item.ContainsKey(status))
                    {
                        var type = item[status];
                        var statusPage = moduleContext.Assembly.CreateInstance(type?.FullName) as IPageStatus;
                        statusPage.StatusMessage = massage;
                        statusPage.StatusCode = status;

                        if (statusPage is Resource resource)
                        {
                            resource.ApplicationContext = applicationContext;
                            resource.ModuleContext = moduleContext;
                        }

                        return statusPage;
                    }
                }
            }
            catch
            {

            }

            return null;
        }

        /// <summary>
        /// Returns the first available module that has a status page.
        /// </summary>
        /// <param name="status">The status code.</param>
        /// <param name="uri">The uri.</param>
        /// <param name="module">The module context where the status pages are located or null for an undefined page (may be from another module) that matches the status code.</param>
        /// <returns>The first status page found to the given states or null</returns>
        public IModuleContext GetDefaultModule(int status, string uri, IModuleContext module = null)
        {
            var applications = ApplicationManager.Applications.Where(x => uri != null && uri.StartsWith(x.ContextPath.ToString()));

            // 1. preferred status page
            var modules = Dictionary.Where(x => x.Value.ContainsKey(status))
                .SelectMany(x => ComponentManager.ModuleManager.GetModules(x.Key))
                .Where(x => x.Context.GetApplicationContexts().Intersect(applications).Any())
                .Where(x => x.Context.ModuleID.Equals(module?.ModuleID, StringComparison.OrdinalIgnoreCase));

            var mod = modules.FirstOrDefault();

            if (mod != null)
            {
                return mod.Context;
            }

            // 2. if not available, alternatively available status page
            modules = Dictionary.Where(x => x.Value.ContainsKey(status))
                .SelectMany(x => ComponentManager.ModuleManager.GetModules(x.Key))
                .Where(x => x.Context.GetApplicationContexts().Intersect(applications).Any());

            mod = modules.FirstOrDefault();

            return mod?.Context;
        }

        /// <summary>
        /// Removes all status pages associated with the specified plugin context.
        /// </summary>
        /// <param name="pluginContext">The context of the plugin that contains the status pages to remove.</param>
        public void Remove(IPluginContext pluginContext)
        {

        }
    }
}
