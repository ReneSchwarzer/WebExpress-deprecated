using System;
using System.Threading;

namespace WebExpress.WebTask
{
    public class Task : ITask
    {
        /// <summary>
        /// Interne Verwaltung des Fortschrittes
        /// </summary>
        private int progress { get; set; }

        /// <summary>
        /// Event wird ausgelöst, wenn die Aufgabe ausgeführt wird
        /// </summary>
        public event EventHandler Process;

        /// <summary>
        /// Die ID der Aufgabe
        /// </summary>
        public string ID { get; internal set; }

        /// <summary>
        /// Liefert den Zustand, indem sich die Aufgabe befindet
        /// </summary>
        public TaskState State { get; internal set; }

        /// <summary>
        /// Threadbeendigung der Aufgabe
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
            Process?.Invoke(this, new EventArgs());
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

            }, TokenSource.Token);
        }

        /// <summary>
        /// Abbruch einer bestehenden Verarbeitung
        /// </summary>
        public void Cancel()
        {
            TokenSource.Cancel();

            State = TaskState.Canceled;
        }
    }
}
