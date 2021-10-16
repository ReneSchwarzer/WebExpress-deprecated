using System;
using WebExpress.Module;
using WebExpress.Scheduler;

namespace WebExpress.Schedule
{
    /// <summary>
    /// Repräsentiert ein Termineintrag im Terminausführungsverzeichnis
    /// </summary>
    internal class ScheduleItem
    {
        /// <summary>
        /// Der zum Modul zugehörige Kontext
        /// </summary>
        public ISchedulerContext Context { get; set; }

        /// <summary>
        /// Das Modul
        /// </summary>
        public IModule Module { get; set; }

        /// <summary>
        /// Liefert oder setzt den Typ
        /// </summary>
        public Type Type { get; internal set; }
    }
}
