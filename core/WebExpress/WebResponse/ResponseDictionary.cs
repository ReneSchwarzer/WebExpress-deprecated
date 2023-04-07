using System.Collections.Generic;
using WebExpress.WebPlugin;

namespace WebExpress.WebResponse
{
    /// <summary>
    /// key = plugin context
    /// value = { key = statuscode, value = status page item}
    /// </summary>
    public class ResponseDictionary : Dictionary<IPluginContext, Dictionary<int, ResponseItem>>
    {
    }
}
