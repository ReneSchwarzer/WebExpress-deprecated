using System;
using WebExpress.WebAttribute;

namespace WebExpress.WebApp.WebAttribute
{
    /// <summary>
    /// Aktivierung von Optionen (z.B. WebEx.WebApp.Setting.SystemInformation für die Anzeige der Systeminformationen)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class OptionAttribute : System.Attribute, IResourceAttribute
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="option">Die Option, welche aktiviert werden soll</param>
        public OptionAttribute(string option)
        {

        }
    }
}
