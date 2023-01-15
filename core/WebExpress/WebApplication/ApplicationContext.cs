using System.Collections.Generic;
using System.Reflection;
using WebExpress.Uri;
using WebExpress.WebPlugin;

namespace WebExpress.WebApplication
{
    public class ApplicationContext : IApplicationContext
    {
        /// <summary>
        /// The assembly containing the application.
        /// </summary>
        public Assembly Assembly { get; internal set; }

        /// <summary>
        /// Returns the context of the associated plugin.
        /// </summary>
        public IPluginContext PluginContext { get; internal set; }

        /// <summary>
        /// Returns the application id.
        /// </summary>
        public string ApplicationID { get; internal set; }

        /// <summary>
        /// Returns the application name.
        /// </summary>
        public string ApplicationName { get; internal set; }

        /// <summary>
        /// Returns or sets the description.
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// Returns the options used.
        /// </summary>
        public IReadOnlyCollection<string> Options { get; internal set; }

        /// <summary>
        /// Returns the asset directory. This is mounted in the asset directory of the server.
        /// </summary>
        public string AssetPath { get; internal set; }

        /// <summary>
        /// Returns the data directory. This is mounted in the data directory of the server.
        /// </summary>
        public string DataPath { get; internal set; }

        /// <summary>
        /// Returns the context path. This is mounted in the context path of the server.
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
        public ApplicationContext()
        {
        }

        /// <summary>
        /// Conversion of the apllication context into its string representation.
        /// </summary>
        /// <returns>The string that uniquely represents the application.</returns>
        public override string ToString()
        {
            return ApplicationID;
        }
    }
}
