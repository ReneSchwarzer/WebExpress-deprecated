using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.Uri;

namespace WebExpress.WebResource
{
    public interface IResource : II18N
    {
        /// <summary>
        /// Liefert die RessourcenID
        /// </summary>
        string ID { get; }

        /// <summary>
        /// Liefert oder setzt den Kontext indem die Ressource existiert
        /// </summary>
        IResourceContext Context { get; }

        /// <summary>
        /// Die Uri der Ressource
        /// </summary>
        IUri Uri { get; }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        void Initialization(IResourceContext context);

        /// <summary>
        /// Vorverarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        void PreProcess(Request request);

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Die Antwort</returns>
        Response Process(Request request);

        /// <summary>
        /// Nachbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <param name="response">Die Antwort</param>
        /// <returns>Die Antwort</returns>
        Response PostProcess(Request request, Response response);
    }
}
