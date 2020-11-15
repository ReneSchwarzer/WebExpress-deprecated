using System.Reflection;
using WebExpress.Pages;

namespace WebExpress.Plugins
{
    public class PluginContext : IPluginContext
    {
        /// <summary>
        /// Liefert oder setzt den Verweis auf das Plugin
        /// </summary>
        private IPlugin Plugin { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="serverContext">Der Kontext des HTTP-Servers</param>
        /// <param name="plugin">Das zugehörige Plugin</param>
        public PluginContext(HttpServerContext serverContext, IPlugin plugin)
        {
            Plugin = plugin;
            Name = plugin.Name;
            Version = plugin.GetType().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
            HttpServerVersion = serverContext.Version;
            IconUrl = plugin.Icon;
            AssetBaseFolder = serverContext.AssetBaseFolder;
            ConfigBaseFolder = serverContext.ConfigBaseFolder;

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

            Log = serverContext.Log;
            Host = serverContext.Host;
        }

        /// <summary>
        /// Copy-Konstruktor
        /// </summary>
        /// <param name="context">Der Kontext des Plugins</param>
        /// <param name="urlBasePath">Der Basispfad des Plugins</param>
        public PluginContext(IPluginContext context, string urlBasePath)
        {
            Name = context.Name;
            Version = context.Version;
            HttpServerVersion = context.HttpServerVersion;
            IconUrl = context.IconUrl;
            AssetBaseFolder = context.AssetBaseFolder;
            ConfigBaseFolder = context.ConfigBaseFolder;

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

            Log = context.Log;
            Host = context.Host;
        }

        /// <summary>
        /// Liefert oder setzt den Name des Plugins 
        /// </summary>
        public string Name { get; protected set; }

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
        /// Liefert das Sitemap
        /// </summary>
        public ISiteMap SiteMap => Plugin?.SiteMap;

        /// <summary>
        /// Liefert oder setzt das Log, zum schreiben von Statusnachrichten auf die Konsole und in eine Log-Datei
        /// </summary>
        public Log Log { get; protected set; }

        /// <summary>
        /// Verweis auf dem Webserver
        /// </summary>
        public IHost Host { get; protected set; }
    }
}
