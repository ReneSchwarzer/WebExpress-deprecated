using System;
using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.WebComponent;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebApiControl
{
    /// <summary>
    /// Dialog, welcher die Fortschrittsanzeige eines WebTask enthält
    /// </summary>
    public class ControlApiModalProgressTaskState : ControlModal
    {
        /// <summary>
        /// Liefert die Fortschrittsanzeige
        /// </summary>
        private ControlProgressBar ProgressBar { get; set; }

        /// <summary>
        /// Liefert die Fortschrittnachricht
        /// </summary>
        private ControlText Message { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlApiModalProgressTaskState(string id)
            : base(id ?? Guid.NewGuid().ToString())
        {
            ProgressBar = new ControlProgressBar($"progressbar_{Id}")
            {
                Margin = new PropertySpacingMargin(PropertySpacing.Space.Two),
                Color = new PropertyColorProgress(TypeColorProgress.Primary),
                Format = TypeFormatProgress.Animated
            };

            Message = new ControlText($"message_{Id}")
            {
                Margin = new PropertySpacingMargin(PropertySpacing.Space.Two),
                TextColor = new PropertyColorText(TypeColorText.Secondary)
            };

            Fade = false;
            ShowIfCreated = true;

            Content.Add(ProgressBar);
            Content.Add(Message);
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var module = ComponentManager.ModuleManager.GetModule(context.ApplicationContext, "webexpress.webapp");
            var code = $"updateTaskModal('{Id}', '{module?.ContextPath.Append("api/v1/taskstatus")}')";


            context.VisualTree.AddScript("webexpress.webapp:controlapimodalprogresstaskstate", code);

            return base.Render(context);
        }
    }
}
