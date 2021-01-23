using WebExpress.WebResource;

namespace WebExpress.UI.WebControl
{
    public class RenderContextFormular : RenderContext
    {
        /// <summary>
        /// Das Formular, indem das Steuerelement gerendert wird
        /// </summary>
        public IControlFormular Formular { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die Seite, indem das Steuerelement gerendert wird</param>
        /// <param name="formular">Das Formular, indem das Steuerelement gerendert wird</param>
        public RenderContextFormular(IPage page, IControlFormular formular)
            : base(page)
        {
            Formular = formular;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement gerendert wird</param>
        /// <param name="formular">Das Formular, indem das Steuerelement gerendert wird</param>
        public RenderContextFormular(RenderContext context, IControlFormular formular)
            : base(context?.Page)
        {
            Formular = formular;
        }
    }
}
