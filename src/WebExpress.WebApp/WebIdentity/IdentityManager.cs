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
        /// Liefert oder setzt das Verzeichnis, indem Returns or sets the id.entitäten, Rollen und Ressourcen gelistet sind
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
        /// <param name="context">The context that applies to the execution of the plugin.</param>
        public void Register(IModuleContext context)
        {
            var assembly = context.PluginContext.Assembly;

            foreach (var type in assembly.GetExportedTypes().Where(x => x.IsClass && x.IsSealed && x.GetInterface(typeof(IIdentityResource).Name) != null))
            {
                var id = type.Name?.ToLower();
                var name = type.Name;
                var description = string.Empty;
                var moduleId = string.Empty;

                foreach (var customAttribute in type.CustomAttributes.Where(x => x.AttributeType.GetInterfaces().Contains(typeof(IModuleAttribute))))
                {
                    if (customAttribute.AttributeType == typeof(WebExIdAttribute))
                    {
                        id = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString().ToLower();
                    }
                    else if (customAttribute.AttributeType == typeof(WebExNameAttribute))
                    {
                        name = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString();
                    }
                    else if (customAttribute.AttributeType == typeof(WebExDescriptionAttribute))
                    {
                        description = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString();
                    }
                    else if (customAttribute.AttributeType.Name == typeof(WebExModuleAttribute<>).Name && customAttribute.AttributeType.Namespace == typeof(WebExModuleAttribute<>).Namespace)
                    {
                        moduleId = customAttribute.AttributeType.GenericTypeArguments.FirstOrDefault()?.FullName?.ToLower();
                    }
                }

                if (string.IsNullOrWhiteSpace(moduleId))
                {
                    // Es wurde kein Modul angebgeben
                    HttpServerContext.Log.Warning(message: InternationalizationManager.I18N("webexpress:identitymanager.moduleless"), args: id);

                    continue;
                }

                // Zugehörige Modul ermitteln. 
                //var module = ModuleManager.GetModule(context.Application, moduleId);

                //if (module == null || !(module.ModuleId == context.ModuleId && module.Application.ApplicationId == context.Application.ApplicationId))
                //{
                //    continue;
                //}


            }
        }

        /// <summary>
        /// Meldet eine Identität an
        /// </summary>
        /// <param name="identity">Returns or sets the id.entität</param>
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
        /// Entfernt alle Identitäten, Rollen und Ressourcen einer Anwendung
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
        /// Loggt die aktuell angemeldete Identität ab
        /// </summary>
        public static void Logout()
        {

        }

        /// <summary>
        /// Prüft, ob Zugriffsrechte vorhanden sind
        /// </summary>
        /// <param name="identiy">Returns or sets the id.entität</param>
        /// <param name="mode">Der Zugriffsmodus Read|Write|Execute</param>
        public static void CheckAccess(IIdentity identiy, string mode)
        {

        }
    }
}
