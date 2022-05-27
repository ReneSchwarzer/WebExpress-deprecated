using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using WebExpress.Config;
using WebExpress.Uri;

namespace WebExpress
{
    /// <summary>
    /// Der Kontext des Http-Servers
    /// </summary>
    public class HttpServerContext : IHttpServerContext
    {
        /// <summary>
        /// Liefert die Endpunkte, auf die der Webserver reagiert
        /// </summary>
        public ICollection<EndpointConfig> Endpoints { get; protected set; }

        /// <summary>
        /// Liefert die Version des Http-Servers 
        /// </summary>
        public string Version { get; protected set; }

        /// <summary>
        /// Liefert oder setzt das Asset-Basisverzeichnis
        /// </summary>
        public string AssetPath { get; protected set; }

        /// <summary>
        /// Liefert oder setzt das Daten-Basisverzeichnis
        /// </summary>
        public string DataPath { get; protected set; }

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
        /// <param name="endpoints">Die Uri des Servers</param>
        /// <param name="assetBaseFolder">Daten-Basisverzeichnis</param>
        /// <param name="dataBaseFolder">Daten-Basisverzeichnis</param>
        /// <param name="configBaseFolder">Konfigurationserzeichnis</param>
        /// <param name="contextPath">Der Basispfad des Servers</param>
        /// <param name="culture">Die Kultur</param>
        /// <param name="log">Log</param>
        public HttpServerContext
        (
            ICollection<EndpointConfig> endpoints,
            string assetBaseFolder,
            string dataBaseFolder,
            string configBaseFolder,
            IUri contextPath,
            CultureInfo culture,
            Log log
        )
        {
            var assembly = typeof(HttpServer).Assembly;
            Version = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;

            Endpoints = endpoints;
            AssetPath = assetBaseFolder;
            DataPath = dataBaseFolder;
            ConfigPath = configBaseFolder;
            ContextPath = contextPath;
            Culture = culture;
            Log = log;
        }
    }
}
