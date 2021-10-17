using System.Reflection;

namespace WebExpress.WebJob
{
    public interface IJobContext
    {
        /// <summary>
        /// Das Assembly, welches das Modul enthällt
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// Liefert die ID des Plugins
        /// </summary>
        string PluginID { get; }

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
