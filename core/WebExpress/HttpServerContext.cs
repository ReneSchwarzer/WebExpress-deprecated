using WebExpress.Plugins;

namespace WebExpress
{
    /// <summary>
    /// Der Kontext des Http-Servers
    /// </summary>
    public class HttpServerContext : IPluginContext
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="assetBaseFolder">Daten-Basisverzeichnis</param>
        /// <param name="configBaseFolder">Konfigurationserzeichnis</param>
        /// <param name="log">Log</param>
        /// <param name="parser">Parser zur Substitution von Zeichenketten</param>
        public HttpServerContext(string assetBaseFolder, string configBaseFolder, Log log)
        {
            AssetBaseFolder = assetBaseFolder;
            ConfigBaseFolder = configBaseFolder;
            Log = log;
        }

        /// <summary>
        /// Liefert oder setzt das Daten-Basisverzeichnis
        /// </summary>
        public string AssetBaseFolder { get; protected set; }

        /// <summary>
        /// Liefert oder setzt das Konfigurationserzeichnis
        /// </summary>
        public string ConfigBaseFolder { get; protected set; }

        /// <summary>
        /// Liefert oder setzt das Log, zum schreiben von Statusnachrichten auf die Konsole und in eine Log-Datei
        /// </summary>
        public Log Log { get; protected set; }
    }
}
