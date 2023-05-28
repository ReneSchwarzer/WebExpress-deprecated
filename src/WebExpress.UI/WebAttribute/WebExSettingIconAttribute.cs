using WebExpress.UI.WebControl;
using WebExpress.WebAttribute;

namespace WebExpress.UI.WebAttribute
{
    public class WebExSettingIconAttribute : System.Attribute, IResourceAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="icon">Das Icon</param>
        public WebExSettingIconAttribute(TypeIcon icon)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="icon">Das Icon</param>
        public WebExSettingIconAttribute(string icon)
        {

        }
    }
}
