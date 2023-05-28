using System;
using WebExpress.WebAttribute;

namespace WebExpress.UI.WebAttribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class WebExSettingSectionAttribute : System.Attribute, IResourceAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="section">Die Sektion, indem die Einstellungsseite aufgelistet wird</param>
        public WebExSettingSectionAttribute(WebExSettingSection section)
        {

        }
    }
}
