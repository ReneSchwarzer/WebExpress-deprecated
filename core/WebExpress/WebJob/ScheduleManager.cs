using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebExpress.WebAttribute;
using WebExpress.WebModule;
using WebExpress.WebPlugin;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.WebJob
{
    /// <summary>
    /// Processing of cyclic jobs
    /// </summary>
    public static class ScheduleManager
    {
        /// <summary>
        /// Thread termination.
        /// </summary>
        private static CancellationTokenSource TokenSource { get; } = new CancellationTokenSource();

        /// <summary>
        /// The clock for determining the execution of the crons.
        /// </summary>
        private static Clock Clock { get; } = new Clock();

        /// <summary>
        /// Returns or sets the reference to the context of the host.
        /// </summary>
        private static IHttpServerContext Context { get; set; }

        /// <summary>
        /// Returns the directory where the jobs are listed.
        /// </summary>
        private static ScheduleDictionary Dictionary { get; } = new ScheduleDictionary();

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The reference to the context of the host.</param>
        internal static void Initialization(IHttpServerContext context)
        {
            Context = context;

            Context.Log.Info(message: I18N("webexpress:schedulermanager.initialization"));
        }

        /// <summary>
        /// Registers the jobs from the specified plugins.
        /// </summary>
        /// <param name="pluginContexts">A list of contexts of plugins that contain job control.</param>
        public static void Register(IEnumerable<IPluginContext> pluginContexts)
        {
            foreach (var pluginContext in pluginContexts)
            {
                var assembly = pluginContext?.Assembly;

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

                    // determine the associated module 
                    var module = ModuleManager.GetModule(pluginContext, moduleID);
                    if (string.IsNullOrEmpty(moduleID))
                    {
                        // no module specified
                        pluginContext.Log.Warning(message: I18N("webexpress:schedulermanager.moduleless", id));
                    }
                    else if (module == null)
                    {
                        // module not found
                        pluginContext.Log.Warning(message: I18N("webexpress:schedulermanager.modulenotfound", moduleID));
                    }
                    else if (module.PluginContext != pluginContext)
                    {
                        // job is not part of the module
                        pluginContext.Log.Warning(message: I18N("webexpress:schedulermanager.wrongmodule", module.ModuleID, id));
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
                            Plugin = module.PluginContext,
                            Cron = new Cron(pluginContext.Host, minute, hour, day, month, weekday),
                            Log = module.Log
                        };

                        instance.Initialization(context);

                        dictItem.Add(new ScheduleDictionaryItem()
                        {
                            Context = context,
                            Type = job,
                            Instance = instance
                        });

                        pluginContext.Log.Info(message: I18N("webexpress:schedulermanager.job.register", module.ModuleID, id));
                    }
                }
            }
        }

        /// <summary>
        /// Executes the schedule.
        /// </summary>
        internal static void Execute()
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
        /// Run jobs on demand (concurrent execution).
        /// </summary>
        private static void Update()
        {
            foreach (var clock in Clock.Synchronize())
            {
                foreach (var item in Dictionary.Values.SelectMany(x => x))
                {
                    if (item.Context.Cron.Matching(Clock))
                    {
                        Context.Log.Info(message: I18N("webexpress:schedulermanager.job.process", item.Context.JobID));

                        Task.Factory.StartNew(() =>
                        {
                            item.Instance?.Process();
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Stop running the scheduler.
        /// </summary>
        public static void ShutDown()
        {
            TokenSource.Cancel();
        }
    }
}
