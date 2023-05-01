using System;

namespace WebExpress.WebAttribute
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class WebExContextPathAttribute : Attribute, WebExIApplicationAttribute, WebExIModuleAttribute, IResourceAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contetxPath">The context path.</param>
        public WebExContextPathAttribute(string contetxPath)
        {

        }
    }
}
