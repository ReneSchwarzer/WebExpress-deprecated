using System;

namespace WebExpress.WebAttribute
{
    /// <summary>
    /// Adds an id over the attribute of a class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class WebExIdAttribute : Attribute, IResourceAttribute, IPluginAttribute, IApplicationAttribute, IModuleAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public WebExIdAttribute(string id)
        {

        }
    }
}
