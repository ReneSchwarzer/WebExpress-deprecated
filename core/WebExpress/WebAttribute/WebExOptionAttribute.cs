using System;

namespace WebExpress.WebAttribute
{
    /// <summary>
    /// Activation of options (e.g. 'webexpress.webapp.settinglog' or 'webexpress.webapp.*').
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class WebExOptionAttribute : Attribute, IApplicationAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="option">The option to activate.</param>
        public WebExOptionAttribute(string option)
        {

        }
    }
}
