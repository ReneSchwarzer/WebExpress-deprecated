using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.Internationalization;

namespace WebExpress.WebApp.WebResource.PageStatus
{
    /// <summary>
    /// Statusseite
    /// </summary>
    [StatusCode(500)]
    public sealed class PageStatusInternalServerError : PageStatusTemplateWebApp
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
        public override void Initialization()
        {
            base.Initialization();

            StatusTitle = this.I18N("webexpress.webapp", "status.500.title");

            Title = $"{ StatusCode } - { StatusTitle }";
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

        }

        /// <summary>
        /// In HTML konvertieren+
        /// </summary>
        /// <returns>Die Seite als HTML-Baum</returns>
        public override IHtmlNode Render()
        {
            return base.Render();
        }
    }
}
