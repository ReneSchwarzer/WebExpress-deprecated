﻿using WebExpress.UI.WebControl;
using WebExpress.WebPage;

namespace WebExpress.UI.WebFragment
{
    public class FragmentControlFormularInline : ControlFormularInline, IFragment
    {
        /// <summary>
        /// Liefert der Kontext
        /// </summary>
        public IFragmentContext FragmentContext { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id of the fragment or null.</param>
        public FragmentControlFormularInline(string id = null)
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
            FragmentContext = context;
        }
    }
}
