using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class RenderContext
    {
        /// <summary>
        /// Die Seite, indem das Steuerelement gerendert wird
        /// </summary>
        public IPage Page { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die Seite, indem das Steuerelement gerendert wird</param>
        public RenderContext(IPage page)
        {
            Page = page;
        }
    }
}
