using System.Globalization;
using WebExpress.Uri;

namespace WebExpress
{
    /// <summary>
    /// Der Kontext des Http-Servers
    /// </summary>
    public interface IHttpServerContext
    {
        /// <summary>
        /// Liefert die Version des Plugins 
        /// </summary>
        string Version { get; }

        /// <summary>
        /// Liefert oder setzt das Daten-Basisverzeichnis
        /// </summary>
        string AssetPath { get; }

        /// <summary>
        /// Liefert oder setzt das Konfigurationserzeichnis
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
        /// Liefert oder setzt das Log, zum schreiben von Statusnachrichten auf die Konsole und in eine Log-Datei
        /// </summary>
        Log Log { get; }
    }
}
