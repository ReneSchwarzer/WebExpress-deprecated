using System;
using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.WebComponent;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebApiControl
{
    /// <summary>
    /// Fortschrittsbalken für Aufgaben (Task)
    /// </summary>
    public class ControlApiProgressBarTaskState : ControlProgressBar
    {
        /// <summary>
        /// Java-Script-Funktion, welche aufgerufen wird, wenn die Aufgabe abgeschlossen ist 
        /// </summary>
        public string OnFinishScript { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlApiProgressBarTaskState(string id)
            : base(id ?? Guid.NewGuid().ToString())
        {
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var module = ComponentManager.ModuleManager.GetModule(context.ApplicationContext, typeof(Module));
            var code = $"updateTaskProgressBar('{Id}', '{module?.ContextPath.Append("api/v1/taskstatus")}', {OnFinishScript});";

            context.VisualTree.AddScript("webexpress.webapp:controlapiprogressbartaskstate", code);


            return base.Render(context);
        }

    }
}
