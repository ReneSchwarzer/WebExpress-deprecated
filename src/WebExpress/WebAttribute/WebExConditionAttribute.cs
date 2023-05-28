using System;

namespace WebExpress.WebAttribute
{
    /// <summary>
    /// Activation of options (e.g. WebEx.WebApp.Setting.SystemInformation for displaying system information).
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class WebExConditionAttribute : Attribute, IResourceAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="condition">Die Bedingung</param>
        public WebExConditionAttribute(Type condition)
        {

        }
    }
}
