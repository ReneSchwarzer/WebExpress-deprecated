using WebExpress.Internationalization;

namespace WebExpress.WebJob
{
    /// <summary>
    /// Eine Aufgabe, die zyklisch ausgeführt werden kann
    /// </summary>
    public interface IJob : II18N
    {
        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext indem der Job ausgeführt wird</param>
        public void Initialization(JobContext context);

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public void Process();

    }
}
