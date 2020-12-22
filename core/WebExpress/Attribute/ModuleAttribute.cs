using System;

namespace WebExpress.Attribute
{
    public class ModuleAttribute : System.Attribute, IResourceAttribute
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="moduleID">Die ID des Moduls</param>
        public ModuleAttribute(string moduleID)
        {

        }
    }
}
