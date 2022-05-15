using System.Collections;
using WebExpress.Message;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebAPI.V1
{
    /// <summary>
    /// Ermittelt den Status und Forschritt einer Aufgabe (WebTask)
    /// </summary>
    [ID("ApiPopupNotificationV1")]
    [Segment("popupnotifications", "")]
    [Path("/api/v1")]
    [Module("webexpress.webapp")]
    [IncludeSubPaths(true)]
    [Optional]
    public sealed class RestPopupNotification : ResourceRest
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public RestPopupNotification()
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

        /// <summary>
        /// Verarbeitung des DELETE-Request
        /// </summary>
        /// <param name="id">Die zu löschende ID</param>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Das Ergebnis der Löschung</returns>
        public override bool DeleteData(string id, Request request)
        {
            NotificationManager.RemoveNotification(request, id);

            return true;
        }
    }
}
