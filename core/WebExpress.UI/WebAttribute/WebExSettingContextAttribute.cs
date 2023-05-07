using System;
using WebExpress.WebAttribute;

namespace WebExpress.UI.WebAttribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class WebExSettingContextAttribute : System.Attribute, IResourceAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Der Kontext, indem die Einstellungsseite zugeordnet wird</param>
        public WebExSettingContextAttribute(string context)
        {

        }
    }
}
