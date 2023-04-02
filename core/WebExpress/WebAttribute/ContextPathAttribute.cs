using System;

namespace WebExpress.WebAttribute
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class ContextPathAttribute : System.Attribute, IApplicationAttribute, IModuleAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contetxPath">The context path.</param>
        public ContextPathAttribute(string contetxPath)
        {

        }
    }
}
