using WebExpress.Attribute;
using WebExpress.Message;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebAPI
{
    /// <summary>
    /// Ermittelt den Status und Forschritt einer Aufgabe (WebTask)
    /// </summary>
    [ID("PopupNotificationV1")]
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
        /// Verarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Ein Objekt welches mittels JsonSerializer serialisiert werden kann.</returns>
        public override object GetData(Request request)
        {
            return NotificationManager.GetNotifications(request);
        }
    }
}
