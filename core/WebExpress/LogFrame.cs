using System;
using System.Runtime.CompilerServices;

namespace WebExpress
{
    /// <summary>
    /// Erstellt einen Rahmen aus Logeinträgen
    /// </summary>
    public class LogFrame : IDisposable
    {
        public string Status { get; set; }

        protected string Instance { get; set; }
        protected int Line { get; set; }
        protected string File { get; set; }

        protected Log Log { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="log">Der Logeintrag</param>
        /// <param name="name">Der Name</param>
        /// <param name="additionalHeading">Die zusätzliche Überschrift oder null</param>
        /// <param name="instance">Methode, die loggen möchte</param>
        /// <param name="line">Die Zeilennummer</param>
        /// <param name="file">Die Quelldatei</param>
        public LogFrame(Log log, string name, string additionalHeading = null, [CallerMemberName] string instance = null, [CallerLineNumber] int? line = null, [CallerFilePath] string file = null)
        {
            Instance = instance;
            Status = string.Format("{0} abgeschlossen. ", name);

            Log = log;
            Log.Seperator();
            Log.Info(string.Format("Beginne mit {0}", name) + (!string.IsNullOrWhiteSpace(additionalHeading) ? " " + additionalHeading : ""), instance, line, file);
            Log.Info("".PadRight(80, '-'), instance, line, file);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="name">Der Name</param>
        /// <param name="additionalHeading">Die zusätzliche Überschrift oder null</param>
        /// <param name="instance">Methode, die loggen möchte</param>
        /// <param name="line">Die Zeilennummer</param>
        /// <param name="file">Die Quelldatei</param>
        public LogFrame(string name, string additionalHeading = null, [CallerMemberName] string instance = null, [CallerLineNumber] int? line = null, [CallerFilePath] string file = null)
            : this(Log.Current, name, additionalHeading, instance, line, file)
        {
        }

        /// <summary>
        /// Freigeben von nicht verwalteten Ressourcen, welche bei der Initialisierung reserviert wurden.
        /// </summary>
        /// <param name="data">Die Eingabedaten</param>
        /// <returns>Die Ausgabedaten</returns>
        public virtual void Dispose()
        {
            Log.Info("".PadRight(80, '='), Instance, Line, File);
            Log.Info(Status, Instance, Line, File);
        }
    }
}
