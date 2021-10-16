using WebExpress.Attribute;

namespace WebExpress.WebApp.Job
{
    /// <summary>
    /// Job zur zyklischen Reinigung der Session. Nicht mehr benutzte Sessions werden entfernt.
    /// Der Job startet 0:30 Uhr am ersten Tag jeden Monats
    /// </summary>
    //[Job("30", "0", "1", "*", "*")]
    [Job("*", "*", "*", "*", "*")]
    internal sealed class JobSessionCleaning : WebExpress.Job.Job
    {
        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Initialization()
        {

        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {

        }
    }
}
