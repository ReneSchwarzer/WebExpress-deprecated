using System;
using System.Collections.Generic;
using System.Text;

namespace WebExpress.Plugins
{
    public class PluginContext : IPluginContext
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="serverContext">Der Kontext des HTTP-Servers</param>
        public PluginContext(HttpServerContext serverContext)
        {
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
        }

        /// <summary>
        /// Copy-Konstruktor
        /// </summary>
        /// <param name="context">Der Kontext des Plugins</param>
        /// <param name="urlBasePath">Der Basispfad des Plugins</param>
        public PluginContext(IPluginContext context, string urlBasePath)
        {
            AssetBaseFolder = context.AssetBaseFolder;
            ConfigBaseFolder = context.ConfigBaseFolder;

            urlBasePath = string.IsNullOrWhiteSpace(urlBasePath) ? urlBasePath.Trim() : string.Empty;

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
        /// Liefert den Basispfad 
        /// </summary>
        public string UrlBasePath { get; protected set; }

        /// <summary>
        /// Liefert oder setzt das Log, zum schreiben von Statusnachrichten auf die Konsole und in eine Log-Datei
        /// </summary>
        public Log Log { get; protected set; }
    }
}
