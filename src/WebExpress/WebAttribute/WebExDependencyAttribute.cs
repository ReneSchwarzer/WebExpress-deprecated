using System;

namespace WebExpress.WebAttribute
{
    /// <summary>
    /// Marks a plugin as dependent on another plugin.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class WebExDependencyAttribute : System.Attribute, IPluginAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dependency">The Id of the plugin to which there is a dependency.</param>
        public WebExDependencyAttribute(string dependency)
        {

        }
    }
}
