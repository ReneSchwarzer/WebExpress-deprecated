using System;

namespace WebExpress.WebPlugin
{
    /// <summary>
    /// Repräsentiert ein Plugineintrag
    /// </summary>
    internal class PluginItem
    {
        /// <summary>
        /// Returns the plugin class.
        /// </summary>
        public Type PluginClass { get; internal set; }

        /// <summary>
        /// Der zum Plugin zugehörige Kontext
        /// </summary>
        public IPluginContext PluginContext { get; set; }

        /// <summary>
        /// Das Plugin
        /// </summary>
        public IPlugin Plugin { get; set; }
    }
}
