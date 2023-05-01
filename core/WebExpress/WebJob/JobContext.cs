﻿using WebExpress.WebModule;
using WebExpress.WebPlugin;

namespace WebExpress.WebJob
{
    public class JobContext : IJobContext
    {
        /// <summary>
        /// Returns the associated plugin context.
        /// </summary>
        public IPluginContext PluginContext { get; internal set; }

        /// <summary>
        /// Returns the corresponding module context.
        /// </summary>
        public IModuleContext ModuleContext { get; internal set; }

        /// <summary>
        /// Returns the job id. 
        /// </summary>
        public string JobID { get; internal set; }

        /// <summary>
        /// Returns the cron-object.
        /// </summary>
        public Cron Cron { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="moduleContext">The module context.</param>
        internal JobContext(IModuleContext moduleContext)
        {
            PluginContext = moduleContext?.PluginContext;
            ModuleContext = moduleContext;
        }
    }
}
