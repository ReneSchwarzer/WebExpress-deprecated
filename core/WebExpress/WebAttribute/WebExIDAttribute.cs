namespace WebExpress.WebAttribute
{
    public class WebExIDAttribute : System.Attribute, IResourceAttribute, IPluginAttribute, WebExIApplicationAttribute, WebExIModuleAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Die ID</param>
        public WebExIDAttribute(string id)
        {

        }
    }
}
