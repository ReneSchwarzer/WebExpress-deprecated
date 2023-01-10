using System.Linq;
using System.Text.Json;
using WebExpress.Message;

namespace WebExpress.WebResource
{
    public abstract class ResourceRest : Resource
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ResourceRest()
        {

        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Processing of the resource. des GET-Request
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Eine Aufzählung, welche JsonSerializer serialisiert werden kann.</returns>
        public virtual object GetData(Request request)
        {
            return null;
        }

        /// <summary>
        /// Processing of the resource. des DELETE-Request
        /// </summary>
        /// <param name="id">Die zu löschende ID</param>
        /// <param name="request">The request.</param>
        /// <returns>Das Ergebnis der Löschung</returns>
        public virtual bool DeleteData(string id, Request request)
        {
            return false;
        }

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The response.</returns>
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
                        content = JsonSerializer.Serialize(DeleteData(request.Uri.Path.Last().Value, request), options);

                        break;
                    }
            };

            response.Header.ContentLength = content.Length;
            response.Content = content;

            return response;
        }
    }
}
