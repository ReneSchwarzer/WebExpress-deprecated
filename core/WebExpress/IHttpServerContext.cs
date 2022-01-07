using System.Collections.Generic;
using System.Globalization;
using WebExpress.Config;
using WebExpress.Uri;

namespace WebExpress
{
    /// <summary>
    /// Der Kontext des Http-Servers
    /// </summary>
    public interface IHttpServerContext
    {
        /// <summary>
        /// Liefert die Endpunkte, auf die der Webserver reagiert
        /// </summary>
        public ICollection<EndpointConfig> Endpoints { get; }

        /// <summary>
        /// Liefert die Version des Http-Servers 
        /// </summary>
        string Version { get; }

        /// <summary>
        /// Liefert das Asset-Basisverzeichnis
        /// </summary>
        string AssetPath { get; }

        /// <summary>
        /// Liefert das Daten-Basisverzeichnis
        /// </summary>
        string DataPath { get; }

        /// <summary>
        /// Liefert das Konfigurationserzeichnis
        /// </summary>
        string ConfigPath { get; }

        /// <summary>
        /// Liefert den Basispfad 
        /// </summary>
        IUri ContextPath { get; }

        /// <summary>
        /// Liefert die Kultur
        /// </summary>
        CultureInfo Culture { get; }

        /// <summary>
        /// Liefert das Log, zum schreiben von Statusnachrichten auf die Konsole und in eine Log-Datei
        /// </summary>
        Log Log { get; }
    }
}
