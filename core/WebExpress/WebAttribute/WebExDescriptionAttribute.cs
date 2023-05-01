namespace WebExpress.WebAttribute
{
    public class WebExDescriptionAttribute : System.Attribute, IPluginAttribute, WebExIApplicationAttribute, WebExIModuleAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="description">Die Beschreibung</param>
        public WebExDescriptionAttribute(string description)
        {

        }
    }
}
