using System.Collections.Generic;
using WebExpress.Application;

namespace WebExpress.Module
{
    /// <summary>
    /// Anwendung -> ID -> Item
    /// </summary>
    internal class ModuleDictionary : Dictionary<IApplicationContext, Dictionary<string, ModuleItem>>
    {
    }
}
