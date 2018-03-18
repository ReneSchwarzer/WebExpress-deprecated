using WebExpress.Messages;
using WebExpress.Pages;
using WebExpress.Plugins;

namespace WebExpress.Workers
{
    /// <summary>
    /// Arbeitet eine Anfrage ab. Dies erfolgt nebenläufig
    /// </summary>
    public interface IWorker
    {
        /// <summary>
        /// Liefert die URL, auf dem der Worker reagiert
        /// </summary>
        Path Path { get; }

        /// <summary>
        /// Liefert oder setzt den Kontext des Plugins
        /// </summary>
        IPluginContext HostContext { get; set; }

        /// <summary>
        /// Vorverarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Die Antwort aus der Vorverarbeitung oder null</returns>
        Response PreProcess(Request request);

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Die Antwort</returns>
        Response Process(Request request);

        /// <summary>
        /// Vorverarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <param name="response">Die Antwort</param>
        /// <returns>Die Antwort</returns>
        Response PostProcess(Request request, Response response);
    }
}
