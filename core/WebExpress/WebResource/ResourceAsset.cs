using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WebExpress.Message;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.WebResource
{
    /// <summary>
    /// Lieferung einer im Assamby eingebetteten Ressource
    /// </summary>
    public class ResourceAsset : ResourceBinary
    {
        /// <summary>
        /// Schutz vor Nebenläufgkeit
        /// </summary>
        private object Gard { get; set; }

        /// <summary>
        /// Liefert oder setzt das Stammverzeichnis
        /// </summary>
        public string AssetDirectory { get; protected set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ResourceAsset()
        {
            Gard = new object();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            AssetDirectory = Context.Assembly.GetName().Name;
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
                var assembly = Context.Assembly;
                var buf = assembly.GetManifestResourceNames().ToList();
                var resources = assembly.GetManifestResourceNames().Where(x => x.StartsWith(AssetDirectory, System.StringComparison.OrdinalIgnoreCase));
                var url = request.Uri.ToString()[Context.ContextPath.ToString().Length..];
                var fileName = Path.GetFileName(url);
                var file = string.Join('.', AssetDirectory.Trim('.'), url.Replace("/", ".").Trim('.'));

                Data = GetData(file, assembly, resources.ToList());

                if (Data == null)
                {
                    return new ResponseNotFound();
                }

                var response = base.Process(request);
                response.HeaderFields.CacheControl = "public, max-age=31536000";

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
                        response.HeaderFields.ContentDisposition = "attatchment; filename=" + fileName + "; size=" + Data.LongLength;
                        response.HeaderFields.ContentType = "application/octet-stream";
                        break;
                    case ".zip":
                        response.HeaderFields.ContentDisposition = "attatchment; filename=" + fileName + "; size=" + Data.LongLength;
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

                Context.Log.Debug(string.Format(I18N("webexpress.ui:resourceasset.load"), request.Client, request.Uri));

                return response;
            }
        }

        /// <summary>
        /// Liest die Daten einer angeegbenen Ressource
        /// </summary>
        /// <param name="file">Die Datei</param>
        /// <param name="assembly">Das Assembly</param>
        /// <returns></returns>
        private static byte[] GetData(string file, Assembly assembly, IEnumerable<string> resources)
        {
            var item = resources.Where(x => x.Equals(file, System.StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (item == null)
            {
                return null;
            }

            using var stream = assembly.GetManifestResourceStream(item);
            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);

            return memoryStream.ToArray();
        }
    }
}