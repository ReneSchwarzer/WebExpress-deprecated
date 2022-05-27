using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebStatusPage
{
    /// <summary>
    /// Statusseite
    /// </summary>
    [StatusCode(500)]
    public sealed class PageStatusWebAppInternalServerError : PageStatusWebApp<ResponseInternalServerError>
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageStatusWebAppInternalServerError()
        {

        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            StatusTitle = this.I18N("webexpress.webapp", "status.500.title");

            Title = $"{StatusCode} - {StatusTitle}";
        }
    }
}
