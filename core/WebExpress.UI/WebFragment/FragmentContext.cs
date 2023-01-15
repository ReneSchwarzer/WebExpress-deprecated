using System.Collections.Generic;
using System.Globalization;
using WebExpress.WebCondition;
using WebExpress.WebPlugin;

namespace WebExpress.UI.WebFragment
{
    public class FragmentContext : IFragmentContext
    {
        /// <summary>
        /// Returns the context of the associated plugin.
        /// </summary>
        public IPluginContext PluginContext { get; internal set; }

        /// <summary>
        /// Returns the application id or '*' for all applications.
        /// </summary>
        public string ApplicationID { get; internal set; }

        /// <summary>
        /// Returns the culture.
        /// </summary>
        public CultureInfo Culture { get; set; }

        /// <summary>
        /// Returns the conditions that must be met for the component to be active.
        /// </summary>
        public ICollection<ICondition> Conditions { get; internal set; } = new List<ICondition>();

        /// <summary>
        /// Determines whether the component is created once and reused on each execution.
        /// </summary>
        public bool Cache { get; internal set; }

        /// <summary>
        /// Returns the log for writing status messages to the console and to a log file.
        /// </summary>
        public Log Log { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public FragmentContext()
        {
        }
    }
}
