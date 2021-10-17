
using System.Globalization;

namespace WebExpress.WebJob
{
    public class Job : IJob
    {
        /// <summary>
        /// Liefert die I18N-PluginID
        /// </summary>
        public string I18N_PluginID => Context?.PluginID;

        /// <summary>
        /// Liefert die Kultur
        /// </summary>
        public CultureInfo Culture { get; set; }

        /// <summary>
        /// Liefert oder setzt den Kontext
        /// </summary>
        public IJobContext Context { get; private set; }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext indem der Job ausgeführt wird</param>
        public virtual void Initialization(JobContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public virtual void Process()
        {

        }
    }
}
