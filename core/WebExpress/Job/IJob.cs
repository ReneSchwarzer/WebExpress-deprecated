namespace WebExpress.Job
{
    /// <summary>
    /// Eine Aufgabe, die zyklisch ausgeführt werden kann
    /// </summary>
    public interface IJob
    {
        /// <summary>
        /// Initialisierung
        /// </summary>
        public void Initialization();

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public void Process();
    }
}
