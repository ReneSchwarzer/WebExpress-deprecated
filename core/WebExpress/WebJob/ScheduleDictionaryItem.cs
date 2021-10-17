using System;

namespace WebExpress.WebJob
{
    /// <summary>
    /// Repräsentiert ein Termineintrag im Terminausführungsverzeichnis
    /// </summary>
    internal class ScheduleDictionaryItem
    {
        /// <summary>
        /// Der zum Modul zugehörige Kontext
        /// </summary>
        public IJobContext Context { get; set; }

        /// <summary>
        /// Liefert oder setzt den Typ
        /// </summary>
        public Type Type { get; internal set; }

        /// <summary>
        /// Liefert die Instance
        /// </summary>
        public IJob Instance { get; internal set; }
    }
}
