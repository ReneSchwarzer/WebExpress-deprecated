using System.Reflection;
using WebExpress.Plugin;

namespace WebExpress.WebJob
{
    public class JobContext : IJobContext
    {
        /// <summary>
        /// Das Assembly, welches das Modul enthällt
        /// </summary>
        public Assembly Assembly { get; internal set; }

        /// <summary>
        /// Liefert den Kontext des zugehörigen Plugins
        /// </summary>
        public IPluginContext Plugin { get; internal set; }

        /// <summary>
        /// Liefert die JobID. 
        /// </summary>
        public string JobID { get; internal set; }

        /// <summary>
        /// Liefert die Cron-Werte
        /// </summary>
        public Cron Cron { get; internal set; }

        /// <summary>
        /// Liefert oder setzt das Log, zum schreiben von Statusnachrichten auf die Konsole und in eine Log-Datei
        /// </summary>
        public Log Log { get; internal set; }
    }
}
