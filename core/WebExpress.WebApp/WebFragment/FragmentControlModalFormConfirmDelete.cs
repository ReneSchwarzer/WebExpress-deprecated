using WebExpress.UI.WebFragment;
using WebExpress.WebApp.WebControl;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebFragment
{
    public class FragmentControlModalFormConfirmDelete : ControlModalFormularConfirmDelete, IFragment
    {
        /// <summary>
        /// Liefert der Kontext
        /// </summary>
        public IFragmentContext Context { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id of the fragment or null.</param>
        public FragmentControlModalFormConfirmDelete(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">The page where the fragment is active.</param>
        public virtual void Initialization(IFragmentContext context, IPage page)
        {
            Context = context;
        }
    }
}
