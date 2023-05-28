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
        /// <param name="moduleId">Die Id des Moduls</param>
        public WebExModuleAttribute(string moduleId)
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
