using System;

namespace WebExpress.Attribute
{
    public class ContextPathAttribute : System.Attribute, IApplicationAttribute, IModuleAttribute
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="contetxPath">Der Kontextpfad</param>
        public ContextPathAttribute(string contetxPath)
        {

        }
    }
}
