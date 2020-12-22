using System;
using WebExpress.Config;

namespace WebExpress.Application
{
    /// <summary>
    /// Diese Interface repräsentiert eine Anwendung
    /// </summary>
    public interface IApplication : IDisposable
    {
        /// <summary>
        /// Initialisierung der Anwendung. 
        /// </summary>
        /// <param name="context">Der Kontext</param>
        void Initialization(IApplicationContext context);

        /// <summary>
        /// Wird aufgerufen, wenn die Anwendung die Arbeit aufnimmt. Der Aufruf erfolgt nebenläufig.
        /// </summary>
        void Run();
    }
}
