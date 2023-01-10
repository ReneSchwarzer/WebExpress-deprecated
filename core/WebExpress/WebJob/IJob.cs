using WebExpress.Internationalization;

namespace WebExpress.WebJob
{
    /// <summary>
    /// Eine Aufgabe, die zyklisch ausgeführt werden kann
    /// </summary>
    public interface IJob : II18N
    {
        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">Der Kontext indem der Job ausgeführt wird</param>
        public void Initialization(JobContext context);

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        public void Process();

    }
}
