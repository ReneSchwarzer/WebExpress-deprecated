using WebExpress.UI.WebControl;
using WebExpress.WebPage;

namespace WebExpress.UI.WebFragment
{
    public interface IFragment : IControl
    {
        /// <summary>
        /// Liefert der Kontext des Fragmentes.
        /// </summary>
        IFragmentContext Context { get; }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">The page where the fragment is active.</param>
        void Initialization(IFragmentContext context, IPage page);
    }
}
