namespace WebExpress.Plugins
{
    /// <summary>
    /// Kennzeichnet ein Factory-Objekt zum Erstellen von Plugins
    /// </summary>
    public interface IPluginFactory
    {
        /// <summary>
        /// Liefert den Anwendungsnamen indem das Plugin aktiv ist. 
        /// </summary>
        string AppArtifactID { get; }

        /// <summary>
        /// Liefert oder setzt die ID
        /// </summary>
        string ArtifactID { get; }

        /// <summary>
        /// Liefert oder setzt die HerstellerID
        /// </summary>
        string ManufacturerID { get; }

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Liefert oder setzt den Namen
        /// </summary>
        string Version { get; }

        /// <summary>
        /// Liefert den Dateinamen der Konfigurationsdatei
        /// </summary>
        string ConfigFileName { get; }

        /// <summary>
        /// Erstellt eine neue Instanz eines Prozesszustandes
        /// </summary>
        /// <param name="host">Der Kontext</param>
        /// <param name="configFileName">Der Dateiname der Konfiguration oder null</param>
        /// <returns>Die Instanz des Plugins</returns>
        IPlugin Create(HttpServerContext host, string configFileName);
    }
}
