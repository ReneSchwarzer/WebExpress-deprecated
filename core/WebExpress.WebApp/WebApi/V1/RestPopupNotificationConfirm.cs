using System.Linq;
using WebExpress.Message;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebAttribute;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebAPI.V1
{
    /// <summary>
    /// Ermittelt den Status und Forschritt einer Aufgabe (WebTask)
    /// </summary>
    [ID("ApiPopupNotificationConfirmV1")]
    [Segment("popupconfirmnotification", "")]
    [Path("/api/v1")]
    [IncludeSubPaths(true)]
    [Module("webexpress.webapp")]
    [Optional]
    public sealed class RestPopupNotificationConfirm : ResourceRest
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public RestPopupNotificationConfirm()
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
        /// Verarbeitung des DELETE-Request
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Das Ergebnis der Löschung</returns>
        public override bool DeleteData(Request request)
        {
            var id = request.Uri.Path.Last().Value;

            NotificationManager.RemoveNotification(request, id);

            return true;
        }
    }
}
