namespace WebExpress.Plugin
{
    /// <summary>
    /// Repräsentiert ein Plugineintrag
    /// </summary>
    internal class PluginItem
    {
        /// <summary>
        /// Der zum Plugin zugehörige Kontext
        /// </summary>
        public IPluginContext Context { get; set; }

        /// <summary>
        /// Das Plugin
        /// </summary>
        public IPlugin Plugin { get; set; }
    }
}
