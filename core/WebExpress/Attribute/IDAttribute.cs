namespace WebExpress.Attribute
{
    public class IDAttribute : System.Attribute, IResourceAttribute, IPluginAttribute, IApplicationAttribute, IModuleAttribute
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public IDAttribute(string id)
        {

        }
    }
}
