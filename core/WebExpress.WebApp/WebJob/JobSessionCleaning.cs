using WebExpress.Attribute;
using WebExpress.WebJob;

namespace WebExpress.WebApp.WebJob
{
    /// <summary>
    /// Job zur zyklischen Reinigung der Session. Nicht mehr benutzte Sessions werden entfernt.
    /// Der Job startet 0:30 Uhr am ersten Tag jeden Monats
    /// </summary>
    //[Job("30", "0", "1", "*", "*")]
    [Job("*", "*", "*", "*", "*")]
    [Module("webexpress.webapp")]
    internal sealed class JobSessionCleaning : Job
    {
        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext indem der Job ausgeführt wird</param>
        public override void Initialization(JobContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            //Context.Log.Info(message: string.Format(this.I18N("job.sessioncleaning.process"), Context.JobID));
        }
    }
}
