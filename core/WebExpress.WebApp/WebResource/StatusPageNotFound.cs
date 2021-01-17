using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebResource;

namespace WebExpress.WebApp.WebResource
{
    /// <summary>
    /// Statusseite
    /// </summary>
    [StatusCode(404)]
    public sealed class StatusPageNotFound : StatusPageTemplateWebApp
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public StatusPageNotFound()
        {
            
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Initialization()
        {
            base.Initialization();

            StatusTitle = this.I18N("webexpress.webapp", "status.404.title");

            Title = $"{ StatusCode } - { StatusTitle }";

            StatusMessage = this.I18N("webexpress.webapp", "status.404.description");
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
