namespace WebExpress.WebAttribute
{
    public class IconAttribute : System.Attribute, IPluginAttribute, IApplicationAttribute, IModuleAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="icon">Das Icon</param>
        public IconAttribute(string icon)
        {

        }
    }
}
