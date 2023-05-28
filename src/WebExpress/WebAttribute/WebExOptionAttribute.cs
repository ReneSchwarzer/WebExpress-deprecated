using System;

namespace WebExpress.WebAttribute
{
    /// <summary>
    /// Activation of options (e.g. 'webexpress.webapp.settinglog' or 'webexpress.webapp.*').
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class WebExOptionAttribute : Attribute, IApplicationAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="option">The option to activate.</param>
        public WebExOptionAttribute(string option)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="moduleClass">The module class.</param>
        public WebExOptionAttribute(Type moduleClass)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="moduleClass">The module or resource class.</param>
        /// <param name="resourceClass">The resource or resource class.</param>
        public WebExOptionAttribute(Type moduleClass, Type resourceClass)
        {

        }
    }
}
