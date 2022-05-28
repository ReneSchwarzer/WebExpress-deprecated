using System;
using System.Text;
using System.Text.Json;
using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.WebModule;
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
            : base(id ?? Guid.NewGuid().ToString())
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

            var settings = new
            {
                ID = "26E517F5-56F7-485E-A212-6033618708F3",
                RestUri = module?.ContextPath.Append("api/v1/popupnotifications")?.ToString(),
                Intervall = 15000
            };

            var jsonOptions = new JsonSerializerOptions { WriteIndented = false };
            var settingsJson = JsonSerializer.Serialize(settings, jsonOptions);
            var builder = new StringBuilder();

            builder.AppendLine($"{{");
            builder.AppendLine($"let settings = {settingsJson};");
            builder.AppendLine($"let container = $('#{ID}');");
            builder.AppendLine($"let obj = new webexpress.webapp.popupNotificationCtrl(settings);");
            builder.AppendLine($"container.replaceWith(obj.getCtrl);");
            builder.AppendLine($"}}");

            context.VisualTree.AddScript("webexpress.webapp:controlapinotificationpopup", builder.ToString());

            return base.Render(context);
        }
    }
}
