using System;
using System.Linq;
using System.Reflection;
using WebExpress.Attribute;
using WebExpress.Module;
using WebExpress.Plugin;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.Attribute;
using WebExpress.WebApp.WebResource;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.WebApp.SettingPage
{
    /// <summary>
    /// Verwaltung der Einstellungsseiten
    /// </summary>
    public class SettingPageManager
    {
        /// <summary>
        /// Liefert oder setzt den Verweis auf Kontext des Plugins
        /// </summary>
        private static IPluginContext Context { get; set; }

        /// <summary>
        /// Liefert oder setzt das Verzeichnis, indem die Kompomenten gelistet sind
        /// </summary>
        private static SettingPageDictionary Dictionary { get; } = new SettingPageDictionary();

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Verweis auf den Kontext des Plugins</param>
        internal static void Initialization(IPluginContext context)
        {
            Context = context;

            Context.Log.Info(message: I18N("webexpress.webapp:pagesettingmanager.initialization"));
        }

        /// <summary>
        /// Fügt Anwendungs-Einträge aus allen geladenen Plugins hinzu
        /// </summary>
        /// <param name="application">Die Anwendung, welche die Komponenten zugewiesen werden</param>
        public static void Register(string application)
        {
            foreach (var plugin in PluginManager.Plugins)
            {
                Register(plugin.Assembly, application);
            }
        }

        /// <summary>
        /// Fügt die Einstellungseiten-Schlüssel-Wert-Paare aus dem angegebenen Plugin hinzu
        /// </summary>
        /// <param name="assembly">Das Assembly, welches die einzufügenden Schlüssel-Wert-Paare enthällt</param>
        /// <param name="application">Die Anwendung, welche die Komponenten zugewiesen werden</param>
        internal static void Register(Assembly assembly, string application)
        {
            var assemblyName = assembly.GetName().Name.ToLower();

            foreach (var settingPageType in assembly.GetTypes().Where(x => x.IsClass && x.IsSealed && (x.GetInterfaces().Contains(typeof(IPageSetting)))))
            {
                var applicationID = string.Empty;
                var id = settingPageType.Name?.ToLower();
                var context = null as string;
                var group = null as string;
                var section = SettingSection.Primary;
                var hide = false;
                var icon = null as PropertyIcon;

                // Attribute ermitteln
                foreach (var customAttribute in settingPageType.CustomAttributes.Where(x => x.AttributeType.GetInterfaces().Contains(typeof(IModuleAttribute))))
                {
                    if (customAttribute.AttributeType == typeof(ApplicationAttribute))
                    {
                        applicationID = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString().ToLower();
                    }
                }

                foreach (var customAttribute in settingPageType.CustomAttributes.Where(x => x.AttributeType.GetInterfaces().Contains(typeof(IResourceAttribute))))
                {
                    if (customAttribute.AttributeType == typeof(IDAttribute))
                    {
                        id = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString();
                    }
                    else if (customAttribute.AttributeType == typeof(SettingContextAttribute))
                    {
                        context = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString();
                    }
                    else if (customAttribute.AttributeType == typeof(SettingGroupAttribute))
                    {
                        group = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString();
                    }
                    else if (customAttribute.AttributeType == typeof(SettingSectionAttribute))
                    {
                        section = (SettingSection)Enum.Parse(typeof(SettingSection), customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString());
                    }
                    else if (customAttribute.AttributeType == typeof(SettingHideAttribute))
                    {
                        hide = true;
                    }
                    else if (customAttribute.AttributeType == typeof(SettingIconAttribute))
                    {
                        var iconAttribute = customAttribute.ConstructorArguments.FirstOrDefault().Value;
                        icon = iconAttribute?.GetType() == typeof(int) ? new PropertyIcon((TypeIcon)Enum.Parse(typeof(TypeIcon), iconAttribute?.ToString())) : new PropertyIcon(new UriRelative(iconAttribute?.ToString()));
                    }
                }

                // Standard für Anwendung festlegen
                if (string.IsNullOrWhiteSpace(applicationID))
                {
                    var moduleContext = ModuleManager.GetApplcation(assembly);
                    applicationID = moduleContext.ApplicationID ?? "*";
                }

                // Seite mit Metainformationen erzeugen
                var page = new SettingPageDictionaryItemMetaPage()
                {
                    ID = id,
                    Page = settingPageType,
                    Icon = icon,
                    Hide = hide
                };

                // Seite in das Dictionary einfügen
                Dictionary.AddPage(applicationID, context, section, group, page);

                // Anwendung wurde nicht gefunden
                Context.Log.Warning(message: I18N("webexpress.webapp:pagesettingmanager.register"), args: new object[] 
                { 
                    applicationID, 
                    context != null ? context : "null", 
                    section.ToString(), 
                    group != null ? group : "null", 
                    page?.ID != null ? page?.ID : "null", 
                    page?.Page != null ? page.Page.ToString() : "null",  
                    page?.Hide != null ? page?.Hide.ToString() : "null"
                });
            }
        }

        /// <summary>
        /// Sucht eine Seite anhand seiner ID
        /// </summary>
        /// <param name="applicationID">Die AnwendungsID</param>
        /// <param name="pageID">Die Seite</param>
        /// <returns>Die gefundene Seite oder null</returns>
        /// <summary>
        public static SettingPageSearchResult FindPage(string applicationID, string pageID)
        {
            return Dictionary.FindPage(applicationID, pageID);
        }

        /// <summary>
        /// Liefere alle Kontexte
        /// </summary>
        /// <param name="applicationID">Die AnwendungsID</param>
        /// <returns>Eine Auflistung aller Kontexte</returns>
        /// <summary>
        public static SettingPageDictionaryItemContext GetContexts(string applicationID)
        {
            return Dictionary.GetContexts(applicationID);
        }

        /// <summary>
        /// Liefere alle Sektionen, welche den seleben Settingkontext besitzen
        /// </summary>
        /// <param name="applicationID">Die AnwendungsID</param>
        /// <param name="context">Der Kontext</param>
        /// <returns>Eine Auflistung aller Sektionen des gleichen Kontextes</returns>
        /// <summary>
        public static SettingPageDictionaryItemSection GetSections(string applicationID, string context)
        {
            return Dictionary.GetSections(applicationID, context);
        }
    }
}