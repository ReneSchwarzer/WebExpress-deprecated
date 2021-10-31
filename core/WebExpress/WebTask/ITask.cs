using System;

namespace WebExpress.WebTask
{
    public interface ITask
    {
        /// <summary>
        /// Event wird ausgelöst, wenn die Aufgabe ausgeführt wird
        /// </summary>
        event EventHandler Process;

        /// <summary>
        /// Die ID der Aufgabe
        /// </summary>
        string ID { get; }

        /// <summary>
        /// Liefert den Zustand, indem sich die Aufgabe befindet
        /// </summary>
        TaskState State { get; }

        /// <summary>
        /// Ermittelt den Fortschritt der Aufgabe. Der Wertebereich liegt zwischen 0 und 100.
        /// </summary>
        int Progress { get; }

        /// <summary>
        /// Initialisierung
        /// </summary>
        void Initialization();

        /// <summary>
        /// Startet die Ausführung nebenläufig
        /// </summary>
        void Run();

        /// <summary>
        /// Abbruch einer bestehenden Verarbeitung
        /// </summary>
        void Cancel();
    }
}
