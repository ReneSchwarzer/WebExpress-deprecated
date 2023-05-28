using System;
using WebExpress.WebAttribute;

namespace WebExpress.UI.WebAttribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class WebExSettingGroupAttribute : System.Attribute, IResourceAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Der Gruppenname</param>
        public WebExSettingGroupAttribute(string name)
        {

        }
    }
}
