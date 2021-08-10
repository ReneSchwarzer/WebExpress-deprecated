using WebExpress.Attribute;
using WebExpress.UI.WebControl;

namespace WebExpress.WebApp.Attribute
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
