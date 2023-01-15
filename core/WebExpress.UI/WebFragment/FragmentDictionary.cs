using System.Collections.Generic;
using WebExpress.WebPlugin;

namespace WebExpress.UI.WebFragment
{
    /// <summary>
    /// component directory
    /// key = plugin context 
    /// value { key = application id, value = { key = section:context, value = component item } }
    /// </summary>
    internal class FragmentDictionary : Dictionary<IPluginContext, Dictionary<string, FragmentDictionaryItem>>
    {
    }
}
