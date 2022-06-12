using System.Reflection;
using WebExpress.Uri;

namespace WebExpress.WebPlugin
{
    public class PluginContext : IPluginContext
    {
        /// <summary>
        /// Das Assembly, welches das Plugin enthällt
        /// </summary>
        public Assembly Assembly { get; internal set; }

        /// <summary>
        /// Liefert oder setzt die ID
        /// </summary>
        public string PluginId { get; internal set; }

        /// <summary>
        /// Liefert oder setzt den Name des Plugins 
        /// </summary>
        public string PluginName { get; internal set; }

        /// <summary>
        /// Liefert oder setzt die HerstellerID
        /// </summary>
        public string Manufacturer { get; internal set; }

        /// <summary>
        /// Liefert oder setzt die Copyright-Informationen
        /// </summary>
        public string Copyright { get; internal set; }

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// Liefert oder setzt die Version
        /// </summary>
        public string Version { get; internal set; }

        /// <summary>
        /// Liefert oder setzt die Lizenz-Informationen
        /// </summary>
        public string License { get; internal set; }

        /// <summary>
        /// Liefert das Icon des Plugins
        /// </summary>
        public IUri Icon { get; internal set; }

        /// <summary>
        /// Liefert das Log, zum schreiben von Statusnachrichten auf die Konsole und in eine Log-Datei
        /// </summary>
        public Log Log { get; internal set; }

        /// <summary>
        /// Liefert den Host-Kontext
        /// </summary>
        public IHttpServerContext Host { get; internal set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PluginContext()
        {
        }
    }
}
