using WebExpress.Messages;
using WebExpress.Pages;

namespace WebExpress.Workers
{
    /// <summary>
    /// Arbeitet eine Anfrage ab. Dies erfolgt nebenläufig
    /// </summary>
    public abstract class WorkerBinary : WorkerAuthentication
    {
        /// <summary>
        /// Liefert oder setzt die Ressource
        /// </summary>
        public byte[] Ressource { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="uri">Die Uri</param>
        public WorkerBinary(UriPage uri)
            : base(uri)
        {
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Die Antwort</returns>
        public override Response Process(Request request)
        {
            var response = new ResponseOK();
            response.HeaderFields.ContentLength = Ressource.Length;
            response.HeaderFields.ContentType = "binary/octet-stream";

            response.Content = Ressource;

            return response;
        }
    }
}
