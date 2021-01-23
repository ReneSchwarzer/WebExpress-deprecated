using WebExpress.Message;
using WebExpress.Module;
using WebExpress.Uri;

namespace WebExpress.WebResource
{
    public class ResourceApi : Resource
    {
        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        public string Content { get; protected set; } = string.Empty;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ResourceApi()
        {

        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Initialization()
        {
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public virtual void Process()
        {

        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Die Antwort</returns>
        public override Response Process(Request request)
        {
            Process();

            var response = new ResponseOK();
            response.HeaderFields.ContentLength = Content.Length;

            response.Content = Content;

            return response;
        }
    }
}
