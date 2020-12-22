using System;

namespace WebExpress.Attribute
{
    public class DescriptionAttribute : System.Attribute, IPluginAttribute, IApplicationAttribute, IModuleAttribute
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="description">Die Beschreibung</param>
        public DescriptionAttribute(string description)
        {

        }
    }
}
