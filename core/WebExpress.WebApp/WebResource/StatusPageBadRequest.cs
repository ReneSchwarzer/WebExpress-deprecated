using WebExpress.Attribute;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebResource;

namespace WebExpress.WebApp.WebResource
{
    /// <summary>
    /// Statusseite
    /// </summary>
    [StatusCode(400)]
    public sealed class StatusPageBadRequest : StatusPageTemplateWebApp
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public StatusPageBadRequest()
        {
            
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Initialization()
        {
            base.Initialization();

            StatusTitle = this.I18N("webexpress.webapp", "status.400.title");

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
