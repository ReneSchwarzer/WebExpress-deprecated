using WebExpress.Attribute;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebPage.PageStatus
{
    /// <summary>
    /// Statusseite
    /// </summary>
    [StatusCode(404)]
    public sealed class PageStatusNotFound : PageStatusTemplateWebApp<ResponseNotFound>
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageStatusNotFound()
        {

        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            StatusTitle = this.I18N("webexpress.webapp", "status.404.title");

            Title = $"{ StatusCode } - { StatusTitle }";

            StatusMessage = this.I18N("webexpress.webapp", "status.404.description");
        }
    }
}
