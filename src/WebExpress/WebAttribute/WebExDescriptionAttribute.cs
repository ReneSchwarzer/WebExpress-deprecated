namespace WebExpress.WebAttribute
{
    public class WebExDescriptionAttribute : System.Attribute, IPluginAttribute, IApplicationAttribute, IModuleAttribute
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
