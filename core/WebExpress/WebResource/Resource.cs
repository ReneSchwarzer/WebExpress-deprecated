using System.Collections.Generic;
using WebExpress.Message;
using WebExpress.Module;
using WebExpress.Uri;

namespace WebExpress.WebResource
{
    public abstract class Resource : IResource
    {
        /// <summary>
        /// Liefert oder setzt die URL, auf dem der Worker reagiert
        /// </summary>
        public IUri Uri { get; internal set; }

        /// <summary>
        /// Liefert oder setzt den Modulkontext indem die Ressource existiert
        /// </summary>
        public IModuleContext Context { get; internal set; }

        /// <summary>
        /// Liefert oder setzt den Ressourcenkontext
        /// </summary>
        public IReadOnlyList<string> ResourceContext { get; internal set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Resource()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public virtual void Initialization()
        {
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
