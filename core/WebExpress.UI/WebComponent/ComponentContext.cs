using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using WebExpress.WebApplication;
using WebExpress.WebCondition;
using WebExpress.WebModule;
using WebExpress.WebPlugin;

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
        public Log Log { get; internal set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ComponentContext()
        {
        }
    }
}
