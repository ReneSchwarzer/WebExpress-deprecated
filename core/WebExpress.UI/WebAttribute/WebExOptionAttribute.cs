using System;
using WebExpress.WebAttribute;

namespace WebExpress.UI.WebAttribute
{
    /// <summary>
    /// Aktivierung von Optionen (z.B. WebEx.WebApp.Setting.SystemInformation für die Anzeige der Systeminformationen)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class WebExOptionAttribute : System.Attribute, IResourceAttribute
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
