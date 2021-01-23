using System.IO;
using System.Reflection;
using WebExpress.Uri;

namespace WebExpress.Application
{
    public class ApplicationContext : IApplicationContext
    {
        /// <summary>
        /// Das Assembly, welches die Anwednung enthällt
        /// </summary>
        public Assembly Assembly { get; internal set;  }

        /// <summary>
        /// Liefert die ID des Plugins
        /// </summary>
        public string PluginID { get; internal set; }

        /// <summary>
        /// Liefert die AnwendungsID. 
        /// </summary>
        public string ApplicationID { get; internal set; }

        /// <summary>
        /// Liefert den Anwendungsnamen. 
        /// </summary>
        public string ApplicationName { get; internal set; }
        
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
        public ApplicationContext()
        {
        }
    }
}
