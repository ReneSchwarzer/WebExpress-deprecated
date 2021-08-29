using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebExpress.Attribute;
using WebExpress.Plugin;
using WebExpress.Uri;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.Application
{
    public static class ApplicationManager
    {
        /// <summary>
        /// Liefert oder setzt den Verweis auf Kontext des Hostes
        /// </summary>
        public static IHttpServerContext Context { get; private set; }

        /// <summary>
        /// Liefert oder setzt das Verzeichnis, indem die Anwendungen gelistet sind
        /// </summary>
        private static ApplicationDictionary Dictionary { get; } = new ApplicationDictionary();

        /// <summary>
        /// Liefert alle gespeicherten Anwendungen
        /// </summary>
        public static ICollection<IApplicationContext> Applications => Dictionary.Values.Select(x => x.Context).ToList();

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Verweis auf den Kontext des Hostes</param>
        internal static void Initialization(IHttpServerContext context)
        {
            Context = context;

            Context.Log.Info(message: I18N("webexpress:applicationmanager.initialization"));
        }

        /// <summary>
        /// Fügt Anwendungs-Einträge aus allen geladenen Plugins hinzu
        /// </summary>
        public static void Register()
        {
            foreach (var plugin in PluginManager.Plugins)
            {
                Register(plugin);
            }
        }

        /// <summary>
        /// Fügt Anwendungs-Einträge aus dem angegebenen Plugin hinzu
        /// </summary>
        /// <param name="pluginContext">Der Kontext des zugehörigen Plugins</param>
        public static void Register(IPluginContext pluginContext)
        {
            var assembly = pluginContext.Assembly;

            foreach (var type in assembly.GetExportedTypes().Where(x => x.IsClass && x.IsSealed && x.GetInterface(typeof(IApplication).Name) != null))
            {
                var id = type.Name?.ToLower();
                var name = type.Name;
                var icon = string.Empty;
                var description = string.Empty;
                var contextPath = string.Empty;
                var assetPath = string.Empty;

                // Attribute ermitteln
                foreach (var customAttribute in type.CustomAttributes.Where(x => x.AttributeType.GetInterfaces().Contains(typeof(IApplicationAttribute))))
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
                }

                // Anwendungskontext erstellen
                var context = new ApplicationContext()
                {
                    Assembly = assembly,
                    PluginID = pluginContext.PluginID,
                    ApplicationID = id,
                    ApplicationName = name,
                    Description = description,
                    Icon = UriRelative.Combine(Context.ContextPath, contextPath, icon),
                    AssetPath = Path.Combine(Context.AssetPath, assetPath),
                    ContextPath = UriRelative.Combine(Context.ContextPath, contextPath),
                    Log = Context.Log
                };

                // Anwendung erstellen
                var application = (IApplication)type.Assembly.CreateInstance(type.FullName);

                if (!Dictionary.ContainsKey(id))
                {
                    Dictionary.Add(id, new ApplicationItem()
                    {
                        Context = context,
                        Application = application
                    });

                    Context.Log.Info(message: I18N("webexpress:applicationmanager.register"), args: id);
                }
                else
                {
                    Context.Log.Warning(message: I18N("webexpress:applicationmanager.duplicate"), args: id);
                }
            }
        }

        /// <summary>
        /// Ermittelt die registrierten Anwendungen 
        /// </summary>
        /// <returns>Eine Aufzählung mit allen registrierten Anwendungen</returns>
        public static IEnumerable<IApplicationContext> GetApplcations()
        {
            return Dictionary.Values.Select(x => x.Context);
        }

        /// <summary>
        /// Ermittelt die Anwendung zu einer gegebenen ID
        /// </summary>
        /// <param name="applicationID">Die AnwendungsID</param>
        /// <returns>Der Kontext der Anwendung oder null</returns>
        public static IApplicationContext GetApplcation(string applicationID)
        {
            if (string.IsNullOrWhiteSpace(applicationID)) return null;

            if (Dictionary.ContainsKey(applicationID.ToLower()))
            {
                return Dictionary[applicationID.ToLower()].Context;
            }

            return null;
        }

        /// <summary>
        /// Ermittelt die Anwendung zu einem gegebenen Assembly
        /// </summary>
        /// <param name="applicationAssembly">Das Assembly, zudem der Anwendungskontext ermittelt werden soll</param>
        /// <returns>Der Kontext der Anwendung oder null</returns>
        public static IApplicationContext GetApplcation(Assembly applicationAssembly)
        {
            return Dictionary.Values.Where(x => x.Context.Assembly == applicationAssembly).Select(x => x.Context).FirstOrDefault();
        }

        /// <summary>
        /// Fürt die Anwendungen aus
        /// </summary>
        internal static void Boot()
        {
            // Piugins initialisieren
            foreach (var application in Dictionary.Values)
            {
                application.Application.Initialization(application.Context);
                //Context.Log.Info(message: I18N("webexpress:pluginmanager.plugin.initialization"), args: application.Context.PluginID);
            }

            // Plugins nebenläufig ausführen
            foreach (var application in Dictionary.Values)
            {
                Task.Run(() =>
                {
                    //Context.Log.Info(message: I18N("webexpress:pluginmanager.plugin.processing.start"), args: plugin.Context.PluginID);

                    application.Application.Run();

                    //Context.Log.Info(message: I18N("webexpress:pluginmanager.plugin.processing.end"), args: plugin.Context.PluginID);
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
