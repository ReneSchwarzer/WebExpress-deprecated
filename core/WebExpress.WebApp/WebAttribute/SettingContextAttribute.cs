using System;
using WebExpress.WebAttribute;

namespace WebExpress.WebApp.WebAttribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SettingContextAttribute : System.Attribute, IResourceAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Der Kontext, indem die Einstellungsseite zugeordnet wird</param>
        public SettingContextAttribute(string context)
        {

        }
    }
}
