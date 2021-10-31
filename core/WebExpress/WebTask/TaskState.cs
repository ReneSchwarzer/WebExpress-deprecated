namespace WebExpress.WebTask
{
    public enum TaskState
    {
        /// <summary>
        /// Der Task wurde erstellt und wartet auf die Ausführung
        /// </summary>
        Created,
        /// <summary>
        /// Der Task befindet sich in der Ausführung
        /// </summary>
        Run,
        /// <summary>
        /// Der Task wurde abgebrochen
        /// </summary>
        Canceled,
        /// <summary>
        /// Der Task wurde beendet
        /// </summary>
        Finish
    }
}
