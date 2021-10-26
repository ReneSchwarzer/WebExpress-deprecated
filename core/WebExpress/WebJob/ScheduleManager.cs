using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebExpress.Attribute;
using WebExpress.Module;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.WebJob
{
    /// <summary>
    /// Verarbeitung von zyklischen Jobs
    /// </summary>
    public static class ScheduleManager
    {
        /// <summary>
        /// Threadbeendigung des ScheduleManager
        /// </summary>
        private static CancellationTokenSource TokenSource { get; } = new CancellationTokenSource();

        /// <summary>
        /// Die Uhr zum Ausführungsermittlung der Crons
        /// </summary>
        private static Clock Clock { get; } = new Clock();

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
            foreach (var module in ModuleManager.Modules)
            {
                Register(module);
            }
        }

        /// <summary>
        /// Registriert die Ressourcen eines Moduls
        /// </summary>
        /// <param name="moduleContext">Der Kontext des Moduls, welches die Ressourcen enthält</param>
        public static void Register(IModuleContext moduleContext)
        {
            var assembly = moduleContext?.Assembly;

            foreach (var job in assembly.GetTypes().Where(x => x.IsClass == true && x.IsSealed && x.GetInterface(typeof(IJob).Name) != null))
            {
                var minute = "*";
                var hour = "*";
                var day = "*";
                var month = "*";
                var weekday = "*";
                var moduleID = string.Empty;
                var id = job.Name;

                foreach (var customAttribute in job.CustomAttributes.Where(x => x.AttributeType == typeof(JobAttribute)))
                {
                    minute = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString();
                    hour = customAttribute.ConstructorArguments.Skip(1).FirstOrDefault().Value?.ToString();
                    day = customAttribute.ConstructorArguments.Skip(2).FirstOrDefault().Value?.ToString();
                    month = customAttribute.ConstructorArguments.Skip(3).FirstOrDefault().Value?.ToString();
                    weekday = customAttribute.ConstructorArguments.Skip(4).FirstOrDefault().Value?.ToString();
                }

                foreach (var customAttribute in job.CustomAttributes.Where(x => x.AttributeType == typeof(ModuleAttribute)))
                {
                    moduleID = customAttribute.ConstructorArguments.FirstOrDefault().Value?.ToString().ToLower();
                }

                // Zugehöriges Modul ermitteln. 
                var module = ModuleManager.GetModule(moduleContext.Application, moduleID);
                if (string.IsNullOrEmpty(moduleID))
                {
                    // Es wurde kein Modul angebgeben
                    Context.Log.Warning(message: I18N("webexpress:schedulermanager.moduleless"), args: id);
                }
                else if (module == null)
                {
                    // Modul wurde nicht gefunden 
                    Context.Log.Warning(message: I18N("webexpress:schedulermanager.modulenotfound"), args: moduleID);
                }
                else if (!module.ModuleID.Equals(moduleContext.ModuleID, StringComparison.OrdinalIgnoreCase))
                {
                    // Ressource gehört nicht zum Modul
                    Context.Log.Warning(message: I18N("webexpress:schedulermanager.wrongmodule"), args: new object[] { module.Application.ApplicationID, module.ModuleID, id });
                }
                else
                {
                    // Job registrieren
                    if (!Dictionary.ContainsKey(module))
                    {
                        Dictionary.Add(module, new List<ScheduleDictionaryItem>());
                    }

                    var dictItem = Dictionary[module];

                    var instance = job?.Assembly.CreateInstance(job?.FullName) as IJob;
                    var context = new JobContext()
                    {
                        Assembly = assembly,
                        JobID = id,
                        Plugin = module.Plugin,
                        Cron = new Cron(Context, minute, hour, day, month, weekday),
                        Log = module.Log
                    };

                    instance.Initialization(context);

                    dictItem.Add(new ScheduleDictionaryItem()
                    {
                        Context = context,
                        Type = job,
                        Instance = instance
                    });

                    Context.Log.Info(message: I18N("webexpress:schedulermanager.job.register"), args: new object[] { module.Application.ApplicationID, module.ModuleID, id });
                }
            }
        }

        /// <summary>
        /// Fürt die Anwendungen aus
        /// </summary>
        internal static void Boot()
        {
            Task.Factory.StartNew(() =>
            {
                while (!TokenSource.IsCancellationRequested)
                {
                    Update();

                    var secendsLeft = 60 - DateTime.Now.Second;
                    Thread.Sleep(secendsLeft * 1000);
                }

            }, TokenSource.Token);
        }

        /// <summary>
        /// Prüft ob Jobs ausgeführt werden müssen
        /// Wird nebenläufig ausgeführt
        /// </summary>
        private static void Update()
        {
            foreach (var clock in Clock.Synchronize())
            {
                foreach (var item in Dictionary.Values.SelectMany(x => x))
                {
                    if (item.Context.Cron.Matching(Clock))
                    {
                        Context.Log.Info(message: I18N("webexpress:schedulermanager.job.process"), args: item.Context.JobID);

                        Task.Factory.StartNew(() =>
                        {
                            item.Instance?.Process();
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Ausführung des Planers beenden
        /// </summary>
        public static void ShutDown()
        {
            TokenSource.Cancel();
        }
    }
}
