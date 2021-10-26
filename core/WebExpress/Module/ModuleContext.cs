using System.Reflection;
using WebExpress.Application;
using WebExpress.Plugin;
using WebExpress.Uri;

namespace WebExpress.Module
{
    public class ModuleContext : IModuleContext
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
        /// Liefert den Kontext der zugehörigen Anwendung 
        /// </summary>
        public IApplicationContext Application { get; internal set; }

        /// <summary>
        /// Liefert die ModulID. 
        /// </summary>
        public string ModuleID { get; internal set; }

        /// <summary>
        /// Liefert den Modulnamen. 
        /// </summary>
        public string ModuleName { get; internal set; }

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// Liefert das Dokumentenverzeichnis. Dieser wird in dem AssetPath des Servers eingehangen.
        /// </summary>
        public string AssetPath { get; internal set; }

        /// <summary>
        /// Liefert oder setzt den Kontextpfad Dieser wird in dem ContextPath des Servers eingehangen.
        /// </summary>
        public IUri ContextPath { get; internal set; }

        /// <summary>
        /// Liefert oder setzt die IconUrl
        /// </summary>
        public IUri Icon { get; internal set; }

        /// <summary>
        /// Liefert oder setzt das Log, zum schreiben von Statusnachrichten auf die Konsole und in eine Log-Datei
        /// </summary>
        public Log Log { get; internal set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ModuleContext()
        {
        }
    }
}
