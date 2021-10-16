using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebExpress.Application;
using WebExpress.Attribute;
using WebExpress.Plugin;
using WebExpress.Uri;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.Module
{
    public static class ModuleManager
    {
        /// <summary>
        /// Liefert oder setzt den Verweis auf Kontext des Hostes
        /// </summary>
        private static IHttpServerContext Context { get; set; }

        /// <summary>
        /// Liefert oder setzt das Verzeichnis, indem die Anwendungen gelistet sind
        /// </summary>
        private static ModuleDictionary Dictionary { get; } = new ModuleDictionary();

        /// <summary>
        /// Liefert alle gespeicherten Module
        /// </summary>
        public static ICollection<IModuleContext> Modules => Dictionary.Values.SelectMany(x => x.Values).Select(x => x.Context).ToList();

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Verweis auf den Kontext des Hostes</param>
        internal static void Initialization(IHttpServerContext context)
        {
            Context = context;

            Context.Log.Info(message: I18N("webexpress:modulemanager.initialization"));
        }

        // <summary>
        /// Fügt Modul-Einträge aus allen geladenen Plugins hinzu
        /// </summary>
        public static void Register()
        {
            foreach (var plugin in PluginManager.Plugins)
            {
                Register(plugin);
            }
        }

        /// <summary>
        /// Fügt Modul-Einträge aus dem angegebenen Plugin hinzu
        /// </summary>
        /// <param name="pluginContext">Der Kontext des zugehörigen Plugins</param>
        public static void Register(IPluginContext pluginContext)
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
                var applicationID = string.Empty;

                foreach (var customAttribute in type.CustomAttributes.Where(x => x.AttributeType.GetInterfaces().Contains(typeof(IModuleAttribute))))
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

                    if (customAttribute.AttributeType == typeof(ContextPathAttribute))
                    {
                        contextPath = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString();
                    }

                    if (customAttribute.AttributeType == typeof(AssetPathAttribute))
                    {
                        assetPath = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString();
                    }

                    if (customAttribute.AttributeType == typeof(ApplicationAttribute))
                    {
                        applicationID = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString().ToLower();
                    }
                }

                if (string.IsNullOrWhiteSpace(applicationID))
                {
                    // Es wurde keine Anwendung angebgeben
                    Context.Log.Warning(message: I18N("webexpress:modulemanager.applicationless"), args: id);
                }

                // Zugehörige Anwendungen ermitteln. 
                var applications = ApplicationManager.GetApplcations(applicationID);

                if (!applications.Any())
                {
                    // Anwendung wurde nicht gefunden
                    Context.Log.Warning(message: I18N("webexpress:modulemanager.applicationnotfound"), args: new object[] { id, applicationID });
                }

                foreach (var application in applications)
                {
                    var cp = UriRelative.Combine(Context.ContextPath, application.ContextPath);

                    // Kontext erstellen
                    var context = new ModuleContext()
                    {
                        Assembly = assembly,
                        PluginID = pluginContext.PluginID,
                        ApplicationID = application.ApplicationID,
                        ModuleID = id,
                        ModuleName = name,
                        Description = description,
                        Icon = UriRelative.Combine(cp, contextPath, icon),
                        AssetPath = Path.Combine(Context.AssetPath, assetPath),
                        ContextPath = UriRelative.Combine(cp, contextPath),
                        Log = Context.Log
                    };

                    // Modul erstellen
                    var module = (IModule)type.Assembly.CreateInstance(type.FullName);

                    if (!Dictionary.ContainsKey(application))
                    {
                        Dictionary.Add(application, new Dictionary<string, ModuleItem>());
                    }

                    var item = Dictionary[application];

                    if (!item.ContainsKey(id))
                    {
                        item.Add(id, new ModuleItem()
                        {
                            Application = application,
                            Context = context,
                            Module = module
                        });

                        Context.Log.Info(message: I18N("webexpress:modulemanager.register"), args: new object[] { id, application.ApplicationID });
                    }
                    else
                    {
                        Context.Log.Warning(message: I18N("webexpress:modulemanager.duplicate"), args: new object[] { id, application.ApplicationID });
                    }
                }
            }
        }

        /// <summary>
        /// Ermittelt das Modul zu einer gegebenen ID
        /// </summary>
        /// <param name="application">Der Kontext der Anwendung</param>
        /// <param name="moduleID">Die ModulID</param>
        /// <returns>Der Kontext des Moduls oder null</returns>
        public static IModuleContext GetModule(IApplicationContext application, string moduleID)
        {
            var item = Dictionary.ContainsKey(application) ? Dictionary[application] : null;

            if (item != null && item.ContainsKey(moduleID?.ToLower()))
            {
                return item[moduleID?.ToLower()].Context;
            }

            return null;
        }

        /// <summary>
        /// Ermittelt das Modul zu einer gegebenen ID
        /// </summary>
        /// <param name="applicationID">Die AnwendungsID</param>
        /// <param name="moduleID">Die ModulID</param>
        /// <returns>Der Kontext des Moduls oder null</returns>
        public static IModuleContext GetModule(string applicationID, string moduleID)
        {
            var application = ApplicationManager.GetApplcation(applicationID);

            if (application == null)
            {
                return null;
            }

            var item = Dictionary.ContainsKey(application) ? Dictionary[application] : null;

            if (item != null && item.ContainsKey(moduleID?.ToLower()))
            {
                return item[moduleID?.ToLower()].Context;
            }

            return null;
        }

        /// <summary>
        /// Ermittelt den Modulkontext zu einem gegebenen Assembly
        /// </summary>
        /// <param name="moduleAssembly">Das Assembly, zudem der Modulkontext ermittelt werden soll</param>
        /// <returns>Der Kontext des ersten passenden Moduls oder null</returns>
        public static IModuleContext GetApplcation(Assembly moduleAssembly)
        {
            return Dictionary.Values
                .SelectMany(x => x.Values)
                .Select(x => x.Context)
                .Where(x => x.Assembly == moduleAssembly)
                .Select(x => x)
                .FirstOrDefault();
        }

        /// <summary>
        /// Fürt die Anwendungen aus
        /// </summary>
        internal static void Boot()
        {
            // Piugins initialisieren
            foreach (var module in Dictionary.Values.SelectMany(x => x.Values))
            {
                module.Module.Initialization(module.Context);
                Context.Log.Info(message: I18N("webexpress:modulemanager.module.initialization"), args: new[] { module.Context.ApplicationID, module.Context.PluginID });
            }

            // Plugins nebenläufig ausführen
            foreach (var module in Dictionary.Values.SelectMany(x => x.Values))
            {
                Task.Run(() =>
                {
                    Context.Log.Info(message: I18N("webexpress:modulemanager.module.processing.start"), args: new[] { module.Context.ApplicationID, module.Context.PluginID });

                    module.Module.Run();

                    Context.Log.Info(message: I18N("webexpress:modulemanager.module.processing.end"), args: new[] { module.Context.ApplicationID, module.Context.PluginID });
                });
            }
        }

        /// <summary>
        /// Ausführung der Anwendungen beenden
        /// </summary>
        public static void ShutDown()
        {

        }
    }
}
