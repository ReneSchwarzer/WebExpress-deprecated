using System.Linq;
using WebExpress.Attribute;
using WebExpress.Message;
using WebExpress.WebApp.WebNotificaation;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebAPI
{
    /// <summary>
    /// Ermittelt den Status und Forschritt einer Aufgabe (WebTask)
    /// </summary>
    [ID("PopupNotificationConfirmV1")]
    [Segment("popupconfirmnotification", "")]
    [Path("/api/v1")]
    [IncludeSubPaths(true)]
    [Module("webexpress.webapp")]
    [Optional]
    public sealed class ApiPopupNotificationConfirm : ResourceApi
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ApiPopupNotificationConfirm()
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
            var id = request.Uri.Path.Last().Value;

            NotificationManager.RemoveNotification(request, id);

            return true;
        }
    }
}
