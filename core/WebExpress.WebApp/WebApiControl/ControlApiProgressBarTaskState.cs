using WebExpress.Html;
using WebExpress.Module;
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
        /// Konstruktor
        /// </summary>
        /// <param name="id"></param>
        public ControlApiProgressBarTaskState(string id)
            :base(id)
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

            var code = @"setInterval(function ()  
            {
                $.ajax({ url: '" + module?.ContextPath.Append("api/v1/taskstatus") + @"', dataType: 'json' }).then(function(data)
                {
                    document.getElementById('" + ID + @"').firstElementChild.style.width = data.Progress + ""%"";
                });
            }, 1000);";


            context.VisualTree.AddScript("webexpress.webapp:controlapiprogressbartaskstate", code);


            return base.Render(context);
        }

    }
}
