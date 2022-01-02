using System.Reflection;
using WebExpress.WebApplication;
using WebExpress.WebPlugin;
using WebExpress.Uri;

namespace WebExpress.WebModule
{
    public interface IModuleContext
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
        /// Liefert den Kontext der zugehörigen Anwendung 
        /// </summary>
        IApplicationContext Application { get; }

        /// <summary>
        /// Liefert die ModulID. 
        /// </summary>
        string ModuleID { get; }

        /// <summary>
        /// Liefert den Modulnamen. 
        /// </summary>
        string ModuleName { get; }

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
