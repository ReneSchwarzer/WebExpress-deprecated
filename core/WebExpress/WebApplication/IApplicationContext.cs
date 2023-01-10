using System.Collections.Generic;
using System.Reflection;
using WebExpress.Uri;
using WebExpress.WebPlugin;

namespace WebExpress.WebApplication
{
    /// <summary>
    /// The application context.
    /// </summary>
    public interface IApplicationContext
    {
        /// <summary>
        /// The assembly that contains the application.
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// Provides the context of the associated plugin.
        /// </summary>
        IPluginContext PluginContext { get; }

        /// <summary>
        /// Returns the application id.
        /// </summary>
        string ApplicationID { get; }

        /// <summary>
        /// Returns the application name.
        /// </summary>
        string ApplicationName { get; }

        /// <summary>
        /// Provides the description.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Provides the options used.
        /// </summary>
        IReadOnlyCollection<string> Options { get; }

        /// <summary>
        /// Returns the asset directory. This is mounted in the asset directory of the server.
        /// </summary>
        string AssetPath { get; }

        /// <summary>
        /// Returns the data directory. This is mounted in the data directory of the server.
        /// </summary>
        string DataPath { get; }

        /// <summary>
        /// Returns the context path. This is mounted in the context path of the server.
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
    }
}
