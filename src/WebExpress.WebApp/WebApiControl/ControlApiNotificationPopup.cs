using System;
using System.Text;
using System.Text.Json;
using WebExpress.WebHtml;
using WebExpress.UI.WebControl;
using WebExpress.WebComponent;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebApiControl
{
    /// <summary>
    /// Popup-Benachrichtigungen
    /// </summary>
    public class ControlApiNotificationPopup : ControlPanel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Die Steuerelement-Id</param>
        public ControlApiNotificationPopup(string id = null)
            : base(id ?? Guid.NewGuid().ToString())
        {
            Classes.Add("popupnotification");
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var module = ComponentManager.ModuleManager.GetModule(context.ApplicationContext, typeof(Module));

            var settings = new
            {
                id = "26E517F5-56F7-485E-A212-6033618708F3",
                resturi = module?.ContextPath.Append("api/v1/popupnotifications")?.ToString(),
                intervall = 15000
            };

            var jsonOptions = new JsonSerializerOptions { WriteIndented = false };
            var settingsJson = JsonSerializer.Serialize(settings, jsonOptions);
            var builder = new StringBuilder();

            builder.AppendLine($"$(document).ready(function () {{");
            builder.AppendLine($"let settings = {settingsJson};");
            builder.AppendLine($"let container = $('#{Id}');");
            builder.AppendLine($"let obj = new webexpress.webapp.popupNotificationCtrl(settings);");
            builder.AppendLine($"container.replaceWith(obj.getCtrl);");
            builder.AppendLine($"}});");

            context.VisualTree.AddScript("webexpress.webapp:controlapinotificationpopup", builder.ToString());

            return base.Render(context);
        }
    }
}
