﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebExpress.Internationalization;
using WebExpress.WebUri;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;

namespace WebExpress.WebPlugin
{
    /// <summary>
    /// The plugin manager manages the WebExpress plugins.
    /// </summary>
    public class PluginManager : IComponent, IExecutableElements, ISystemComponent
    {
        /// <summary>
        /// An event that fires when an plugin is added.
        /// </summary>
        public event EventHandler<IPluginContext> AddPlugin;

        /// <summary>
        /// An event that fires when an plugin is removed.
        /// </summary>
        public event EventHandler<IPluginContext> RemovePlugin;

        /// <summary>
        /// Returns or sets the reference to the context of the host.
        /// </summary>
        public IHttpServerContext HttpServerContext { get; private set; }

        /// <summary>
        /// Returns the directory where the plugins are listed.
        /// </summary>
        private PluginDictionary Dictionary { get; } = new PluginDictionary();

        /// <summary>
        /// Returns all plugins.
        /// </summary>
        public ICollection<IPluginContext> Plugins => Dictionary.Values.Select(x => x.PluginContext).ToList();

        /// <summary>
        /// Constructor
        /// </summary>
        internal PluginManager()
        {
            ComponentManager.AddComponent += (s, e) =>
            {
                //AssignToComponent(e);
            };

            ComponentManager.RemoveComponent += (s, e) =>
            {
                //DetachFromcomponent(e);
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
                InternationalizationManager.I18N("webexpress:pluginmanager.initialization")
            );
        }

        /// <summary>
        /// Loads and registers the plugins that are static (i.e. located in the application's folder).
        /// </summary>1
        /// <returns>A list of plugins created.</returns>
        internal void Register()
        {
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
                        HttpServerContext.Log.Debug
                        (
                            InternationalizationManager.I18N
                            (
                                "webexpress:pluginmanager.load",
                                assembly.GetName().Name,
                                assembly.GetName().Version.ToString()
                            )
                        );
                    }
                }
                catch (BadImageFormatException)
                {

                }
            }

            // register plugin
            foreach (var assembly in assemblies.OrderBy(x => x.GetCustomAttribute(typeof(SystemPluginAttribute)) != null ? 0 : 1))
            {
                Register(assembly);
            }
        }

        /// <summary>
        /// Loads and registers the plugins from a path.
        /// </summary>
        /// <param name="path">The directory where the plugins are located.</param>
        internal IEnumerable<IPluginContext> Register(string path)
        {
            var assemblies = new List<Assembly>();
            var pluginContexts = new List<IPluginContext>();

            if (!Directory.Exists(path))
            {
                return pluginContexts;
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
                        HttpServerContext.Log.Debug
                        (
                            InternationalizationManager.I18N
                            (
                                "webexpress:pluginmanager.load",
                                assembly.GetName().Name,
                                assembly.GetName().Version.ToString()
                            )
                        );
                    }
                }
                catch (BadImageFormatException)
                {

                }
            }

            // register plugin
            foreach (var assembly in assemblies)
            {
                pluginContexts.AddRange(Register(assembly));
            }

            return pluginContexts;
        }

        /// <summary>
        /// Loads and registers the plugin from an assembly.
        /// </summary>
        /// <param name="assembly">The assembly where the plugin is located.</param>
        /// <returns>A list of plugins created.</returns>
        private IEnumerable<IPluginContext> Register(Assembly assembly)
        {
            var list = new List<IPluginContext>();

            try
            {
                foreach (var type in assembly
                    .GetExportedTypes()
                    .Where(x => x.IsClass && x.IsSealed)
                    .Where(x => x.GetInterface(typeof(IPlugin).Name) != null)
                    .Where(x => x.Name.Equals("Plugin")))
                {
                    var id = type.Name?.ToLower();
                    var name = type.Assembly.GetCustomAttribute<AssemblyTitleAttribute>()?.Title;
                    var icon = string.Empty;
                    var description = type.Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;

                    foreach (var customAttribute in type.CustomAttributes
                        .Where(x => x.AttributeType.GetInterfaces().Contains(typeof(IPluginAttribute))))
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

                    var pluginContext = new PluginContext()
                    {
                        Assembly = type.Assembly,
                        PluginID = id,
                        PluginName = name,
                        Manufacturer = type.Assembly.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company,
                        Copyright = type.Assembly.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright,
                        //License = type.Assembly.GetCustomAttribute<AssemblyLicenseAttribute>()?.Copyright,
                        Icon = UriRelative.Combine(HttpServerContext.ContextPath, icon),
                        Description = description,
                        Version = type.Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion,
                        Host = HttpServerContext,
                        Log = HttpServerContext.Log
                    };

                    if (!Dictionary.ContainsKey(id))
                    {
                        list.Add(pluginContext);

                        Dictionary.Add(id, new PluginItem()
                        {
                            PluginClass = type,
                            PluginContext = pluginContext,
                            Plugin = null
                        });

                        HttpServerContext.Log.Debug
                        (
                            InternationalizationManager.I18N("webexpress:pluginmanager.created", id)
                        );

                        OnAddPlugin(pluginContext);
                    }
                    else
                    {
                        HttpServerContext.Log.Warning
                        (
                            InternationalizationManager.I18N("webexpress:pluginmanager.duplicate", id)
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                HttpServerContext.Log.Exception(ex);
            }

            return list;
        }

        /// <summary>
        /// Returns a plugin context based on its ID.
        /// </summary>
        /// <param name="id">The id of the plugin.</param>
        /// <returns>The plugin context.</returns>
        public IPluginContext GetPlugin(string id)
        {
            return Dictionary.Values
                .Where(x => x.PluginContext != null && x.PluginContext.PluginID.Equals(id, StringComparison.OrdinalIgnoreCase))
                .Select(x => x.PluginContext)
                .FirstOrDefault();
        }

        /// <summary>
        /// Boots the specified plugin.
        /// </summary>
        /// <param name="contexts">The context of the plugin to run.</param>
        internal void Boot(IPluginContext context)
        {
            if (!Dictionary.ContainsKey(context?.PluginID?.ToLower()))
            {
                HttpServerContext.Log.Warning
                (
                    InternationalizationManager.I18N
                    (
                        "webexpress:pluginmanager.notavailable",
                        context?.PluginID
                    )
                );

                return;
            }

            var plugin = Dictionary[context?.PluginID?.ToLower()];

            plugin.Plugin = (IPlugin)plugin.PluginClass.Assembly.CreateInstance(plugin.PluginClass.FullName);

            // initialize plugin
            plugin.Plugin.Initialization(plugin.PluginContext);
            HttpServerContext.Log.Debug
            (
                InternationalizationManager.I18N
                (
                    "webexpress:pluginmanager.plugin.initialization",
                    plugin.PluginContext.PluginID
                )
            );

            // run plugin concurrently
            Task.Run(() =>
            {
                HttpServerContext.Log.Debug
                (
                    InternationalizationManager.I18N
                    (
                        "webexpress:pluginmanager.plugin.processing.start", plugin.PluginContext.PluginID
                    )
                );

                plugin.Plugin.Run();

                HttpServerContext.Log.Debug
                (
                    InternationalizationManager.I18N
                    (
                        "webexpress:pluginmanager.plugin.processing.end",
                        plugin.PluginContext.PluginID
                    )
                );
            });
        }

        /// <summary>
        /// Boots the specified plugins.
        /// </summary>
        /// <param name="contexts">A list with the contexts of the plugins to run.</param>
        internal void Boot(IEnumerable<IPluginContext> contexts)
        {
            foreach (var context in contexts)
            {
                Boot(context);
            }
        }

        /// <summary>
        /// Shut down the plugin.
        /// </summary>
        /// <param name="pluginContext">The context of the plugin to shut down.</param>
        public void ShutDown(IPluginContext pluginContext)
        {

        }

        /// <summary>
        /// Shut down the plugins.
        /// </summary>
        /// <param name="contexts">A list of contexts of plugins to shut down.</param>
        public void ShutDown(IEnumerable<IPluginContext> contexts)
        {
            foreach (var context in contexts)
            {
                ShutDown(context);
            }
        }

        /// <summary>
        /// Raises the AddPlugin event.
        /// </summary>
        /// <param name="component">The plugin context.</param>
        private void OnAddPlugin(IPluginContext pluginContext)
        {
            AddPlugin?.Invoke(this, pluginContext);
        }

        /// <summary>
        /// Raises the RemovePlugin event.
        /// </summary>
        /// <param name="component">The plugin context.</param>
        private void OnRemovePlugin(IPluginContext pluginContext)
        {
            RemovePlugin?.Invoke(this, pluginContext);
        }
    }
}
