using System.Reflection;
using WebExpress.WebApplication;
using WebExpress.Internationalization;
using WebExpress.WebModule;
using WebExpress.WebPlugin;
using System.Collections.Generic;
using WebExpress.WebCondition;

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
        /// Liefert die Bedingungen, die erfüllt sein müssen, damit die Ressource aktiv ist
        /// </summary>
        ICollection<ICondition> Conditions { get; }

        /// <summary>
        /// Bestimmt, ob die Ressource einmalig erstellt und bei jedem Aufruf wiederverwendet wird.
        /// </summary>
        bool Cache { get; }

        /// <summary>
        /// Liefert oder setzt das Log, zum schreiben von Statusnachrichten auf die Konsole und in eine Log-Datei
        /// </summary>
        Log Log { get; }
    }
}
