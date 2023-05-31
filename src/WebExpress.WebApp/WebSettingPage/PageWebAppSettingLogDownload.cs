using System.IO;
using WebExpress.WebAttribute;
using WebExpress.WebMessage;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebSettingPage
{
    /// <summary>
    /// Download the log file.
    /// </summary>
    [WebExSegment("download", "")]
    [WebExContextPath("/")]
    [WebExParent(typeof(PageWebAppSettingLog))]
    [WebExModule<Module>]
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

