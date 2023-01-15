using System.Collections.Generic;
using System.Reflection;
using WebExpress.Uri;
using WebExpress.WebApplication;
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
        /// Returns the associated plugin.
        /// </summary>
        IPluginContext PluginContext { get; }

        /// <summary>
        /// Returns the corresponding module.
        /// </summary>
        IModuleContext ModuleContext { get; }

        /// <summary>
        /// Returns or sets the context name that provides the resource. The context name 
        /// is a string with a name (e.g. global, admin), which can be used by elements to 
        /// determine whether content and how content should be displayed.
        /// </summary>
        IReadOnlyList<string> ContentContext { get; }

        /// <summary>
        /// Provides the conditions that must be met for the resource to be active.
        /// </summary>
        ICollection<ICondition> Conditions { get; }

        /// <summary>
        /// Determines whether the resource is created once and reused each time it is called.
        /// </summary>
        bool Cache { get; }

        /// <summary>
        /// Returns the log to write status messages to the console and to a log file.
        /// </summary>
        Log Log { get; }

        /// <summary>
        /// Determines the contexts of the applications referenced by the module.
        /// </summary>
        /// <returns>A list of application contexts associated with the module.</returns>
        IEnumerable<IApplicationContext> GetApplicationContexts();

        /// <summary>
        /// Checks whether the application context is related to the module context.
        /// </summary>
        /// <returns>True if successful, false otherwise.</returns>
        bool LinkedWithApplication(IApplicationContext applicationContext);

        /// <summary>
        /// Returns a context path. This is hooked in the context paths of the linked modules.
        /// </summary>
        /// <param name="applicationContext">The application context to determine the context path.</param>
        /// <returns>The currently valid context paths that address the resource.</returns>
        IUri GetContextPath(IApplicationContext applicationContext);
    }
}
