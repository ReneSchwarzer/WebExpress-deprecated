using System;
using WebExpress.Attribute;

namespace WebExpress.WebApp.Attribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SettingSectionAttribute : System.Attribute, IResourceAttribute
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="section">Die Sektion, indem die Einstellungsseite aufgelistet wird</param>
        public SettingSectionAttribute(SettingSection section)
        {

        }
    }
}
