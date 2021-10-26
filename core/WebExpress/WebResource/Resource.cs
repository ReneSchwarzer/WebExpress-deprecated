using System.Globalization;
using WebExpress.Message;

namespace WebExpress.WebResource
{
    public abstract class Resource : IResource
    {
        /// <summary>
        /// Liefert die RessourcenID
        /// </summary>
        public string ID { get; internal set; }

        /// <summary>
        /// Liefert oder setzt den Modulkontext indem die Ressource existiert
        /// </summary>
        public IResourceContext Context { get; private set; }

        /// <summary>
        /// Liefert die I18N-PluginID
        /// </summary>
        public string I18NKey => Context?.Plugin.PluginID;

        /// <summary>
        /// Liefert die Kultur
        /// </summary>
        public CultureInfo Culture { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Resource()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public virtual void Initialization(IResourceContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Vorverarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        public virtual void PreProcess(Request request)
        {
            return;
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Die Antwort</returns>
        public abstract Response Process(Request request);

        /// <summary>
        /// Nachbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <param name="response">Die Antwort</param>
        /// <returns>Die Antwort</returns>
        public virtual Response PostProcess(Request request, Response response)
        {
            return response;
        }
    }
}
