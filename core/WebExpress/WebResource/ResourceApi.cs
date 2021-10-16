using WebExpress.Message;

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
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
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
