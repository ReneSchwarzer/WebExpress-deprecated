using System;

namespace WebExpress.WebAttribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class WebExParentAttribute : Attribute, IResourceAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The parent id.</param>
        public WebExParentAttribute(string id)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parentClass">The parent class.</param>
        public WebExParentAttribute(Type parentClass)
        {

        }
    }
}
