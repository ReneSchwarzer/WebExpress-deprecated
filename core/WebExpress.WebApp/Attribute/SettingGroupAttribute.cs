using System;
using WebExpress.Attribute;

namespace WebExpress.WebApp.Attribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SettingGroupAttribute : System.Attribute, IResourceAttribute
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="name">Der Gruppenname</param>
        public SettingGroupAttribute(string name)
        {

        }
    }
}
