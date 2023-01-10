using WebExpress.Message;
using WebExpress.UI.WebPage;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebPage
{
    public class RenderContextWebApp : RenderContextControl
    {
        /// <summary>
        /// Liefert die visuelle Darstellung der Seite
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
        /// <param name="page">Die Seite, indem das Steuerelement gerendert wird</param>
        /// <param name="request">The request.</param>
        /// <param name="visualTree">Der visuelle Baum</param>
        public RenderContextWebApp(IPage page, Request request, VisualTreeWebApp visualTree)
            : base(page, request, visualTree)
        {
        }

        /// <summary>
        /// Copy-Constructor
        /// </summary>
        /// <param name="context">Der zu kopierende Kontext/param>
        public RenderContextWebApp(RenderContext context)
            : this(context?.Page, context?.Request, context?.VisualTree as VisualTreeWebApp)
        {
        }
    }
}
