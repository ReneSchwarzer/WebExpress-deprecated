using System;

namespace WebExpress.WebPlugin
{
    /// <summary>
    /// Diese Interface repräsentiert ein Plugin
    /// </summary>
    public interface IPlugin : IDisposable
    {
        /// <summary>
        /// Initialisierung des Plugins.
        /// </summary>
        /// <param name="context">Der Kontext</param>
        void Initialization(IPluginContext context);

        /// <summary>
        /// Wird aufgerufen, wenn das Plugin mit der Arbeit beginnt
        /// </summary>
        void Run();
    }
}
