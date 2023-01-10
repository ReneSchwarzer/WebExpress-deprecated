using System;

namespace WebExpress.WebAttribute
{
    /// <summary>
    /// Kennzeichnet eine Seite oder eine Komponente, dass diese wiederverwendet werden kann
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class CacheAttribute : System.Attribute, IResourceAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CacheAttribute()
        {

        }
    }
}
