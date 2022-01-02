using System;
using WebExpress.WebAttribute;

namespace WebExpress.WebApp.WebAttribute
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
