using System.Collections.Generic;
using System.Reflection;
using WebExpress.WebUri;
using WebExpress.WebCondition;
using WebExpress.WebModule;
using WebExpress.WebPlugin;

namespace WebExpress.WebResource
{
    public class ResourceContext : IResourceContext
    {
        /// <summary>
        /// The assembly that contains the module
        /// </summary>
        public Assembly Assembly { get; private set; }

        /// <summary>
        /// Returns the associated plugin context.
        /// </summary>
        public IPluginContext PluginContext { get; private set; }

        /// <summary>
        /// Returns the corresponding module context.
        /// </summary>
        public IModuleContext ModuleContext { get; private set; }

        /// <summary>
        /// Returns or sets the context name that provides the resource. The context name 
        /// is a string with a name (e.g. global, admin), which can be used by elements to 
        /// determine whether content and how content should be displayed.
        /// </summary>
        public IReadOnlyList<string> Context { get; internal set; }

        /// <summary>
        /// Returns the conditions that must be met for the resource to be active.
        /// </summary>
        public ICollection<ICondition> Conditions { get; internal set; } = new List<ICondition>();

        /// <summary>
        /// Returns or sets the resource id.
        /// </summary>
        public string ResourceID { get; internal set; }

        /// <summary>
        /// Returns or sets the resource title.
        /// </summary>
        public string ResourceTitle { get; internal set; }

        /// <summary>
        /// Returns whether the resource is created once and reused each time it is called.
        /// </summary>
        public bool Cache { get; internal set; }

        /// <summary>
        /// Returns the context path.
        /// </summary>
        public IUri ContextPath { get; internal set; }

        /// <summary>
        /// Returns the log to write status messages to the console and to a log file.
        /// </summary>
        public Log Log { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="moduleContext">The module context.</param>
        internal ResourceContext(IModuleContext moduleContext)
        {
            Assembly = moduleContext?.Assembly;
            PluginContext = moduleContext?.PluginContext;
            ModuleContext = moduleContext;
            Log = moduleContext?.Log;
        }
    }
}
