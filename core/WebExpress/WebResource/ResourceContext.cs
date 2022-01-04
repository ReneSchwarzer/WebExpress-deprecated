using System.Collections.Generic;
using System.Reflection;
using WebExpress.WebApplication;
using WebExpress.WebCondition;
using WebExpress.WebModule;
using WebExpress.WebPlugin;
using WebExpress.Uri;

namespace WebExpress.WebResource
{
    public class ResourceContext : IResourceContext
    {
        /// <summary>
        /// Das Assembly, welches das Modul enthällt
        /// </summary>
        public Assembly Assembly { get; private set; }

        /// <summary>
        /// Liefert das zugehörige Plugins
        /// </summary>
        public IPluginContext Plugin { get; private set; }

        /// <summary>
        /// Liefert den Kontext der zugehörigen Anwendung 
        /// </summary>
        public IApplicationContext Application { get; private set; }

        /// <summary>
        /// Liefert das zugehörige Modul. 
        /// </summary>
        public IModuleContext Module { get; private set; }

        /// <summary>
        /// Liefert oder setzt den Kontextpfad Dieser wird in dem ContextPath des Servers eingehangen.
        /// </summary>
        public IUri ContextPath { get; private set; }

        /// <summary>
        /// Liefert oder setzt den Ressourcenkontext
        /// </summary>
        public IReadOnlyList<string> Context { get; internal set; }

        /// <summary>
        /// Liefert die Bedingungen, die erfüllt sein müssen, damit die Ressource aktiv ist
        /// </summary>
        public ICollection<ICondition> Conditions { get; internal set; } = new List<ICondition>();

        /// <summary>
        /// Bestimmt, ob die Ressource einmalig erstellt und bei jedem Aufruf wiederverwendet wird.
        /// </summary>
        public bool Cache { get; internal set; }

        /// <summary>
        /// Liefert oder setzt das Log, zum schreiben von Statusnachrichten auf die Konsole und in eine Log-Datei
        /// </summary>
        public Log Log { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="moduleContext">Der Modulkontext</param>
        internal ResourceContext(IModuleContext moduleContext)
        {
            Assembly = moduleContext?.Assembly;
            Plugin = moduleContext?.Plugin;
            Application = moduleContext?.Application;
            Module = moduleContext;
            ContextPath = moduleContext?.ContextPath;
            Log = moduleContext?.Log;
        }
    }
}
