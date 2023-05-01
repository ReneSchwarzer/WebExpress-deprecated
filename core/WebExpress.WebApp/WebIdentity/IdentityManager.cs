using System.Linq;
using System.Security;
using WebExpress.Internationalization;
using WebExpress.WebApplication;
using WebExpress.WebAttribute;
using WebExpress.WebModule;
using WebExpress.WebPlugin;

namespace WebExpress.WebApp.WebIdentity
{
    /// <summary>
    /// Verwaltung der Identitäten (Benutzer)
    /// </summary>
    public class IdentityManager
    {
        /// <summary>
        /// Returns or sets the reference to the context of the host.
        /// </summary>
        public IHttpServerContext HttpServerContext { get; private set; }

        /// <summary>
        /// Ermittelt die aktuelle angemeldete Identität
        /// </summary>
        public static IIdentity CurrentIdentity { get; set; }

        /// <summary>
        /// Liefert oder setzt den Verweis auf Kontext des Plugins
        /// </summary>
        private static IPluginContext Context { get; set; }

        /// <summary>
        /// Liefert oder setzt das Verzeichnis, indem die Identitäten, Rollen und Ressourcen gelistet sind
        /// </summary>
        private static IdentityDictionary Dictionary { get; } = new IdentityDictionary();

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The reference to the context of the host.</param>
        public void Initialization(IHttpServerContext context)
        {
            HttpServerContext = context;

            HttpServerContext.Log.Debug(message: InternationalizationManager.I18N("webexpress:identitymanager.initialization"));
        }

        /// <summary>
        /// Fügt Identitäts-Einträge hinzu
        /// </summary>
        /// <param name="context">Der Kontext, welcher für die Ausführung des Plugins gilt</param>
        public void Register(IModuleContext context)
        {
            var assembly = context.PluginContext.Assembly;

            foreach (var type in assembly.GetExportedTypes().Where(x => x.IsClass && x.IsSealed && x.GetInterface(typeof(IIdentityResource).Name) != null))
            {
                var id = type.Name?.ToLower();
                var name = type.Name;
                var description = string.Empty;
                var moduleID = string.Empty;

                foreach (var customAttribute in type.CustomAttributes.Where(x => x.AttributeType.GetInterfaces().Contains(typeof(WebExIModuleAttribute))))
                {
                    if (customAttribute.AttributeType == typeof(WebExIDAttribute))
                    {
                        id = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString().ToLower();
                    }

                    if (customAttribute.AttributeType == typeof(WebExNameAttribute))
                    {
                        name = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString();
                    }

                    if (customAttribute.AttributeType == typeof(WebExDescriptionAttribute))
                    {
                        description = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString();
                    }

                    if (customAttribute.AttributeType == typeof(WebExModuleAttribute))
                    {
                        moduleID = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString().ToLower();
                    }
                }

                if (string.IsNullOrWhiteSpace(moduleID))
                {
                    // Es wurde kein Modul angebgeben
                    HttpServerContext.Log.Warning(message: InternationalizationManager.I18N("webexpress:identitymanager.moduleless"), args: id);

                    continue;
                }

                // Zugehörige Modul ermitteln. 
                //var module = ModuleManager.GetModule(context.Application, moduleID);

                //if (module == null || !(module.ModuleID == context.ModuleID && module.Application.ApplicationID == context.Application.ApplicationID))
                //{
                //    continue;
                //}


            }
        }

        /// <summary>
        /// Meldet eine Identität an
        /// </summary>
        /// <param name="identity">Die Identität</param>
        public static void Register(IIdentity identity)
        {

        }

        /// <summary>
        /// Meldet eine Rolle an
        /// </summary>
        /// <param name="role">Die (Identitäts-)Rolle</param>
        public static void Register(IIdentityRole role)
        {

        }

        /// <summary>
        /// Meldet eine Ressource an
        /// </summary>
        /// <param name="resource">Die Ressorce</param>
        public static void Register(IIdentityResource resource)
        {

        }

        /// <summary>
        /// Entfernt alle IDentitäten, Rollen und Ressourcen einer Anwendung
        /// </summary>
        /// <param name="application">Die Anwendung</param>
        public static void Unregister(IApplicationContext application)
        {

        }

        /// <summary>
        /// MEldet eine Identität an
        /// </summary>
        /// <param name="login">Die Loginkennung</param>
        /// <param name="password">Das Passwort</param>
        /// <returns>true wenn erfolgreich, false sonst</returns>
        public static bool Login(string login, SecureString password)
        {
            return false;
        }

        /// <summary>
        /// Loggt die aktuell angemeldete IDentität ab
        /// </summary>
        public static void Logout()
        {

        }

        /// <summary>
        /// Prüft, ob Zugriffsrechte vorhanden sind
        /// </summary>
        /// <param name="identiy">Die Identität</param>
        /// <param name="mode">Der Zugriffsmodus Read|Write|Execute</param>
        public static void CheckAccess(IIdentity identiy, string mode)
        {

        }
    }
}
