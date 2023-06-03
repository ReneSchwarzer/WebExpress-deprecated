using System;
using WebExpress.WebResource;

namespace WebExpress.WebAttribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class WebExParentAttribute<T> : Attribute, IResourceAttribute where T : class, IResource
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public WebExParentAttribute()
        {

        }
    }
}
