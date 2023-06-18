using WebExpress.WebJob;

namespace WebExpress.WebApp.WebJob
{
    /// <summary>
    /// Job for cyclic cleaning of the session. Sessions that are no longer in use will be removed. 
    /// The job starts at 0:30 a.m. on the first day of each month.
    /// </summary>
    internal sealed class JobSessionCleaning : Job
    {
        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context in which the job is executed.</param>
        public override void Initialization(JobContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        public override void Process()
        {
            //Context.Log.Info(message: string.Format(this.I18N("job.sessioncleaning.process"), Context.JobId));
        }
    }
}
