using WebExpress.Message;
using WebExpress.Module;
using WebExpress.Uri;

namespace WebExpress.WebResource
{
    /// <summary>
    /// Arbeitet eine Anfrage ab. Dies erfolgt nebenläufig
    /// </summary>
    public class ResourceFile : ResourceBinary
    {
        /// <summary>
        /// Schutz bei Nebenläufgkeit
        /// </summary>
        private object Gard { get; set; }

        /// <summary>
        /// Liefert oder setzt das Stammverzeichnis
        /// </summary>
        public string RootDirectory { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ResourceFile()
        {
            Gard = new object();
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Die Antwort</returns>
        public override Response Process(Request request)
        {
            lock (Gard)
            {
                var url = ""; // request.URL[Context.ContextPath.ToString().Length..];

                var path = System.IO.Path.GetFullPath(RootDirectory + url);

                if (!System.IO.File.Exists(path))
                {
                    return new ResponseNotFound();
                }

                Data = System.IO.File.ReadAllBytes(path);

                var response = base.Process(request);
                response.HeaderFields.CacheControl = "public, max-age=31536000";

                var extension = System.IO.Path.GetExtension(path);
                extension = !string.IsNullOrWhiteSpace(extension) ? extension.ToLower() : "";

                switch (extension)
                {
                    case ".pdf":
                        response.HeaderFields.ContentType = "application/pdf";
                        break;
                    case ".txt":
                        response.HeaderFields.ContentType = "text/plain";
                        break;
                    case ".css":
                        response.HeaderFields.ContentType = "text/css";
                        break;
                    case ".xml":
                        response.HeaderFields.ContentType = "text/xml";
                        break;
                    case ".html":
                    case ".htm":
                        response.HeaderFields.ContentType = "text/html";
                        break;
                    case ".exe":
                        response.HeaderFields.ContentDisposition = "attatchment; filename=" + System.IO.Path.GetFileName(path) + "; size=" + Data.LongLength;
                        response.HeaderFields.ContentType = "application/octet-stream";
                        break;
                    case ".zip":
                        response.HeaderFields.ContentDisposition = "attatchment; filename=" + System.IO.Path.GetFileName(path) + "; size=" + Data.LongLength;
                        response.HeaderFields.ContentType = "application/zip";
                        break;
                    case ".doc":
                    case ".docx":
                        response.HeaderFields.ContentType = "application/msword";
                        break;
                    case ".xls":
                    case ".xlx":
                        response.HeaderFields.ContentType = "application/vnd.ms-excel";
                        break;
                    case ".ppt":
                        response.HeaderFields.ContentType = "application/vnd.ms-powerpoint";
                        break;
                    case ".gif":
                        response.HeaderFields.ContentType = "image/gif";
                        break;
                    case ".png":
                        response.HeaderFields.ContentType = "image/png";
                        break;
                    case ".svg":
                        response.HeaderFields.ContentType = "image/svg+xml";
                        break;
                    case ".jpeg":
                    case ".jpg":
                        response.HeaderFields.ContentType = "image/jpg";
                        break;
                    case ".ico":
                        response.HeaderFields.ContentType = "image/x-icon";
                        break;
                }

                Context.Log.Debug(request.Client + ": Datei '" + request.URL + "' wurde geladen.");

                return response;
            }
        }
    }
}
