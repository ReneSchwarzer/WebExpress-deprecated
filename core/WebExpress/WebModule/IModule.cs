using System;

namespace WebExpress.WebModule
{
    public interface IModule : IDisposable
    {
        /// <summary>
        /// Initialisierung des Moduls. 
        /// </summary>
        /// <param name="context">Der Kontext</param>
        void Initialization(IModuleContext context);

        /// <summary>
        /// Wird aufgerufen, wenn das Modul die Arbeit aufnimmt. Der Aufruf erfolgt nebenläufig.
        /// </summary>
        void Run();
    }
}
