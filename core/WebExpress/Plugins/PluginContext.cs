using System.Reflection;
using WebExpress.Pages;

namespace WebExpress.Plugins
{
    public class PluginContext : IPluginContext
    {
        /// <summary>
        /// Liefert den Anwendungsnamen indem das Plugin aktiv ist. 
        /// </summary>
        public string AppArtifactID { get; protected set; }

        /// <summary>
        /// Liefert oder setzt den Name des Plugins 
        /// </summary>
        public string PluginName { get; protected set; }

        /// <summary>
        /// Liefert die Version des Plugins 
        /// </summary
        public string Version { get; protected set; }

        /// <summary>
        /// Liefert oder setzt die Version des HttpServers 
        /// </summary>
        public string HttpServerVersion { get; protected set; }

        /// <summary>
        /// Liefert oder setzt das Daten-Basisverzeichnis
        /// </summary>
        public string AssetBaseFolder { get; protected set; }

        /// <summary>
        /// Liefert oder setzt das Konfigurationserzeichnis
        /// </summary>
        public string ConfigBaseFolder { get; protected set; }

        /// <summary>
        /// Liefert oder setzt den Basispfad 
        /// </summary>
        public string UrlBasePath { get; protected set; }

        /// <summary>
        /// Liefert oder setzt die IconUrl
        /// </summary>
        public string IconUrl { get; set; }

        /// <summary>
        /// Liefert oder setzt das Log, zum schreiben von Statusnachrichten auf die Konsole und in eine Log-Datei
        /// </summary>
        public Log Log { get; protected set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="serverContext">Der Kontext des HTTP-Servers</param>
        /// <param name="factory">Die zugehörige Factory</param>
        /// <param name="configFileName">Der Ort der Konfiguration</param>
        public PluginContext(HttpServerContext serverContext, PluginFactory factory, string configFileName)
        {
            HttpServerVersion = serverContext.Version;
            AssetBaseFolder = serverContext.AssetBaseFolder;
            ConfigBaseFolder = configFileName;
            Log = serverContext.Log;

            AppArtifactID = factory?.AppArtifactID;
            PluginName = factory?.PluginName;
            Version = factory?.Version;
            IconUrl = factory?.Icon;

            var urlBasePath = !string.IsNullOrWhiteSpace(serverContext?.UrlBasePath) ? serverContext?.UrlBasePath?.Trim() : string.Empty;

            if (!string.IsNullOrWhiteSpace(urlBasePath) && !urlBasePath.StartsWith("/"))
            {
                urlBasePath = "/" + urlBasePath;
            }

            if (!string.IsNullOrWhiteSpace(urlBasePath) && urlBasePath.EndsWith("/"))
            {
                urlBasePath = urlBasePath.Substring(0, urlBasePath.Length - 1);
            }

            UrlBasePath = urlBasePath;
        }

        /// <summary>
        /// Copy-Konstruktor
        /// </summary>
        /// <param name="context">Der Kontext des Plugins</param>
        /// <param name="urlBasePath">Der Basispfad des Plugins</param>
        public PluginContext(IPluginContext context, string urlBasePath)
        {
            AppArtifactID = context.AppArtifactID;
            PluginName = context.PluginName;
            Version = context.Version;
            HttpServerVersion = context.HttpServerVersion;
            IconUrl = context.IconUrl;
            AssetBaseFolder = context.AssetBaseFolder;
            ConfigBaseFolder = context.ConfigBaseFolder;
            Log = context.Log;

            urlBasePath = !string.IsNullOrWhiteSpace(urlBasePath) ? urlBasePath.Trim() : string.Empty;

            if (!string.IsNullOrWhiteSpace(urlBasePath) && !urlBasePath.StartsWith("/"))
            {
                urlBasePath = "/" + urlBasePath;
            }

            if (!string.IsNullOrWhiteSpace(urlBasePath) && urlBasePath.EndsWith("/"))
            {
                urlBasePath = urlBasePath.Substring(0, urlBasePath.Length - 1);
            }

            UrlBasePath = context.UrlBasePath + urlBasePath;
        }
    }
}
