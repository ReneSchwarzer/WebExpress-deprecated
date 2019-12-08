using WebExpress.Messages;
using WebExpress.Pages;

namespace WebExpress.Workers
{
    /// <summary>
    /// Arbeitet eine Anfrage ab. Dies erfolgt nebenläufig
    /// </summary>
    public class WorkerFileDownload : WorkerFile
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public WorkerFileDownload(Path url, string root)
            : base(url, root)
        {
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Die Antwort</returns>
        public override Response Process(Request request)
        {
            var path = System.IO.Path.GetFullPath(Root + request.URL);

            var response = base.Process(request);

            if (response is ResponseOK)
            {
                response.HeaderFields.ContentType = "application/force-download";
                response.HeaderFields.ContentDisposition = "attatchment; filename=" + System.IO.Path.GetFileName(path) + "; size=" + Ressource.LongLength;
            }

            return response;
        }
    }
}
