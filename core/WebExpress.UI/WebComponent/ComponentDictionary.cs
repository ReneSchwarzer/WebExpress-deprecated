using System.Collections.Generic;
using WebExpress.WebPlugin;

namespace WebExpress.UI.WebComponent
{
    /// <summary>
    /// component directory
    /// key = plugin context 
    /// value { key = application id, value = { key = section:context, value = component item } }
    /// </summary>
    internal class ComponentDictionary : Dictionary<IPluginContext, Dictionary<string, ComponentDictionaryItem>>
    {
    }
}
