using System;

namespace WebExpress.WebAttribute
{
    /// <summary>
    /// Markeirt eine Ressource als optional. Diese wird aktiv, wenn in der Anwendung die Option angegeben wird.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class OptionalAttribute : System.Attribute, IResourceAttribute
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public OptionalAttribute()
        {

        }
    }
}
