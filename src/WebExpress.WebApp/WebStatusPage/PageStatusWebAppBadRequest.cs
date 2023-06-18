using WebExpress.Internationalization;
using WebExpress.WebMessage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebStatusPage
{
    /// <summary>
    /// Statusseite
    /// </summary>
    [StatusCode(400)]
    [Default]
    public sealed class PageStatusWebAppBadRequest : PageStatusWebApp<ResponseBadRequest>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PageStatusWebAppBadRequest()
        {

        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            StatusTitle = this.I18N("webexpress.webapp", "status.400.title");

            Title = $"{StatusCode} - {StatusTitle}";
        }
    }
}
