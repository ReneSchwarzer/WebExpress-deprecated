using WebExpress.Message;
using WebExpress.WebPage;

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
        /// <param name="request">Die Anfrage</param>
        /// <param name="visualTree">Der visuelle Baum</param>
        /// <param name="formular">Das Formular, indem das Steuerelement gerendert wird</param>
        public RenderContextFormular(IPage page, Request request, IVisualTree visualTree, IControlFormular formular)
            : base(page, request, visualTree)
        {
            Formular = formular;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement gerendert wird</param>
        /// <param name="formular">Das Formular, indem das Steuerelement gerendert wird</param>
        public RenderContextFormular(RenderContext context, IControlFormular formular)
            : base(context)
        {
            Formular = formular;
        }

        /// <summary>
        /// Copy-Konstruktor
        /// </summary>
        /// <param name="context">Der zu kopierende Kontext/param>
        public RenderContextFormular(RenderContextFormular context)
            : this(context, context?.Formular)
        {
        }
    }
}
