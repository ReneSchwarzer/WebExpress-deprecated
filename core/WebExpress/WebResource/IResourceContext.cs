using System.Collections.Generic;
using System.Reflection;
using WebExpress.Uri;

namespace WebExpress.WebResource
{
    public interface IResourceContext
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
        /// Liefert den AnwendungsID. 
        /// </summary>
        string ApplicationID { get; }

        /// <summary>
        /// Liefert die ModulID. 
        /// </summary>
        string ModuleID { get; }

        /// <summary>
        /// Liefert oder setzt den Kontextpfad Dieser wird in dem ContextPath des Servers eingehangen.
        /// </summary>
        IUri ContextPath { get; }

        /// <summary>
        /// Liefert oder setzt den Ressourcenkontext
        /// </summary>
        IReadOnlyList<string> Context { get; }

        /// <summary>
        /// Liefert oder setzt das Log, zum schreiben von Statusnachrichten auf die Konsole und in eine Log-Datei
        /// </summary>
        Log Log { get; }
    }
}
