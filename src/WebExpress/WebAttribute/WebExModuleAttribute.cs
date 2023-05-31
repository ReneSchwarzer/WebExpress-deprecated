using System;
using WebExpress.WebModule;

namespace WebExpress.WebAttribute
{
    /// <summary>
    /// Specifying a module.
    /// </summary>
    public class WebExModuleAttribute<T> : Attribute, IResourceAttribute, IModuleAttribute where T : IModule
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public WebExModuleAttribute()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="moduleId">Returns or sets the id. des Moduls</param>
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
