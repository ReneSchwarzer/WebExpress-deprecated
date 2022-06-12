using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.WebAttribute;
using WebExpress.WebApp.WebSettingPage;
using WebExpress.WebApplication;
using WebExpress.WebAttribute;
using WebExpress.WebModule;
using WebExpress.WebPlugin;
using WebExpress.WebResource;
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
        public static void Register(IApplicationContext application)
        {
            foreach (var module in ModuleManager.Modules.Where(x => x.Application.Equals(application)))
            {
                Register(module);
            }
        }

        /// <summary>
        /// Fügt die Einstellungseiten-Schlüssel-Wert-Paare aus dem angegebenen Plugin hinzu
        /// </summary>
        /// <param name="module">Das Modul, welches die Einstellungsseiten enthällt</param>
        /// <param name="application">Die Anwendung, welche die Komponenten zugewiesen werden</param>
        internal static void Register(IModuleContext module)
        {
            var assemblyName = module.Assembly.GetName().Name.ToLower();

            foreach (var settingPageType in module.Assembly.GetTypes().Where(x => x.IsClass && x.IsSealed && (x.GetInterfaces().Contains(typeof(IPageSetting)))))
            {
                var id = settingPageType.Name?.ToLower();
                var context = null as string;
                var group = null as string;
                var section = SettingSection.Primary;
                var hide = false;
                var icon = null as PropertyIcon;
                var optional = false;

                // Attribute ermitteln
                foreach (var customAttribute in settingPageType.CustomAttributes.Where(x => x.AttributeType.GetInterfaces().Contains(typeof(IResourceAttribute))))
                {
                    if (customAttribute.AttributeType == typeof(IdAttribute))
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
                    else if (customAttribute.AttributeType == typeof(OptionalAttribute))
                    {
                        optional = true;
                    }
                }

                // Prüfe ob eine optionale Ressource
                if (optional && !(module.Application.Options.Contains($"{module.ModuleID}.*".ToLower()) || module.Application.Options.Contains($"{module.ModuleID}.{id}".ToLower())))
                {
                    continue;
                }

                // Seite mit Metainformationen erzeugen
                var page = new SettingPageDictionaryItemMetaPage()
                {
                    ID = id,
                    ModuleContext = module,
                    Type = settingPageType,
                    Node = ResourceManager.FindByID(module.Application.ApplicationID, id),
                    Icon = icon,
                    Hide = hide
                };

                // Seite in das Dictionary einfügen
                Dictionary.AddPage(module.Application.ApplicationID, context, section, group, page);

                // Logging
                var log = new List<string>
                {
                    I18N("webexpress.webapp:pagesettingmanager.register"),
                    "    ApplicationID    = " + module?.Application.ApplicationID,
                    "    ModuleID         = " + module?.ModuleID,
                    "    SettingContext   = " + context ?? "null",
                    "    SettingSection   = " + section.ToString(),
                    "    SettingGroup     = " + group ?? "null",
                    "    SettingPage.ID   = " + page?.ID ?? "null",
                    "    SettingPage.Type = " + (page?.Type != null ? page.Type.ToString() : "null"),
                    "    SettingPage.Node = " + (page?.Node != null ? page.Node?.ToString() : "null"),
                    "    SettingPage.Hide = " + (page?.Hide != null ? page?.Hide.ToString() : "null")
                };

                Context.Log.Info(string.Join(Environment.NewLine, log));
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