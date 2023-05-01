namespace WebExpress.WebAttribute
{
    public class WebExNameAttribute : System.Attribute, IPluginAttribute, WebExIApplicationAttribute, WebExIModuleAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Der Name</param>
        public WebExNameAttribute(string name)
        {

        }
    }
}
