using System;

namespace WebExpress.WebAttribute
{
    /// <summary>
    /// Specifying a module.
    /// </summary>
    public class WebExModuleAttribute : Attribute, IResourceAttribute, IModuleAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="moduleID">Die ID des Moduls</param>
        public WebExModuleAttribute(string moduleID)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="moduleClass">The module class.</param>
        public WebExModuleAttribute(Type moduleClass)
        {

        }
    }
}
