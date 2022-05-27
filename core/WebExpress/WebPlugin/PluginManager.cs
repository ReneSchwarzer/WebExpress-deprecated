using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebExpress.Uri;
using WebExpress.WebAttribute;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.WebPlugin
{
    public static class PluginManager
    {
        /// <summary>
        /// Liefert oder setzt den Verweis auf Kontext des Hostes
        /// </summary>
        public static IHttpServerContext Context { get; private set; }

        /// <summary>
        /// Liefert oder setzt das Verzeichnis, indem die Plugins gelistet sind
        /// </summary>
        private static PluginDictionary Dictionary { get; } = new PluginDictionary();

        /// <summary>
        /// Liefert alle gespeicherten Plugins
        /// </summary>
        public static ICollection<IPluginContext> Plugins => Dictionary.Values.Select(x => x.Context).ToList();

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Verweis auf den Kontext des Hostes
        internal static void Initialization(IHttpServerContext context)
        {
            Context = context;

            Context.Log.Info(message: I18N("webexpress:pluginmanager.initialization"));
        }

        /// <summary>
        /// Lädt und registriert die Plugins
        /// </summary>
        /// <param name="path">Das Verzeichnis, indem sich die Plugins befinden</param>
        internal static void Register(string path)
        {
            if (!File.Exists(path))
            {
                path = Environment.CurrentDirectory;
            }

            var assemblies = new List<Assembly>();

            // Plugins erstellen
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

            // Plugin registrieren
            foreach (var assembly in assemblies)
            {
                Register(assembly);
            }
        }

        /// <summary>
        /// Lädt und registriert das Plugin aus einem Assembly
        /// </summary>
        /// <param name="assembly">Das Assembly, indem sich das Plugins befindet</param>
        public static void Register(Assembly assembly)
        {
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
                            if (customAttribute.AttributeType == typeof(IDAttribute))
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
                    }
                    //else
                    //{
                    //    Context.Log.Warning(message: I18N("webexpress:pluginmanager.notfound"), args: assembly.FullName);
                    //}
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// Ermittelt alle Plugins
        /// </summary>
        /// <returns>Die Auflistung der registrierten Plugins</returns>
        public static IEnumerable<IPluginContext> GetPlugins()
        {
            return Dictionary.Values.Select(x => x.Context);
        }

        /// <summary>
        /// Ermittelt das Plugin zu einer gegebenen ID
        /// </summary>
        /// <param name="pluginID">Die PluginID</param>
        /// <returns>Der Kontext des Plugins oder null</returns>
        public static IPluginContext GetPlugin(string pluginID)
        {
            if (Dictionary.ContainsKey(pluginID?.ToLower()))
            {
                return Dictionary[pluginID?.ToLower()].Context;
            }

            return null;
        }

        /// <summary>
        /// Ermittelt ein Plugin anhand der Dateinamens
        /// </summary>
        /// <param name="pluginFileName">Der Dateiname</param>
        /// <returns>Der Kontext des Plugins oder null</returns>
        public static IPluginContext GetPluginByFileName(string pluginFileName)
        {
            var pluginContext = Dictionary.Values.Where(x => x.Context.Assembly.ManifestModule.Name == pluginFileName).FirstOrDefault()?.Context;

            return pluginContext;
        }

        /// <summary>
        /// Fürt die Plugins aus
        /// </summary>
        internal static void Boot()
        {
            // Piugins initialisieren
            foreach (var plugin in Dictionary.Values)
            {
                plugin.Plugin.Initialization(plugin.Context);
                Context.Log.Info(message: I18N("webexpress:pluginmanager.plugin.initialization"), args: plugin.Context.PluginID);
            }

            // Plugins nebenläufig ausführen
            foreach (var plugin in Dictionary.Values)
            {
                Task.Run(() =>
                {
                    Context.Log.Info(message: I18N("webexpress:pluginmanager.plugin.processing.start"), args: plugin.Context.PluginID);

                    plugin.Plugin.Run();

                    Context.Log.Info(message: I18N("webexpress:pluginmanager.plugin.processing.end"), args: plugin.Context.PluginID);
                });
            }
        }

        /// <summary>
        /// Fürt die Plugins aus
        /// </summary>
        /// <param name="context">Der Kontext des auszuführenden Plugins</param>
        internal static void Boot(IPluginContext context)
        {
            if (!Dictionary.ContainsKey(context?.PluginID?.ToLower()))
            {
                Context.Log.Warning(message: I18N("webexpress:pluginmanager.notavailable"), args: context?.PluginID);

                return;
            }

            var plugin = Dictionary[context?.PluginID?.ToLower()];

            // Piugin initialisieren
            plugin.Plugin.Initialization(plugin.Context);
            Context.Log.Info(message: I18N("webexpress:pluginmanager.plugin.initialization"), args: plugin.Context.PluginID);

            // Plugin nebenläufig ausführen
            Task.Run(() =>
            {
                Context.Log.Info(message: I18N("webexpress:pluginmanager.plugin.processing.start"), args: plugin.Context.PluginID);

                plugin.Plugin.Run();

                Context.Log.Info(message: I18N("webexpress:pluginmanager.plugin.processing.end"), args: plugin.Context.PluginID);
            });
        }

        /// <summary>
        /// Zerstört die Plugins
        /// </summary>
        public static void ShutDown()
        {

        }

        /// <summary>
        /// Meldet ein Plugin ab und zerstört alle darin enthaltenden Elemente
        /// </summary>
        public static void Unsubscribe(string name)
        {

        }

    }
}
