using System.Globalization;
using System.Reflection;
using WebExpress.Uri;

namespace WebExpress
{
    /// <summary>
    /// Der Kontext des Http-Servers
    /// </summary>
    public class HttpServerContext : IHttpServerContext
    {
        /// <summary>
        /// Liefert den Port
        /// </summary>
        public int Port { get; protected set; }

        /// <summary>
        /// Liefert die Version des Plugins 
        /// </summary>
        public string Version { get; protected set; }

        /// <summary>
        /// Liefert oder setzt das Daten-Basisverzeichnis
        /// </summary>
        public string AssetPath { get; protected set; }

        /// <summary>
        /// Liefert oder setzt das Konfigurationserzeichnis
        /// </summary>
        public string ConfigPath { get; protected set; }

        /// <summary>
        /// Liefert den Basispfad 
        /// </summary>
        public IUri ContextPath { get; protected set; }

        /// <summary>
        /// Liefert oder setzt die Kultur
        /// </summary>
        public CultureInfo Culture { get; protected set; }

        /// <summary>
        /// Liefert oder setzt das Log, zum schreiben von Statusnachrichten auf die Konsole und in eine Log-Datei
        /// </summary>
        public Log Log { get; protected set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="port">Der Port</param>
        /// <param name="assetBaseFolder">Daten-Basisverzeichnis</param>
        /// <param name="configBaseFolder">Konfigurationserzeichnis</param>
        /// <param name="contextPath">Der Basispfad des Servers</param>
        /// <param name="culture">Die Kultur</param>
        /// <param name="log">Log</param>
        public HttpServerContext
        (
            int port,
            string assetBaseFolder,
            string configBaseFolder,
            IUri contextPath,
            CultureInfo culture,
            Log log
        )
        {
            var assembly = typeof(HttpServer).Assembly;
            Version = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;

            Port = port;
            AssetPath = assetBaseFolder;
            ConfigPath = configBaseFolder;
            ContextPath = contextPath;
            Culture = culture;
            Log = log;
        }
    }
}
