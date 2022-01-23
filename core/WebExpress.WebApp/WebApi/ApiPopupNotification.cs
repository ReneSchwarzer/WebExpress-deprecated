using WebExpress.WebAttribute;
using WebExpress.Message;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebResource;
using System.Collections;

namespace WebExpress.WebApp.WebAPI
{
    /// <summary>
    /// Ermittelt den Status und Forschritt einer Aufgabe (WebTask)
    /// </summary>
    [ID("APIPopupNotificationV1")]
    [Segment("popupnotifications", "")]
    [Path("/api/v1")]
    [Module("webexpress.webapp")]
    [Optional]
    public sealed class ApiPopupNotification : ResourceApi
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ApiPopupNotification()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Verarbeitung des GET-Request
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Eine Aufzählung, welche mittels JsonSerializer serialisiert werden kann.</returns>
        public override ICollection GetData(Request request)
        {
            return (ICollection)NotificationManager.GetNotifications(request);
        }
    }
}
