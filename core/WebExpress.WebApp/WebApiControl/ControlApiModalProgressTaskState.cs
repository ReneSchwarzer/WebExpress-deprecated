using WebExpress.Html;
using WebExpress.WebModule;
using WebExpress.UI.WebControl;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebApiControl
{
    /// <summary>
    /// Dialog, welcher die Fortschrittsanzeige eines WebTask enthällt
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
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlApiModalProgressTaskState(string id)
            : base(id)
        {
            ProgressBar = new ControlProgressBar($"progressbar_{ ID }")
            {
                Margin = new PropertySpacingMargin(PropertySpacing.Space.Two),
                Color = new PropertyColorProgress(TypeColorProgress.Primary),
                Format = TypeFormatProgress.Animated
            };

            Message = new ControlText($"message_{ ID }")
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
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var module = ModuleManager.GetModule(context.Application, "webexpress.webapp");
            var code = $"updateTaskModal('{ ID }', '{ module?.ContextPath.Append("api/v1/taskstatus") }')";


            context.VisualTree.AddScript("webexpress.webapp:controlapimodalprogresstaskstate", code);

            return base.Render(context);
        }
    }
}
