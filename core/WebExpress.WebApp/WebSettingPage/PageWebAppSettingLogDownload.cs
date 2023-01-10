using System.IO;
using WebExpress.Message;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebSettingPage
{
    /// <summary>
    /// Download der Logdatei
    /// </summary>
    [Id("SettingLogDownload")]
    [Segment("download", "")]
    [Path("/Setting/SettingLog")]
    [Module("webexpress.webapp")]
    [Context("admin")]
    [Optional]
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
            if (!File.Exists(ResourceContext.Log.Filename))
            {
                return new ResponseNotFound();
            }


            Data = File.ReadAllBytes(ResourceContext.Log.Filename);

            var response = base.Process(request);
            response.Header.CacheControl = "no-cache";
            response.Header.ContentType = "text/plain";

            return response;
        }
    }
}

