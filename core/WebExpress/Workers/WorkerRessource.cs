using System.IO;
using System.Linq;
using System.Reflection;
using WebExpress.Messages;
using WebExpress.Pages;

namespace WebExpress.Workers
{
    /// <summary>
    /// Stellt eine Ressource aus dem Plugin bereit
    /// </summary>
    public class WorkerRessource : WorkerBinary
    {
        /// <summary>
        /// Schutz vor Nebenläufgkeit
        /// </summary>
        private object Gard { get; set; }

        /// <summary>
        /// Das Assembly, indem sich die Ressourcen befindet
        /// </summary>
        private Assembly Assembly { get; set; }

        /// <summary>
        /// Liefert oder setzt das Wurzelverzeichnis
        /// </summary>
        public string Root { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="uri">Die aufzulösende Uri</param>
        /// <param name="assembly">Das Assembly, indem sich die Ressource befindet</param>
        /// <param name="root">Die Wurzel</param>
        public WorkerRessource(UriPage uri, Assembly assembly, string root)
            : base(uri)
        {
            Gard = new object();
            Assembly = assembly;
            Root = root;
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
                var resources = Assembly.GetManifestResourceNames();
                var url = request.URL.Substring(Context.UrlBasePath.Length);
                var fileName = Path.GetFileName(url);
                var file = string.Join("", Root, url.Replace("/", "."));

                Ressource = GetData(file, Assembly);

                if (Ressource == null)
                {
                    return new ResponseNotFound();
                }

                var response = base.Process(request);

                var extension = Path.GetExtension(fileName);
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
                        response.HeaderFields.ContentDisposition = "attatchment; filename=" + fileName + "; size=" + Ressource.LongLength;
                        response.HeaderFields.ContentType = "application/octet-stream";
                        break;
                    case ".zip":
                        response.HeaderFields.ContentDisposition = "attatchment; filename=" + fileName + "; size=" + Ressource.LongLength;
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
                    case ".jpeg":
                    case ".jpg":
                        response.HeaderFields.ContentType = "image/jpg";
                        break;
                    case ".ico":
                        response.HeaderFields.ContentType = "image/x-icon";
                        break;
                }

                //HostContext.Log.Debug(MethodBase.GetCurrentMethod(), request.Client + ": Datei '" + request.URL + "' wurde geladen.");

                return response;
            }
        }

        /// <summary>
        /// Liest die Daten einer angeegbenen Ressource
        /// </summary>
        /// <param name="file">Die Datei</param>
        /// <param name="assembly">Das Assembly</param>
        /// <returns></returns>
        private byte[] GetData(string file, Assembly assembly)
        {
            var resources = Assembly.GetManifestResourceNames();
            var item = resources.Where(x => x.Equals(file)).FirstOrDefault();
            if (item == null)
            {
                return null;
            }

            using (var stream = Assembly.GetManifestResourceStream(item))
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);

                    return memoryStream.ToArray();
                }
            }
        }
    }
}
