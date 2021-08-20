using System;
using WebExpress.Attribute;

namespace WebExpress.WebApp.Attribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SettingContextAttribute : System.Attribute, IResourceAttribute
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Der Kontext, indem die Einstellungsseite zugeordnet wird</param>
        public SettingContextAttribute(string context)
        {

        }
    }
}
