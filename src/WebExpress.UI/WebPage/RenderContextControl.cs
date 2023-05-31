using WebExpress.WebMessage;
using WebExpress.WebPage;

namespace WebExpress.UI.WebPage
{
    public class RenderContextControl : RenderContext
    {
        /// <summary>
        /// Returns or sets the visual representation of the page.
        /// </summary>
        public new VisualTreeControl VisualTree
        {
            get { return base.VisualTree as VisualTreeControl; }
            set { base.VisualTree = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public RenderContextControl()
        {
            VisualTree = new VisualTreeControl();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="page">The page where the control is rendered.</param>
        /// <param name="request">The request.</param>
        /// <param name="visualTree">The visual tree.</param>
        public RenderContextControl(IPage page, Request request, VisualTreeControl visualTree)
            : base(page, request, visualTree)
        {
        }

        /// <summary>
        /// Copy-Constructor
        /// </summary>
        /// <param name="context">The render context to copy./param>
        public RenderContextControl(RenderContext context)
            : this(context?.Page, context?.Request, context?.VisualTree as VisualTreeControl)
        {
        }
    }
}
