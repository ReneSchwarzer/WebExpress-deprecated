using System;

namespace WebExpress.Attribute
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class PathAttribute : System.Attribute, IResourceAttribute
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="path">Der Pfad</param>
        public PathAttribute(string path)
        {

        }
    }
}
