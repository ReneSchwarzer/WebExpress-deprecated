using System.Reflection;

namespace WebExpress.UI.WebComponent
{
    public interface IComponentContext
    {
        /// <summary>
        /// Das Assembly, welches die Komponente enthällt
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
        /// Liefert oder setzt das Log, zum schreiben von Statusnachrichten auf die Konsole und in eine Log-Datei
        /// </summary>
        Log Log { get; }
    }
}
