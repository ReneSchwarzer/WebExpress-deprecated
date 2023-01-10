using System;
using WebExpress.WebAttribute;

namespace WebExpress.WebApp.WebAttribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SettingGroupAttribute : System.Attribute, IResourceAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Der Gruppenname</param>
        public SettingGroupAttribute(string name)
        {

        }
    }
}
