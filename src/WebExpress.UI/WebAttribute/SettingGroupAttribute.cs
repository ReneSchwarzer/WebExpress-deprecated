using System;
using WebExpress.WebAttribute;

namespace WebExpress.UI.WebAttribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SettingGroupAttribute : System.Attribute, IResourceAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The group name.</param>
        public SettingGroupAttribute(string name)
        {

        }
    }
}
