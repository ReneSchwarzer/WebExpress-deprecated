using System.Reflection;
using WebExpress.Plugin;

namespace WebExpress.WebJob
{
    public interface IJobContext
    {
        /// <summary>
        /// Das Assembly, welches das Modul enthällt
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// Liefert den Kontext des zugehörigen Plugins
        /// </summary>
        IPluginContext Plugin { get; }

        /// <summary>
        /// Liefert die JobID. 
        /// </summary>
        string JobID { get; }

        /// <summary>
        /// Liefert die Cron-Werte
        /// </summary>
        Cron Cron { get; }

        /// <summary>
        /// Liefert oder setzt das Log, zum schreiben von Statusnachrichten auf die Konsole und in eine Log-Datei
        /// </summary>
        Log Log { get; }
    }
}
