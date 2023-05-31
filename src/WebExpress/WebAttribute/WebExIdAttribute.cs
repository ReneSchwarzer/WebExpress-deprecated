using System;

namespace WebExpress.WebAttribute
{
    /// <summary>
    /// Adds an id over the attribute of a class.
    /// </summary>
    [Obsolete("In the future, the id will be determined from the class names.")]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class WebExIdAttribute : Attribute, IResourceAttribute, IPluginAttribute, IApplicationAttribute, IModuleAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
#pragma warning disable IDE0060
        public WebExIdAttribute(string id)
#pragma warning restore IDE0060
        {

        }
    }
}
