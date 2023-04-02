using System.Reflection;
using WebExpress.WebModule;
using WebExpress.WebPlugin;

namespace WebExpress.WebJob
{
    public interface IJobContext
    {
        /// <summary>
        /// The assembly that contains the module.
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// Returns the associated plugin context.
        /// </summary>
        IPluginContext PluginContext { get; }

        /// <summary>
        /// Returns the corresponding module context.
        /// </summary>
        IModuleContext ModuleContext { get; }

        /// <summary>
        /// Returns the job id. 
        /// </summary>
        string JobID { get; }

        /// <summary>
        /// Returns the cron-object.
        /// </summary>
        Cron Cron { get; }

        /// <summary>
        /// Returns the log to write status messages to the console and to a log file.
        /// </summary>
        Log Log { get; }
    }
}
