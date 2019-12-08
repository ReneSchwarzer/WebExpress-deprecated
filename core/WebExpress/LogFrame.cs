using System;
using System.Reflection;


namespace WebExpress
{
    /// <summary>
    /// Erstellt einen Rahmen aus Logeinträgen
    /// </summary>
    public class LogFrame : IDisposable
    {
        public string Status { get; set; }

        protected MethodBase Instance { get; set; }

        protected Log Log { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="instance">Methode, die loggen möchte</param>
        /// <param name="name">Der Name</param>
        /// <param name="additionalHeading">Die zusätzliche Überschrift oder null</param>
        public LogFrame(Log log, MethodBase instance, string name, string additionalHeading = null)
        {
            Instance = instance;
            Status = string.Format("{0} abgeschlossen. ", name);

            Log = log;
            Log.Seperator();
            Log.Info(Instance, string.Format("Beginne mit {0}", name) + (!string.IsNullOrWhiteSpace(additionalHeading) ? " " + additionalHeading : ""));
            Log.Info(Instance, "".PadRight(80, '-'));
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="instance">Methode, die loggen möchte</param>
        /// <param name="name">Der Name</param>
        /// <param name="additionalHeading">Die zusätzliche Überschrift oder null</param>
        public LogFrame(MethodBase instance, string name, string additionalHeading = null)
            : this(Log.Current, instance, name, additionalHeading)
        {
        }

        /// <summary>
        /// Freigeben von nicht verwalteten Ressourcen, welche bei der Initialisierung reserviert wurden.
        /// </summary>
        /// <param name="data">Die Eingabedaten</param>
        /// <returns>Die Ausgabedaten</returns>
        public virtual void Dispose()
        {
            Log.Info(Instance, "".PadRight(80, '='));
            Log.Info(Instance, Status);
        }
    }
}
