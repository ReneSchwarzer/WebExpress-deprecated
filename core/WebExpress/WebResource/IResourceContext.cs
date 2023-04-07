using System.Collections.Generic;
using System.Reflection;
using WebExpress.WebUri;
using WebExpress.WebCondition;
using WebExpress.WebModule;
using WebExpress.WebPlugin;

namespace WebExpress.WebResource
{
    public interface IResourceContext
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
        /// Returns or sets the context name that provides the resource. The context name 
        /// is a string with a name (e.g. global, admin), which can be used by elements to 
        /// determine whether content and how content should be displayed.
        /// </summary>
        IReadOnlyList<string> Context { get; }

        /// <summary>
        /// Provides the conditions that must be met for the resource to be active.
        /// </summary>
        ICollection<ICondition> Conditions { get; }

        /// <summary>
        /// Returns or sets the resource id.
        /// </summary>
        string ResourceID { get; }

        /// <summary>
        /// Returns or sets the resource title.
        /// </summary>
        string ResourceTitle { get; }

        /// <summary>
        /// Determines whether the resource is created once and reused each time it is called.
        /// </summary>
        bool Cache { get; }

        /// <summary>
        /// Returns the log to write status messages to the console and to a log file.
        /// </summary>
        Log Log { get; }

        /// <summary>
        /// Returns the context path.
        /// </summary>
        IUri ContextPath { get; }
    }
}
