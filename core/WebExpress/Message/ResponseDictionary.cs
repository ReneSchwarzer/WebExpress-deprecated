using System;
using System.Collections.Generic;
using WebExpress.WebPlugin;

namespace WebExpress.Message
{
    /// <summary>
    /// key = plugin context
    /// value = { key = statuscode, value = status page type}
    /// </summary>
    public class ResponseDictionary : Dictionary<IPluginContext, Dictionary<int, Type>>
    {
    }
}
