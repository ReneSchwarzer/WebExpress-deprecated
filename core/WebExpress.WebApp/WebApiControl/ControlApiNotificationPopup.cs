using WebExpress.Html;
using WebExpress.Module;
using WebExpress.UI.WebControl;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebApiControl
{
    /// <summary>
    /// Popup-Benachrichtigungen
    /// </summary>
    public class ControlApiNotificationPopup : ControlPanel
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die Steuerelement-ID</param>
        public ControlApiNotificationPopup(string id = null)
            : base(id)
        {
            Classes.Add("popupnotification");
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var module = ModuleManager.GetModule(context.Application, "webexpress.webapp");
            var code = $"updatePopupNotification('{ ID }', '{ module?.ContextPath.Append("api/v1/popupnotifications") }', '{ module?.ContextPath.Append("api/v1/popupconfirmnotification") }')";

            context.VisualTree.AddScript("webexpress.webapp:controlapinotificationpopup", code);

            return base.Render(context);
        }
    }
}
