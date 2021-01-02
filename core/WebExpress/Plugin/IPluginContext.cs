using System.Reflection;
using WebExpress.Uri;

namespace WebExpress.Plugin
{
    /// <summary>
    /// Der Kontext
    /// </summary>
    public interface IPluginContext
    {
        /// <summary>
        /// Das Assembly, welches das Plugin enthällt
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// Liefert oder setzt die ID
        /// </summary>
        string PluginID { get; }

        /// <summary>
        /// Liefert den Name des Plugins 
        /// </summary>
        string PluginName { get; }

        /// <summary>
        /// Liefert oder setzt den Hersteller
        /// </summary>
        string Manufacturer { get; }

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Liefert oder setzt die Version
        /// </summary>
        string Version { get; }

        /// <summary>
        /// Liefert oder setzt die Copyright-Informationen
        /// </summary>
        string Copyright { get; }

        /// <summary>
        /// Liefert das Icon des Plugins
        /// </summary>
        IUri Icon { get; }

        /// <summary>
        /// Liefert das Log, zum schreiben von Statusnachrichten auf die Konsole und in eine Log-Datei
        /// </summary>
        Log Log { get; }
    }
}
