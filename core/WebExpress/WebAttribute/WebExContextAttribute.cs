using System;

namespace WebExpress.WebAttribute
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class WebExContextAttribute : System.Attribute, IResourceAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Der Name</param>
        public WebExContextAttribute(string name)
        {

        }
    }
}
