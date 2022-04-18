using System.Collections;
using System.Text.Json;
using WebExpress.Message;

namespace WebExpress.WebResource
{
    public abstract class ResourceRest : Resource
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ResourceRest()
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
        /// Verarbeitung des GET-Request
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Eine Aufzählung, welche JsonSerializer serialisiert werden kann.</returns>
        public virtual object GetData(Request request)
        {
            return null;
        }

        /// <summary>
        /// Verarbeitung des DELETE-Request
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Das Ergebnis der Löschung</returns>
        public virtual bool DeleteData(Request request)
        {
            return false;
        }

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
            
            var response = new ResponseOK();
            var content = string.Empty;

            switch (request.Method)
            {
                case RequestMethod.GET:
                    {
                        content = JsonSerializer.Serialize(GetData(request), options);

                        break;
                    }
                case RequestMethod.DELETE:
                    {
                        content = JsonSerializer.Serialize(DeleteData(request), options);

                        break;
                    }
            };

            response.Header.ContentLength = content.Length;
            response.Content = content;

            return response;
        }
    }
}
