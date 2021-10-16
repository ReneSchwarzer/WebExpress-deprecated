using System.Reflection;

namespace WebExpress.UI.WebComponent
{
    public class ComponentContext : IComponentContext
    {
        /// <summary>
        /// Das Assembly, welches die Komponente enthällt
        /// </summary>
        public Assembly Assembly { get; internal set; }

        /// <summary>
        /// Liefert die ID des Plugins
        /// </summary>
        public string PluginID { get; internal set; }

        /// <summary>
        /// Liefert den AnwendungsID. 
        /// </summary>
        public string ApplicationID { get; internal set; }

        /// <summary>
        /// Liefert die ModulID. 
        /// </summary>
        public string ModuleID { get; internal set; }

        /// <summary>
        /// Liefert oder setzt das Log, zum schreiben von Statusnachrichten auf die Konsole und in eine Log-Datei
        /// </summary>
        public Log Log { get; internal set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentContext()
        {
        }
    }
}
