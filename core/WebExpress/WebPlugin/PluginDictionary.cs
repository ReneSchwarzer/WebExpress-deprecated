using System.Collections.Generic;

namespace WebExpress.WebPlugin
{
    /// <summary>
    /// Verzeichnis über die registrieten Plugins
    /// Key = PluginID
    /// Value = Plugin-Metadaten
    /// </summary>
    internal class PluginDictionary : Dictionary<string, PluginItem>
    {
    }
}
