using System.Collections.Generic;
using WebExpress.Module;
using WebExpress.Schedule;

namespace WebExpress.Scheduler
{
    internal class ScheduleDictionary : Dictionary<IModuleContext, List<ScheduleItem>>
    {
    }
}
