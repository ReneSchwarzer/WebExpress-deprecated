namespace WebExpress.Plugins
{
    /// <summary>
    /// Der Kontext
    /// </summary>
    public interface IPluginContext
    {
        /// <summary>
        /// Liefert den Name des Plugins 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Liefert die Version des Plugins 
        /// </summary>
        string Version { get; }

        /// <summary>
        /// Liefert die Version des HttpServers 
        /// </summary>
        string HttpServerVersion { get; }

        /// <summary>
        /// Liefert das Konfigurationserzeichnis
        /// </summary>
        string ConfigBaseFolder { get; }

        /// <summary>
        /// Liefert das Dokumentenverzeichnis
        /// </summary>
        string AssetBaseFolder { get; }

        /// <summary>
        /// Liefert den Basispfad 
        /// </summary>
        string UrlBasePath { get; }

        /// <summary>
        /// Liefert die IconUrl
        /// </summary>
        public string IconUrl { get; set; }

        /// <summary>
        /// Liefert das Log, zum schreiben von Statusnachrichten auf die Konsole und in eine Log-Datei
        /// </summary>
        Log Log { get; }
    }
}
