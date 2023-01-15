using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebExpress.Uri;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebPlugin;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.WebApplication
{
    /// <summary>
    /// Management of WebExpress applications.
    /// </summary>
    public class ApplicationManager : IComponentPlugin, IExecutableElements, ISystemComponent
    {
        /// <summary>
        /// Returns or sets the reference to the context of the host.
        /// </summary>
        public IHttpServerContext Context { get; private set; }

        /// <summary>
        /// Returns or sets the directory where the applications are listed.
        /// </summary>
        private ApplicationDictionary Dictionary { get; } = new ApplicationDictionary();

        /// <summary>
        /// Returns the stored applications.
        /// </summary>
        public IEnumerable<IApplicationContext> Applications => Dictionary.Values.SelectMany(x => x.Values).Select(x => x.Context);

        /// <summary>
        /// Constructor
        /// </summary>
        internal ApplicationManager()
        {

        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The reference to the context of the host.</param>
        public void Initialization(IHttpServerContext context)
        {
            Context = context;

            Context.Log.Info(message: I18N("webexpress:applicationmanager.initialization"));
        }

        /// <summary>
        /// Discovers and registers applications from the specified plugin.
        /// </summary>
        /// <param name="pluginContext">A context of a plugin whose applications are to be registered.</param>
        public void Register(IPluginContext pluginContext)
        {
            if (!Dictionary.ContainsKey(pluginContext))
            {
                Dictionary.Add(pluginContext, new Dictionary<string, ApplicationItem>());
            }

            var assembly = pluginContext.Assembly;
            var pluginDict = Dictionary[pluginContext];

            foreach (var type in assembly.GetExportedTypes().Where(x => x.IsClass && x.IsSealed && x.GetInterface(typeof(IApplication).Name) != null))
            {
                var id = type.Name?.ToLower();
                var name = type.Name;
                var icon = string.Empty;
                var description = string.Empty;
                var contextPath = string.Empty;
                var assetPath = Path.DirectorySeparatorChar.ToString();
                var dataPath = Path.DirectorySeparatorChar.ToString();
                var options = new List<string>();

                // Attribute ermitteln
                foreach (var customAttribute in type.CustomAttributes.Where(x => x.AttributeType.GetInterfaces().Contains(typeof(IApplicationAttribute))))
                {
                    if (customAttribute.AttributeType == typeof(IdAttribute))
                    {
                        id = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString().ToLower();
                    }
                    else if (customAttribute.AttributeType == typeof(NameAttribute))
                    {
                        name = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString();
                    }
                    else if (customAttribute.AttributeType == typeof(IconAttribute))
                    {
                        icon = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString();
                    }
                    else if (customAttribute.AttributeType == typeof(DescriptionAttribute))
                    {
                        description = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString();
                    }
                    else if (customAttribute.AttributeType == typeof(ContextPathAttribute))
                    {
                        contextPath = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString();
                    }
                    else if (customAttribute.AttributeType == typeof(AssetPathAttribute))
                    {
                        assetPath = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString();
                    }
                    else if (customAttribute.AttributeType == typeof(DataPathAttribute))
                    {
                        dataPath = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString();
                    }
                    else if (customAttribute.AttributeType == typeof(OptionAttribute))
                    {
                        options.Add(customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString().ToLower());
                    }
                }

                // Creating application context
                var context = new ApplicationContext()
                {
                    Assembly = assembly,
                    PluginContext = pluginContext,
                    ApplicationID = id,
                    ApplicationName = name,
                    Description = description,
                    Options = options,
                    AssetPath = Path.GetFullPath(Context.AssetPath + Path.Combine(assetPath)),
                    DataPath = Path.GetFullPath(Context.DataPath + dataPath),
                    ContextPath = UriRelative.Combine(Context.ContextPath, contextPath),
                    Icon = new UriResource(Context.ContextPath, contextPath, icon),
                    Log = Context.Log
                };

                // Create application
                var application = (IApplication)type.Assembly.CreateInstance(type.FullName);

                if (!pluginDict.ContainsKey(id))
                {
                    pluginDict.Add(id, new ApplicationItem()
                    {
                        Context = context,
                        Application = application
                    });

                    Context.Log.Info(message: I18N("webexpress:applicationmanager.register", id));
                }
                else
                {
                    Context.Log.Warning(message: I18N("webexpress:applicationmanager.duplicate", id));
                }
            }
        }

        /// <summary>
        /// Discovers and registers applications from the specified plugin.
        /// </summary>
        /// <param name="pluginContexts">A list with plugin contexts that contain the applications.</param>
        public void Register(IEnumerable<IPluginContext> pluginContexts)
        {
            foreach (var pluginContext in pluginContexts)
            {
                Register(pluginContext);
            }
        }

        /// <summary>
        /// Determines the application contexts for a given application id.
        /// </summary>
        /// <param name="applicationID">The application id.</param>
        /// <returns>The context of the application or null.</returns>
        public IApplicationContext GetApplcation(string applicationID)
        {
            if (string.IsNullOrWhiteSpace(applicationID)) return null;

            var items = Dictionary.Values
                .Where(x => x.ContainsKey(applicationID.ToLower()))
                .Select(x => x[applicationID.ToLower()])
                .FirstOrDefault();

            if (items != null)
            {
                return items.Context;
            }

            return null;
        }

        /// <summary>
        /// Determines the application contexts for the given application ids.
        /// </summary>
        /// <param name="applicationIDs">The applications ids. Can contain regular expressions or * for all.</param>
        /// <returns>The contexts of the applications as an enumeration.</returns>
        public IEnumerable<IApplicationContext> GetApplcations(IEnumerable<string> applicationIDs)
        {
            var list = new List<IApplicationContext>();

            foreach (var applicationID in applicationIDs)
            {
                if (applicationID == "*")
                {
                    list.AddRange(Applications);
                }
                else
                {
                    list.AddRange
                    (
                        Applications.Where
                        (
                            x =>
                            x.ApplicationID.Equals(applicationID, StringComparison.OrdinalIgnoreCase) ||
                            Regex.Match(x.ApplicationID, applicationID).Success
                        )
                    );
                }
            }

            return list.Distinct();
        }

        /// <summary>
        /// Determines the application contexts for the given plugin.
        /// </summary>
        /// <param name="pluginContext">The context of the plugin.</param>
        /// <returns>The contexts of the applications as an enumeration.</returns>
        public IEnumerable<IApplicationContext> GetApplcations(IPluginContext pluginContext)
        {
            if (!Dictionary.ContainsKey(pluginContext))
            {
                return new List<IApplicationContext>();
            }

            return Dictionary[pluginContext].Values.Select(x => x.Context);
        }

        /// <summary>
        /// Boots the applications.
        /// </summary>
        /// <param name="pluginContext">The context of the plugin that contains the applications.</param>
        public void Boot(IPluginContext pluginContext)
        {
            if (!Dictionary.ContainsKey(pluginContext))
            {
                Context.Log.Warning(message: I18N("webexpress:applicationmanager.application.boot.notfound", pluginContext.PluginID));

                return;
            }

            foreach (var applicationItem in Dictionary[pluginContext].Values)
            {
                // Initialize application
                applicationItem.Application.Initialization(applicationItem.Context);
                Context.Log.Info(message: I18N("webexpress:applicationmanager.application.initialization", applicationItem.Context.ApplicationID));

                // Run the application concurrently
                Task.Run(() =>
                {
                    Context.Log.Info(message: I18N("webexpress:applicationmanager.application.processing.start", applicationItem.Context.ApplicationID));

                    applicationItem.Application.Run();

                    Context.Log.Info(message: I18N("webexpress:applicationmanager.application.processing.end", applicationItem.Context.ApplicationID));
                });
            }
        }

        /// <summary>
        /// Shutting down applications.
        /// </summary>
        ///  <param name="pluginContext">The context of the plugin that contains the applications.</param>
        public void ShutDown(IPluginContext pluginContext)
        {

        }

        /// <summary>
        /// Removes all applications associated with the specified plugin context.
        /// </summary>
        /// <param name="pluginContext">The context of the plugin that contains the applications to remove.</param>
        public void Remove(IPluginContext pluginContext)
        {

        }
    }
}
