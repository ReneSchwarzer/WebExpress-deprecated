using System;

namespace WebExpress.WebAttribute
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class WebExContextPathAttribute : Attribute, IApplicationAttribute, IModuleAttribute, IResourceAttribute
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
