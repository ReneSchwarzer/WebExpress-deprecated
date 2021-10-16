using WebExpress.Attribute;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebPage.PageStatus
{
    /// <summary>
    /// Statusseite
    /// </summary>
    [StatusCode(400)]
    public sealed class PageStatusBadRequest : PageStatusTemplateWebApp<ResponseBadRequest>
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageStatusBadRequest()
        {

        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            StatusTitle = this.I18N("webexpress.webapp", "status.400.title");

            Title = $"{ StatusCode } - { StatusTitle }";
        }
    }
}
