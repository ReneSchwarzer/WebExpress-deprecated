using System;

namespace WebExpress.WebAttribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class WebExStatusCodeAttribute : System.Attribute, IApplicationAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="status">Der Statuscode</param>
        public WebExStatusCodeAttribute(int status)
        {

        }
    }
}
