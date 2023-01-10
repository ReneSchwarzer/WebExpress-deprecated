using System;

namespace WebExpress.WebAttribute
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class ContextAttribute : System.Attribute, IResourceAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Der Name</param>
        public ContextAttribute(string name)
        {

        }
    }
}
