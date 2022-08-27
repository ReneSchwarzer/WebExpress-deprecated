using System;
using System.Collections.Generic;
using System.Threading;

namespace WebExpress.WebTask
{
    public class Task : ITask
    {
        /// <summary>
        /// Interne Verwaltung des Fortschrittes.
        /// </summary>
        private int progress { get; set; }

        /// <summary>
        /// Event wird ausgelöst, wenn die Aufgabe ausgeführt wird.
        /// </summary>
        public event EventHandler<TaskEventArgs> Process;

        /// <summary>
        /// Event wird ausgelöst, wenn die Aufgabe beendet wird
        /// </summary>
        public event EventHandler<TaskEventArgs> Finish;

        /// <summary>
        /// Die ID der Aufgabe
        /// </summary>
        public string ID { get; internal set; }

        /// <summary>
        /// Liefert den Zustand, indem sich die Aufgabe befindet.
        /// </summary>
        public TaskState State { get; internal set; }

        /// <summary>
        /// Die Argumente
        /// </summary>
        public ICollection<object> Arguments { get; internal set; }

        /// <summary>
        /// Threadbeendigung der Aufgabe.
        /// </summary>
        private CancellationTokenSource TokenSource { get; } = new CancellationTokenSource();

        /// <summary>
        /// Ermittelt den Fortschritt der Aufgabe. Der Wertebereich liegt zwischen 0 und 100.
        /// </summary>
        public int Progress
        {
            get => progress;
            set => progress = Math.Min(value, 100);
        }

        /// <summary>
        /// Liefert oder setzt eine Nachricht, die Auskunft über die Abarbeitung gibt.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public virtual void Initialization()
        {
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        protected virtual void OnProcess()
        {
            Process?.Invoke(this, new TaskEventArgs());
        }

        /// <summary>
        /// Wird ausgelöst, wenn die Aufgabe abgeschlossen ist
        /// </summary>
        protected virtual void OnFinish()
        {
            Finish?.Invoke(this, new TaskEventArgs());
        }

        /// <summary>
        /// Startet die Ausführung nebenläufig
        /// </summary>
        public void Run()
        {
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                State = TaskState.Run;

                Progress = 0;

                OnProcess();

                Progress = 100;

                State = TaskState.Finish;

                OnFinish();

                TaskManager.RemoveTask(this);

            }, TokenSource.Token);
        }

        /// <summary>
        /// Abbruch einer bestehenden Verarbeitung.
        /// </summary>
        public void Cancel()
        {
            TokenSource.Cancel();

            State = TaskState.Canceled;

            TaskManager.RemoveTask(this);
        }
    }
}
