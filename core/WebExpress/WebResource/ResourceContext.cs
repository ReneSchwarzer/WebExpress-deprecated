using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebExpress.Uri;
using WebExpress.WebApplication;
using WebExpress.WebComponent;
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
        /// Returns the associated plugin.
        /// </summary>
        public IPluginContext PluginContext { get; private set; }

        /// <summary>
        /// Returns the corresponding module.
        /// </summary>
        public IModuleContext ModuleContext { get; private set; }

        /// <summary>
        /// Returns or sets the context name that provides the resource. The context name 
        /// is a string with a name (e.g. global, admin), which can be used by elements to 
        /// determine whether content and how content should be displayed.
        /// </summary>
        public IReadOnlyList<string> ContentContext { get; internal set; }

        /// <summary>
        /// Returns the conditions that must be met for the resource to be active.
        /// </summary>
        public ICollection<ICondition> Conditions { get; internal set; } = new List<ICondition>();

        /// <summary>
        /// Returns whether the resource is created once and reused each time it is called.
        /// </summary>
        public bool Cache { get; internal set; }

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

        /// <summary>
        /// Determines the contexts of the applications referenced by the module.
        /// </summary>
        /// <returns>A list of application contexts associated with the module.</returns>
        public IEnumerable<IApplicationContext> GetApplicationContexts()
        {
            return ComponentManager.ApplicationManager.GetApplcations(ModuleContext.Applications);
        }

        /// <summary>
        /// Checks whether the application context is related to the module context.
        /// </summary>
        /// <returns>True if successful, false otherwise.</returns>
        public bool LinkedWithApplication(IApplicationContext applicationContext)
        {
            return GetApplicationContexts().Where(x => x == applicationContext).Any();
        }

        /// <summary>
        /// Returns a context path. This is hooked in the context paths of the linked modules.
        /// </summary>
        /// <param name="applicationContext">The application context to determine the context path.</param>
        /// <returns>The currently valid context paths that address the resource.</returns>
        public IUri GetContextPath(IApplicationContext applicationContext)
        {
            return ModuleContext.GetContextPath(applicationContext);                
        }
    }
}
