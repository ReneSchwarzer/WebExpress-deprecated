using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebExpress.Uri;
using WebExpress.WebApplication;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebModule;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.WebPlugin
{
    /// <summary>
    /// The plugin manager manages the WebExpress plugins.
    /// </summary>
    public class PluginManager : IComponent, ISystemComponent
    {
        /// <summary>
        /// Returns or sets the reference to the context of the host.
        /// </summary>
        public IHttpServerContext Context { get; private set; }

        /// <summary>
        /// Returns the directory where the plugins are listed.
        /// </summary>
        private PluginDictionary Dictionary { get; } = new PluginDictionary();

        /// <summary>
        /// Returns all plugins.
        /// </summary>
        public ICollection<IPluginContext> Plugins => Dictionary.Values.Select(x => x.Context).ToList();

        /// <summary>
        /// Constructor
        /// </summary>
        internal PluginManager()
        {

        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The reference to the context of the host.</param>
        public void Initialization(IHttpServerContext context)
        {
            Context = context;

            Context.Log.Info(message: I18N("webexpress:pluginmanager.initialization"));
        }

        /// <summary>
        /// Loads and registers the plugins that are static (i.e. located in the application's folder).
        /// </summary>
        /// <returns>A list of registered plugins.</returns>
        internal IEnumerable<IPluginContext> Register()
        {
            var list = new List<IPluginContext>();
            var path = Environment.CurrentDirectory;
            var assemblies = new List<Assembly>();

            // create plugins
            foreach (var assemblyFile in Directory.EnumerateFiles(path, "*.dll", SearchOption.TopDirectoryOnly))
            {
                try
                {
                    var assembly = Assembly.LoadFrom(assemblyFile);
                    if (assembly != null)
                    {
                        assemblies.Add(assembly);
                        Context.Log.Info(message: I18N("webexpress:pluginmanager.load"), args: new object[] { assembly.GetName().Name, assembly.GetName().Version.ToString() });
                    }
                }
                catch (BadImageFormatException)
                {

                }
            }

            // register plugin
            foreach (var assembly in assemblies)
            {
                list.AddRange(Register(assembly));
            }

            return list;
        }

        /// <summary>
        /// Loads and registers the plugins from a path.
        /// </summary>
        /// <param name="path">The directory where the plugins are located.</param>
        /// <returns>A list of registered plugins.</returns>
        internal IEnumerable<IPluginContext> Register(string path)
        {
            var list = new List<IPluginContext>();
            var assemblies = new List<Assembly>();

            if (!Directory.Exists(path))
            {
                return list;
            }

            // create plugins
            foreach (var assemblyFile in Directory.EnumerateFiles(path, "*.dll", SearchOption.AllDirectories))
            {
                try
                {
                    var assembly = Assembly.LoadFrom(assemblyFile);
                    if (assembly != null)
                    {
                        assemblies.Add(assembly);
                        Context.Log.Info(message: I18N("webexpress:pluginmanager.load"), args: new object[] { assembly.GetName().Name, assembly.GetName().Version.ToString() });
                    }
                }
                catch (BadImageFormatException)
                {

                }
            }

            // register plugin
            foreach (var assembly in assemblies)
            {
                list.AddRange(Register(assembly));
            }

            return list;
        }

        /// <summary>
        /// Loads and registers the plugin from an assembly.
        /// </summary>
        /// <param name="assembly">The assembly where the plugin is located.</param>
        /// <returns>A list of registered plugins.</returns>
        public IEnumerable<IPluginContext> Register(Assembly assembly)
        {
            var list = new List<IPluginContext>();

            try
            {
                foreach (var type in assembly.GetExportedTypes())
                {
                    if (type.IsClass && type.IsSealed && type.GetInterface(typeof(IPlugin).Name) != null && type.Name.Equals("Plugin"))
                    {
                        var id = type.Name?.ToLower();
                        var name = type.Assembly.GetCustomAttribute<AssemblyTitleAttribute>()?.Title;
                        var icon = string.Empty;
                        var description = type.Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;

                        foreach (var customAttribute in type.CustomAttributes.Where(x => x.AttributeType.GetInterfaces().Contains(typeof(IPluginAttribute))))
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
                        }

                        var context = new PluginContext()
                        {
                            Assembly = type.Assembly,
                            PluginID = id,
                            PluginName = name,
                            Manufacturer = type.Assembly.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company,
                            Copyright = type.Assembly.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright,
                            //License = type.Assembly.GetCustomAttribute<AssemblyLicenseAttribute>()?.Copyright,
                            Icon = UriRelative.Combine(Context.ContextPath, icon),
                            Description = description,
                            Version = type.Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion,
                            Host = Context,
                            Log = Context.Log
                        };

                        var plugin = (IPlugin)type.Assembly.CreateInstance(type.FullName);

                        if (!Dictionary.ContainsKey(id))
                        {
                            Dictionary.Add(id, new PluginItem()
                            {
                                Context = context,
                                Plugin = plugin
                            });

                            Context.Log.Info(message: I18N("webexpress:pluginmanager.created"), args: id);
                        }
                        else
                        {
                            Context.Log.Warning(message: I18N("webexpress:pluginmanager.duplicate"), args: id);
                        }

                        list.Add(context);
                    }
                    //else
                    //{
                    //    Context.Log.Warning(message: I18N("webexpress:pluginmanager.notfound"), args: assembly.FullName);
                    //}
                }
            }
            catch (Exception ex)
            {
                Context.Log.Exception(ex);
            }

            return list;
        }

        /// <summary>
        /// Boots the specified plugins.
        /// </summary>
        /// <param name="contexts">A list with the contexts of the plugins to run.</param>
        internal void Boot(IEnumerable<IPluginContext> contexts)
        {
            foreach (var context in contexts)
            {
                if (!Dictionary.ContainsKey(context?.PluginID?.ToLower()))
                {
                    Context.Log.Warning(message: I18N("webexpress:pluginmanager.notavailable"), args: context?.PluginID);

                    return;
                }

                var plugin = Dictionary[context?.PluginID?.ToLower()];

                // initialize plugin
                plugin.Plugin.Initialization(plugin.Context);
                Context.Log.Info(message: I18N("webexpress:pluginmanager.plugin.initialization"), args: plugin.Context.PluginID);

                // run plugin concurrently
                Task.Run(() =>
                {
                    Context.Log.Info(message: I18N("webexpress:pluginmanager.plugin.processing.start"), args: plugin.Context.PluginID);

                    plugin.Plugin.Run();

                    Context.Log.Info(message: I18N("webexpress:pluginmanager.plugin.processing.end"), args: plugin.Context.PluginID);
                });

                // booting applications
                ComponentManager.ApplicationManager.Boot(context);

                // booting modules
                ComponentManager.ModuleManager.Boot(context);
            }
        }

        /// <summary>
        /// Shut down the plugin.
        /// </summary>
        /// <param name="contexts">A list of contexts of plugins to shut down.</param>
        public void ShutDown(IEnumerable<IPluginContext> contexts)
        {

        }
    }
}
