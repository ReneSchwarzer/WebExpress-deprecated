using WebExpress.UI.WebControl;
using WebExpress.WebAttribute;

namespace WebExpress.WebApp.WebAttribute
{
    public class SettingIconAttribute : System.Attribute, IResourceAttribute
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="icon">Das Icon</param>
        public SettingIconAttribute(TypeIcon icon)
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="icon">Das Icon</param>
        public SettingIconAttribute(string icon)
        {

        }
    }
}
