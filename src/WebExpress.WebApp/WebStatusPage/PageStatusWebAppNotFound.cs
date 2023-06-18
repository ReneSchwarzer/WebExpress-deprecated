using WebExpress.Internationalization;
using WebExpress.WebMessage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebStatusPage
{
    /// <summary>
    /// Statusseite
    /// </summary>
    [StatusCode(404)]
    [Default]
    public sealed class PageStatusWebAppNotFound : PageStatusWebApp<ResponseNotFound>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PageStatusWebAppNotFound()
        {

        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            StatusTitle = this.I18N("webexpress.webapp", "status.404.title");

            Title = $"{StatusCode} - {StatusTitle}";

            StatusMessage = this.I18N("webexpress.webapp", "status.404.description");
        }
    }
}
