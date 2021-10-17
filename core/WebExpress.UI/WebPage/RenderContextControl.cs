using WebExpress.Message;
using WebExpress.WebPage;

namespace WebExpress.UI.WebPage
{
    public class RenderContextControl : RenderContext
    {
        /// <summary>
        /// Liefert die visuelle Darstellung der Seite
        /// </summary>
        public new VisualTreeControl VisualTree
        {
            get { return base.VisualTree as VisualTreeControl; }
            set { base.VisualTree = value; }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public RenderContextControl()
        {
            VisualTree = new VisualTreeControl();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die Seite, indem das Steuerelement gerendert wird</param>
        /// <param name="request">Die Anfrage</param>
        /// <param name="visualTree">Der visuelle Baum</param>
        public RenderContextControl(IPage page, Request request, VisualTreeControl visualTree)
            : base(page, request, visualTree)
        {
        }

        /// <summary>
        /// Copy-Konstruktor
        /// </summary>
        /// <param name="context">Der zu kopierende Kontext/param>
        public RenderContextControl(RenderContext context)
            : this(context?.Page, context?.Request, context?.VisualTree as VisualTreeControl)
        {
        }
    }
}
