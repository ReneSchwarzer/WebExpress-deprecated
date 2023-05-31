using WebExpress.WebMessage;
using WebExpress.UI.WebPage;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebPage
{
    public class RenderContextWebApp : RenderContextControl
    {
        /// <summary>
        /// Returns or sets the visual representation of the page.
        /// </summary>
        public new VisualTreeWebApp VisualTree
        {
            get { return base.VisualTree as VisualTreeWebApp; }
            set { base.VisualTree = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public RenderContextWebApp()
        {
            VisualTree = new VisualTreeWebApp();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="page">The page where the control is rendered.</param>
        /// <param name="request">The request.</param>
        /// <param name="visualTree">The visual tree.</param>
        public RenderContextWebApp(IPage page, Request request, VisualTreeWebApp visualTree)
            : base(page, request, visualTree)
        {
        }

        /// <summary>
        /// Copy-Constructor
        /// </summary>
        /// <param name="context">The render context to copy./param>
        public RenderContextWebApp(RenderContext context)
            : this(context?.Page, context?.Request, context?.VisualTree as VisualTreeWebApp)
        {
        }
    }
}
