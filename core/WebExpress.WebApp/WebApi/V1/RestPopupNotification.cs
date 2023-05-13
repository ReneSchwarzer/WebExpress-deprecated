using System.Collections;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebAttribute;
using WebExpress.WebComponent;
using WebExpress.WebMessage;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebAPI.V1
{
    /// <summary>
    /// Ermittelt den Status und Forschritt einer Aufgabe (WebTask)
    /// </summary>
    [WebExID("ApiPopupNotificationV1")]
    [WebExSegment("popupnotifications", "")]
    [WebExContextPath("/api/v1")]
    [WebExModule("webexpress.webapp")]
    [WebExIncludeSubPaths(true)]
    [WebExOptional]
    public sealed class RestPopupNotification : ResourceRest
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RestPopupNotification()
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Processing of the resource. des GET-Request
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Eine Aufzählung, welche mittels JsonSerializer serialisiert werden kann.</returns>
        public override ICollection GetData(Request request)
        {
            return (ICollection)ComponentManager.GetComponent<NotificationManager>()?.GetNotifications(request);
        }

        /// <summary>
        /// Processing of the resource. des DELETE-Request
        /// </summary>
        /// <param name="id">Die zu löschende ID</param>
        /// <param name="request">The request.</param>
        /// <returns>Das Ergebnis der Löschung</returns>
        public override bool DeleteData(string id, Request request)
        {
            ComponentManager.GetComponent<NotificationManager>()?.RemoveNotification(request, id);

            return true;
        }
    }
}
