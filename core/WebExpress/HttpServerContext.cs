using System.Collections.Generic;
using System.Reflection;
using WebExpress.Plugins;

namespace WebExpress
{
    /// <summary>
    /// Der Kontext des Http-Servers
    /// </summary>
    public class HttpServerContext
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="port">Der Port</param>
        /// <param name="plugins">Die Plugins</param>
        /// <param name="assetBaseFolder">Daten-Basisverzeichnis</param>
        /// <param name="configBaseFolder">Konfigurationserzeichnis</param>
        /// <param name="urlBasePath">Der Basispfad des Servers</param>
        /// <param name="log">Log</param>
        /// <param name="parser">Parser zur Substitution von Zeichenketten</param>
        public HttpServerContext
        (
            int port,
            string assetBaseFolder,
            string configBaseFolder,
            string urlBasePath,
            Log log
        )
        {
            var assembly = typeof(HttpServer).Assembly;
            Version = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;

            AssetBaseFolder = assetBaseFolder;
            ConfigBaseFolder = configBaseFolder;

            urlBasePath = !string.IsNullOrWhiteSpace(urlBasePath) ? urlBasePath.Trim() : string.Empty;

            if (!string.IsNullOrWhiteSpace(urlBasePath) && !urlBasePath.StartsWith("/"))
            {
                urlBasePath = "/" + urlBasePath;
            }

            if (!string.IsNullOrWhiteSpace(urlBasePath) && urlBasePath.EndsWith("/"))
            {
                urlBasePath = urlBasePath.Substring(0, urlBasePath.Length - 1);
            }

            UrlBasePath = urlBasePath;

            Log = log;
        }

        /// <summary>
        /// Liefert die Version des Plugins 
        /// </summary>
        public string Version { get; protected set; }

        /// <summary>
        /// Liefert oder setzt die Plugins
        /// </summary>
        public List<IPluginContext> Plugins { get; protected set; } = new List<IPluginContext>();

        /// <summary>
        /// Liefert oder setzt das Daten-Basisverzeichnis
        /// </summary>
        public string AssetBaseFolder { get; protected set; }

        /// <summary>
        /// Liefert oder setzt das Konfigurationserzeichnis
        /// </summary>
        public string ConfigBaseFolder { get; protected set; }

        /// <summary>
        /// Liefert den Basispfad 
        /// </summary>
        public string UrlBasePath { get; protected set; }

        /// <summary>
        /// Liefert oder setzt das Log, zum schreiben von Statusnachrichten auf die Konsole und in eine Log-Datei
        /// </summary>
        public Log Log { get; protected set; }
    }
}
