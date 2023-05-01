﻿using WebExpress.Internationalization;
using WebExpress.WebMessage;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebStatusPage
{
    /// <summary>
    /// Statusseite
    /// </summary>
    [WebExStatusCode(500)]
    [WebExDefault]
    public sealed class PageStatusWebAppInternalServerError : PageStatusWebApp<ResponseInternalServerError>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PageStatusWebAppInternalServerError()
        {

        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            StatusTitle = this.I18N("webexpress.webapp", "status.500.title");

            Title = $"{StatusCode} - {StatusTitle}";
        }
    }
}
