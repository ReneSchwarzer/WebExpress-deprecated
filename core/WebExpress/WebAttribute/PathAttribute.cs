using System;

namespace WebExpress.WebAttribute
{
    [Obsolete]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class PathAttribute : System.Attribute, IResourceAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="path">Der Pfad</param>
        public PathAttribute(string path)
        {

        }
    }
}
