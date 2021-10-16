using System.Collections.Generic;
using System.Reflection;
using WebExpress.Module;
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
        /// Liefert die ID des Plugins
        /// </summary>
        public string PluginID { get; private set; }

        /// <summary>
        /// Liefert den AnwendungsID. 
        /// </summary>
        public string ApplicationID { get; private set; }

        /// <summary>
        /// Liefert die ModulID. 
        /// </summary>
        public string ModuleID { get; private set; }

        /// <summary>
        /// Liefert oder setzt den Kontextpfad Dieser wird in dem ContextPath des Servers eingehangen.
        /// </summary>
        public IUri ContextPath { get; private set; }

        /// <summary>
        /// Liefert oder setzt den Ressourcenkontext
        /// </summary>
        public IReadOnlyList<string> Context { get; internal set; }

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
            PluginID = moduleContext?.PluginID;
            ApplicationID = moduleContext?.ApplicationID;
            ModuleID = moduleContext?.ModuleID;
            ContextPath = moduleContext?.ContextPath;
            Log = moduleContext?.Log;
        }
    }
}
