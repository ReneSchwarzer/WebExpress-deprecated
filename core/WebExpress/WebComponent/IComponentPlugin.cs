﻿using System.Collections.Generic;
using WebExpress.WebPlugin;

namespace WebExpress.WebComponent
{
    /// <summary>
    /// Interface of the manager classes.
    /// </summary>
    public interface IComponentPlugin : IComponent
    {
        /// <summary>
        /// Discovers and registers entries from the specified plugin.
        /// </summary>
        /// <param name="pluginContext">A context of a plugin whose elements are to be registered.</param>
        void Register(IPluginContext pluginContext);

        /// <summary>
        /// Discovers and registers entries from the specified plugin.
        /// </summary>
        /// <param name="pluginContexts">A list with plugin contexts that contain the elements.</param>
        void Register(IEnumerable<IPluginContext> pluginContexts);

        /// <summary>
        /// Removes all elemets associated with the specified plugin context.
        /// </summary>
        /// <param name="pluginContext">The context of the plugin that contains the elemets to remove.</param>
        void Remove(IPluginContext pluginContext);

        /// <summary>
        /// Information about the component is collected and prepared for output in the log.
        /// </summary>
        /// <param name="pluginContext">The context of the plugin.</param>
        /// <param name="output">A list of log entries.</param>
        /// <param name="deep">The shaft deep.</param>
        void PrepareForLog(IPluginContext pluginContext, IList<string> output, int deep);
    }
}
