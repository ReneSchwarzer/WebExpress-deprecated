using WebExpress.Attribute;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebPage.PageStatus
{
    /// <summary>
    /// Statusseite
    /// </summary>
    [StatusCode(500)]
    public sealed class PageStatusInternalServerError : PageStatusTemplateWebApp<ResponseInternalServerError>
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageStatusInternalServerError()
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

            Title = $"{ StatusCode } - { StatusTitle }";
        }
    }
}
