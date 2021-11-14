using System.Globalization;
using System.Reflection;
using WebExpress.Application;
using WebExpress.Module;
using WebExpress.Plugin;

namespace WebExpress.UI.WebComponent
{
    public class ComponentContext : IComponentContext
    {
        /// <summary>
        /// Das Assembly, welches die Komponente enthällt
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
        /// Liefert das zugehörige Modul
        /// </summary>
        public IModuleContext Module { get; internal set; }

        /// <summary>
        /// Liefert die Kultur
        /// </summary>
        public CultureInfo Culture { get; set; }

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
