using System.Reflection;
using WebExpress.Uri;

namespace WebExpress.Application
{
    /// <summary>
    /// Der Anwendungskontext
    /// </summary>
    public interface IApplicationContext
    {
        /// <summary>
        /// Das Assembly, welches die Anwendung enthällt
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// Liefert die ID des Plugins
        /// </summary>
        string PluginID { get; }

        /// <summary>
        /// Liefert den AnwendungsID. 
        /// </summary>
        string ApplicationID { get; }

        /// <summary>
        /// Liefert den Anwendungsnamen. 
        /// </summary>
        string ApplicationName { get; }

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Liefert das Dokumentenverzeichnis. Dieser wird in dem AssetPath des Servers eingehangen.
        /// </summary>
        string AssetPath { get; }

        /// <summary>
        /// Liefert oder setzt den Kontextpfad Dieser wird in dem ContextPath des Servers eingehangen.
        /// </summary>
        IUri ContextPath { get; }

        /// <summary>
        /// Liefert oder setzt die IconUrl
        /// </summary>
        IUri Icon { get; }

        /// <summary>
        /// Liefert oder setzt das Log, zum schreiben von Statusnachrichten auf die Konsole und in eine Log-Datei
        /// </summary>
        Log Log { get; }
    }
}
