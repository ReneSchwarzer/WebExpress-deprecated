using System;
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
        /// <param name="assertBaseFolder">Daten-Basisverzeichnis</param>
        /// <param name="configBaseFolder">Konfigurationserzeichnis</param>
        public HttpServerContext(string assertBaseFolder, string configBaseFolder)
        {
            AssertBaseFolder = assertBaseFolder;
            ConfigBaseFolder = configBaseFolder;
        }

        /// <summary>
        /// Liefert oder setzt das Daten-Basisverzeichnis
        /// </summary>
        public string AssertBaseFolder { get; protected set; }

        /// <summary>
        /// Liefert oder setzt das Konfigurationserzeichnis
        /// </summary>
        public string ConfigBaseFolder { get; protected set; }
    }
}
