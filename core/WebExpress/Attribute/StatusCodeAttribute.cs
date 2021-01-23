using System;

namespace WebExpress.Attribute
{
    public class StatusCodeAttribute : System.Attribute, IApplicationAttribute
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="status">Der Statuscode</param>
        public StatusCodeAttribute(int status)
        {

        }
    }
}
