using System.Collections.Generic;
using WebExpress.Internationalization;
using WebExpress.WebCondition;
using WebExpress.WebModule;
using WebExpress.WebPlugin;

namespace WebExpress.UI.WebFragment
{
    public interface IFragmentContext : II18N
    {
        /// <summary>
        /// Returns the context of the associated plugin.
        /// </summary>
        IPluginContext PluginContext { get; }

        /// <summary>
        /// Returns the module context.
        /// </summary>
        IModuleContext ModuleContext { get; }

        /// <summary>
        /// Returns the conditions that must be met for the component to be active.
        /// </summary>
        ICollection<ICondition> Conditions { get; }

        /// <summary>
        /// Determines whether the component is created once and reused on each execution.
        /// </summary>
        bool Cache { get; }

        /// <summary>
        /// Returns the log for writing status messages to the console and to a log file.
        /// </summary>
        Log Log { get; }
    }
}
