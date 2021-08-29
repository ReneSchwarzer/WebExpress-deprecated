using WebExpress.Message;
using WebExpress.Module;
using WebExpress.Uri;

namespace WebExpress.WebResource
{
    /// <summary>
    /// Arbeitet eine Anfrage ab. Dies erfolgt nebenläufig
    /// </summary>
    public abstract class ResourceBinary : Resource
    {
        /// <summary>
        /// Liefert oder setzt die Daten
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ResourceBinary()
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
            response.HeaderFields.ContentLength = Data != null ? Data.Length : 0;
            response.HeaderFields.ContentType = "binary/octet-stream";

            response.Content = Data;

            return response;
        }
    }
}
