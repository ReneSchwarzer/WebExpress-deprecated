using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Message;
using WebExpress.UI.WebControl;
using WebExpress.WebPage;

namespace WebExpress.UI.WebFragment
{
    public class FragmentCacheItem
    {
        /// <summary>
        /// Returns the type.
        /// </summary>
        private Type Type { get; set; }

        /// <summary>
        /// Returns or sets the fragment instance.
        /// </summary>
        private IFragment Instance { get; set; }

        /// <summary>
        /// Provides the context of the fragment.
        /// </summary>
        public IFragmentContext Context { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">The context of the fragment.</param>
        /// <param name="type">The type.</param>
        public FragmentCacheItem(IFragmentContext context, Type type)
        {
            Context = context;
            Type = type;
        }

        /// <summary>
        /// Creates a new instance or returns a cached instance.
        /// </summary>
        /// <param name="page">The page in which the fragment is active.</param>
        /// <param name="request">The request.</param>
        /// <returns>An enumeration of the fragment instances.</returns>
        public IEnumerable<T> CreateInstance<T>(IPage page, Request request) where T : IControl
        {
            if (!CheckControl(request))
            {
                return new List<T>();
            }
            else if (Context.Cache && Instance != null)
            {
                return new List<T>() { (T) Instance };
            }

            if (Type.Assembly.CreateInstance(Type.FullName) is IFragment instance)
            {
                instance.Initialization(Context, page);

                if (Context.Cache)
                {
                    Instance = instance;
                }

                return new List<T>() { (T)instance };
            }
            else if (Type.Assembly.CreateInstance(Type.FullName) is IFragmentDynamic dynamicInstance)
            {
                dynamicInstance.Initialization(Context, page);

                return dynamicInstance.Create<T>();
            }

            return new List<T>();
        }

        /// <summary>
        /// Checks the component to see if they are displayed or disabled.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>True the fragment is active, false otherwise.</returns>
        private bool CheckControl(Request request)
        {
            return !Context.Conditions.Any() || Context.Conditions.All(x => x.Fulfillment(request));
        }
    }
}
