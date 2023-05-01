namespace WebExpress.WebAttribute
{
    public class WebExIconAttribute : System.Attribute, IPluginAttribute, WebExIApplicationAttribute, WebExIModuleAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="icon">Das Icon</param>
        public WebExIconAttribute(string icon)
        {

        }
    }
}
