using System.IO;
using WebExpress.Attribute;
using WebExpress.Message;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebSettingPage
{
    /// <summary>
    /// Download der Logdatei
    /// </summary>
    [ID("SettingLogDownload")]
    [Segment("download", "")]
    [Path("/Setting/SettingLog")]
    [Module("webexpress.webapp")]
    [Context("admin")]
    [Optional]
    public sealed class PageWebAppSettingLogDownload : ResourceBinary
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageWebAppSettingLogDownload()
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
            response.Header.CacheControl = "no-cache";
            response.Header.ContentType = "text/plain";

            return response;
        }
    }
}

