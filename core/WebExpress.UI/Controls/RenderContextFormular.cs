using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class RenderContextFormular : RenderContext
    {
        /// <summary>
        /// Das Formular, indem das Steuerelement gerendert wird
        /// </summary>
        public ControlFormular Formular { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die Seite, indem das Steuerelement gerendert wird</param>
        /// <param name="formular">Das Formular, indem das Steuerelement gerendert wird</param>
        public RenderContextFormular(IPage page, ControlFormular formular)
            : base(page)
        {
            Formular = formular;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement gerendert wird</param>
        /// <param name="formular">Das Formular, indem das Steuerelement gerendert wird</param>
        public RenderContextFormular(RenderContext context, ControlFormular formular)
            : base(context?.Page)
        {
            Formular = formular;
        }
    }
}
