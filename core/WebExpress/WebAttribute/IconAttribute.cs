namespace WebExpress.WebAttribute
{
    public class IconAttribute : System.Attribute, IPluginAttribute, IApplicationAttribute, IModuleAttribute
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="icon">Das Icon</param>
        public IconAttribute(string icon)
        {

        }
    }
}
