namespace WebExpress.Application
{
    /// <summary>
    /// Repräsentiert ein Anwendungseintrag im Anwendungsverzeichnis
    /// </summary>
    internal class ApplicationItem
    {
        /// <summary>
        /// Der zur Anwendung zugehörige Kontext
        /// </summary>
        public IApplicationContext Context { get; set; }

        /// <summary>
        /// Die Anwendung
        /// </summary>
        public IApplication Application { get; set; }
    }
}
