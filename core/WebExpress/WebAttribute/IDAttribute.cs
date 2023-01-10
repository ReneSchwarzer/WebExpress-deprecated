namespace WebExpress.WebAttribute
{
    public class IdAttribute : System.Attribute, IResourceAttribute, IPluginAttribute, IApplicationAttribute, IModuleAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Die ID</param>
        public IdAttribute(string id)
        {

        }
    }
}
