using System.Collections.Generic;
using WebExpress.WebApplication;

namespace WebExpress.WebModule
{
    /// <summary>
    /// Anwendung -> ID -> Item
    /// </summary>
    internal class ModuleDictionary : Dictionary<IApplicationContext, Dictionary<string, ModuleItem>>
    {
    }
}
