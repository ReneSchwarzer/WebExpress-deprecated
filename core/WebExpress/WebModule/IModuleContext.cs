using System.Collections.Generic;
using System.Reflection;
using WebExpress.Uri;
using WebExpress.WebApplication;
using WebExpress.WebPlugin;

namespace WebExpress.WebModule
{
    public interface IModuleContext
    {
        /// <summary>
        /// The assembly that contains the module.
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// Returns the context of the associated plugin.
        /// </summary>
        IPluginContext PluginContext { get; }

        /// <summary>
        /// Returns the associated application context.
        /// </summary>
        IApplicationContext ApplicationContext { get; }

        /// <summary>
        /// Returns the modul id.
        /// </summary>
        string ModuleID { get; }

        /// <summary>
        /// Returns the module name.
        /// </summary>
        string ModuleName { get; }

        /// <summary>
        /// Returns the description.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Returns the asset directory.
        /// </summary>
        string AssetPath { get; }

        /// <summary>
        /// Returns the data directory.
        /// </summary>
        string DataPath { get; }

        /// <summary>
        /// Returns the context path.
        /// </summary>
        IUri ContextPath { get; }

        /// <summary>
        /// Returns the icon uri.
        /// </summary>
        IUri Icon { get; }

        /// <summary>
        /// Returns the log to write status messages to the console and to a log file.
        /// </summary>
        Log Log { get; }

        ///// <summary>
        ///// Determines the contexts of the applications referenced by the module.
        ///// </summary>
        ///// <returns>A list of application contexts associated with the module.</returns>
        //IEnumerable<IApplicationContext> GetApplicationContexts();

        ///// <summary>
        ///// Checks whether the application context is related to the module context.
        ///// </summary>
        ///// <returns>True if successful, false otherwise.</returns>
        //bool LinkedWithApplication(IApplicationContext applicationContext);

        ///// <summary>
        ///// Returns the asset directory. This is mounted in the asset directory of the application.
        ///// </summary>
        ///// <param name="applicationContext">The application context.</param>
        //string GetAssetPath(IApplicationContext applicationContext);

        ///// <summary>
        ///// Returns the data directory. This is mounted in the data directory of the application.
        ///// </summary>
        ///// <param name="applicationContext">The application context.</param>
        //string GetDataPath(IApplicationContext applicationContext);

        ///// <summary>
        ///// Returns a context path. This is hooked in the context paths of the linked application.
        ///// </summary>
        ///// <param name="applicationContext">The application context to determine the context path.</param>
        ///// <returns>The currently valid context paths that address the module.</returns>
        //IUri GetContextPath(IApplicationContext applicationContext);

        ///// <summary>
        ///// Returns the icon uri. This is mounted in the context path of the application.
        ///// </summary>
        ///// <param name="applicationContext">The application context.</param>
        //IUri GetIcon(IApplicationContext applicationContext);
    }
}
