using System.Collections.Generic;
using WebExpress.Module;

namespace WebExpress.WebJob
{
    internal class ScheduleDictionary : Dictionary<IModuleContext, List<ScheduleDictionaryItem>>
    {
    }
}
