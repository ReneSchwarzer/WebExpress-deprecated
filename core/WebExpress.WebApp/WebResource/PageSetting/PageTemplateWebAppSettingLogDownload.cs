using System.IO;
using WebExpress.Message;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebResource.PageSetting
{
    /// <summary>
    /// Download der Logdatei
    /// </summary>
    public abstract class PageTemplateWebAppSettingLogDownload : ResourceBinary
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageTemplateWebAppSettingLogDownload()
        {
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Die Antwort</returns>
        public override Response Process(Request request)
        {
            if (!File.Exists(Context.Log.Filename))
            {
                return new ResponseNotFound();
            }


            Data = File.ReadAllBytes(Context.Log.Filename);

            var response = base.Process(request);
            response.HeaderFields.CacheControl = "no-cache";
            response.HeaderFields.ContentType = "text/plain";

            return response;
        }
    }
}

