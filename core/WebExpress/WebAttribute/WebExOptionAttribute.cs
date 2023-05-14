using System;

namespace WebExpress.WebAttribute
{
    /// <summary>
    /// Aktivierung von Optionen (z.B. WebEx.WebApp.Setting.SystemInformation für die Anzeige der Systeminformationen)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class WebExOptionAttribute : System.Attribute, IApplicationAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="option">Die Option, welche aktiviert werden soll</param>
        public WebExOptionAttribute(string option)
        {

        }
    }
}
