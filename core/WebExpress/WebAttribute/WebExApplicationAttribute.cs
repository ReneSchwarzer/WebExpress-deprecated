using System;

namespace WebExpress.WebAttribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class WebExApplicationAttribute : Attribute, IModuleAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="applicationID">The id of the application.</param>
        public WebExApplicationAttribute(string applicationID)
        {

        }
    }
}
