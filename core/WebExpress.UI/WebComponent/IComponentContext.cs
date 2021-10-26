using System.Reflection;
using WebExpress.Application;
using WebExpress.Internationalization;
using WebExpress.Module;
using WebExpress.Plugin;

namespace WebExpress.UI.WebComponent
{
    public interface IComponentContext : II18N
    {
        /// <summary>
        /// Das Assembly, welches die Komponente enthällt
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
        /// Liefert das zugehörige Modul
        /// </summary>
        IModuleContext Module { get; }

        /// <summary>
        /// Liefert oder setzt das Log, zum schreiben von Statusnachrichten auf die Konsole und in eine Log-Datei
        /// </summary>
        Log Log { get; }
    }
}
