using System;

namespace WebExpress.WebTask
{
    public interface ITask
    {
        /// <summary>
        /// Event wird ausgelöst, wenn die Aufgabe ausgeführt wird
        /// </summary>
        event EventHandler<TaskEventArgs> Process;

        /// <summary>
        /// Event wird ausgelöst, wenn die Aufgabe beendet wird
        /// </summary>
        event EventHandler<TaskEventArgs> Finish;

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
        int Progress { get; set; }

        /// <summary>
        /// Liefert oder setzt eine Nachricht, die Auskunft über die Abarbeitung gibt.
        /// </summary>
        string Message { get; set;  }

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
