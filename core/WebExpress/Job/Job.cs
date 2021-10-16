using WebExpress.Scheduler;

namespace WebExpress.Job
{
    public class Job : IJob
    {
        /// <summary>
        /// Liefert oder setzt den Kontext
        /// </summary>
        public ISchedulerContext Context { get; internal set; }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public virtual void Initialization()
        {

        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public virtual void Process()
        {

        }
    }
}
