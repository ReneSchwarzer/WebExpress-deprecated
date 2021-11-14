using System.Text.Json;
using WebExpress.Message;

namespace WebExpress.WebResource
{
    public abstract class ResourceApi : Resource
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
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Ein Objekt welches mittels JsonSerializer serialisiert werden kann.</returns>
        public abstract object GetData(Request request);

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Die Antwort</returns>
        public override Response Process(Request request)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var content = JsonSerializer.Serialize(GetData(request), options);

            var response = new ResponseOK();
            response.Header.ContentLength = content.Length;

            response.Content = content;

            return response;
        }
    }
}
