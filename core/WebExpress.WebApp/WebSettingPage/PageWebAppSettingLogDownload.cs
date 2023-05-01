using System.IO;
using WebExpress.WebMessage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebSettingPage
{
    /// <summary>
    /// Download der Logdatei
    /// </summary>
    [WebExID("SettingLogDownload")]
    [WebExSegment("download", "")]
    [WebExContextPath("/Setting/SettingLog")]
    [WebExModule("webexpress.webapp")]
    [WebExContext("admin")]
    [WebExOptional]
    public sealed class PageWebAppSettingLogDownload : ResourceBinary
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PageWebAppSettingLogDownload()
        {
        }

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The response.</returns>
        public override Response Process(Request request)
        {
            if (!File.Exists(request.ServerContext.Log.Filename))
            {
                return new ResponseNotFound();
            }


            Data = File.ReadAllBytes(request.ServerContext.Log.Filename);

            var response = base.Process(request);
            response.Header.CacheControl = "no-cache";
            response.Header.ContentType = "text/plain";

            return response;
        }
    }
}

