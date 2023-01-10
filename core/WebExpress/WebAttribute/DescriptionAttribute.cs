namespace WebExpress.WebAttribute
{
    public class DescriptionAttribute : System.Attribute, IPluginAttribute, IApplicationAttribute, IModuleAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="description">Die Beschreibung</param>
        public DescriptionAttribute(string description)
        {

        }
    }
}
