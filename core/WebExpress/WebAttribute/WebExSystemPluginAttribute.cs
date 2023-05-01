using System;

namespace WebExpress.WebAttribute
{
    /// <summary>
    /// Marks an assembly as systemically relevant.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class WebExSystemPluginAttribute : Attribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public WebExSystemPluginAttribute()
        {

        }
    }
}
