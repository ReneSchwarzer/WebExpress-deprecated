using WebExpress.Html;
using WebExpress.WebModule;
using WebExpress.UI.WebControl;
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
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlApiProgressBarTaskState(string id)
            : base(id)
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var module = ModuleManager.GetModule(context.Application, "webexpress.webapp");
            var code = $"updateTaskProgressBar('{ ID }', '{ module?.ContextPath.Append("api/v1/taskstatus") }', { OnFinishScript });";

            context.VisualTree.AddScript("webexpress.webapp:controlapiprogressbartaskstate", code);


            return base.Render(context);
        }

    }
}
