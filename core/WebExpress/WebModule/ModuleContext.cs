using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WebExpress.Uri;
using WebExpress.WebApplication;
using WebExpress.WebPlugin;
using static System.Net.Mime.MediaTypeNames;

namespace WebExpress.WebModule
{
    public class ModuleContext : IModuleContext
    {
        /// <summary>
        /// The assembly that contains the module.
        /// </summary>
        public Assembly Assembly { get; internal set; }

        /// <summary>
        /// Returns the context of the associated plugin.
        /// </summary>
        public IPluginContext PluginContext { get; internal set; }

        /// <summary>
        /// Returns the associated application ids.
        /// </summary>
        public IEnumerable<string> Applications { get; internal set; } = new List<string>();

        /// <summary>
        /// Returns the modul id.
        /// </summary>
        public string ModuleID { get; internal set; }

        /// <summary>
        /// Returns the module name.
        /// </summary>
        public string ModuleName { get; internal set; }

        /// <summary>
        /// Returns the description.
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// Returns the asset directory.
        /// </summary>
        public string AssetPath { get; internal set; }

        /// <summary>
        /// Returns the data directory. 
        /// </summary>
        public string DataPath { get; internal set; }

        /// <summary>
        /// Returns the context path.
        /// </summary>
        public IUri ContextPath { get; internal set; }

        /// <summary>
        /// Returns the icon uri.
        /// </summary>
        public IUri Icon { get; internal set; }

        /// <summary>
        /// Returns the log to write status messages to the console and to a log file.
        /// </summary>
        public Log Log { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ModuleContext()
        {
        }

        /// <summary>
        /// Determines the contexts of the applications referenced by the module.
        /// </summary>
        /// <returns>A list of application contexts associated with the module.</returns>
        public IEnumerable<IApplicationContext> GetApplicationContexts() 
        {
            return ApplicationManager.GetApplcations(Applications);
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
        /// Returns the asset directory. This is mounted in the asset directory of the application.
        /// </summary>
        /// <param name="applicationContext">The application context.</param>
        public string GetAssetPath(IApplicationContext applicationContext)
        {
            return Path.GetFullPath(Path.Combine(applicationContext.AssetPath, AssetPath));
        }

        /// <summary>
        /// Returns the data directory. This is mounted in the data directory of the application.
        /// </summary>
        /// <param name="applicationContext">The application context.</param>
        public string GetDataPath(IApplicationContext applicationContext)
        {
            return Path.GetFullPath(Path.Combine(applicationContext.DataPath, DataPath));
        }

        /// <summary>
        /// Returns a context path. This is hooked in the context paths of the linked application.
        /// </summary>
        /// <param name="applicationContext">The application context to determine the context path.</param>
        /// <returns>The currently valid context paths that address the module.</returns>
        public IUri GetContextPath(IApplicationContext applicationContext)
        {
            return UriRelative.Combine(applicationContext.ContextPath, ContextPath);
        }

        /// <summary>
        /// Returns the icon uri. This is mounted in the path of the application.
        /// </summary>
        /// <param name="applicationContext">The application context.</param>
        public IUri GetIcon(IApplicationContext applicationContext)
        {
            return new UriResource(applicationContext.ContextPath, ContextPath, Icon);
        }
    }
}
