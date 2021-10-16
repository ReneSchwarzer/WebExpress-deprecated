using System.Linq;
using System.Reflection;
using WebExpress.Attribute;
using WebExpress.Job;
using WebExpress.Plugin;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.Scheduler
{
    /// <summary>
    /// Verarbeitung von zyklischen Jobs
    /// </summary>
    public static class ScheduleManager
    {
        /// <summary>
        /// Liefert oder setzt den Verweis auf Kontext des Hostes
        /// </summary>
        private static IHttpServerContext Context { get; set; }

        /// <summary>
        /// Liefert oder setzt das Verzeichnis, indem die Jobs gelistet sind
        /// </summary>
        private static ScheduleDictionary Dictionary { get; } = new ScheduleDictionary();

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Verweis auf den Kontext des Hostes
        internal static void Initialization(IHttpServerContext context)
        {
            Context = context;

            Context.Log.Info(message: I18N("webexpress:schedulermanager.initialization"));
        }

        /// <summary>
        /// Registriert die Jobs
        /// </summary>
        public static void Register()
        {
            foreach (var module in PluginManager.Plugins)
            {
                Register(module);
            }
        }

        /// <summary>
        /// Registriert die Ressourcen eines Moduls
        /// </summary>
        /// <param name="moduleContext">Der Kontext des Moduls, welches die Ressourcen enthält</param>
        public static void Register(IPluginContext moduleContext)
        {
            var assembly = moduleContext?.Assembly;

            Register(assembly);
        }

        /// <summary>
        /// Registriert die Ressourcen eines Moduls
        /// </summary>
        /// <param name="assembly">Die Assembly, welche die Jobs enthält</param>
        private static void Register(Assembly assembly)
        {
            foreach (var job in assembly.GetTypes().Where(x => x.IsClass == true && x.IsSealed && x.GetInterface(typeof(IJob).Name) != null))
            {
                var minute = "*";
                var hour = "*";
                var day = "*";
                var month = "*";
                var weekday = "*";

                foreach (var customAttribute in job.CustomAttributes.Where(x => x.AttributeType == typeof(JobAttribute)))
                {
                    minute = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString();
                    hour = customAttribute.ConstructorArguments.Skip(1).FirstOrDefault().Value?.ToString();
                    day = customAttribute.ConstructorArguments.Skip(2).FirstOrDefault().Value?.ToString();
                    month = customAttribute.ConstructorArguments.Skip(3).FirstOrDefault().Value?.ToString();
                    weekday = customAttribute.ConstructorArguments.Skip(4).FirstOrDefault().Value?.ToString();
                }
            }
        }

        /// <summary>
        /// Ausführung des Planers beenden
        /// </summary>
        public static void ShutDown()
        {

        }
    }
}
