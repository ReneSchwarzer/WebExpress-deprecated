using System;

namespace WebExpress.UI.WebFragment
{
    /// <summary>
    /// Fragments are components that can be integrated into pages to dynamically expand functionalities.
    /// </summary>
    internal class FragmentItem
    {
        /// <summary>
        /// The context of the fragment.
        /// </summary>
        public IFragmentContext Context { get; set; }

        /// <summary>
        /// The type of fragment.
        /// </summary>
        public Type Fragment { get; set; }

        /// <summary>
        /// The order of the fragment.
        /// </summary>
        public int Order { get; set; }
    }
}
