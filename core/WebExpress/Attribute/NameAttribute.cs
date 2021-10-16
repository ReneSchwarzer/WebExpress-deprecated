namespace WebExpress.Attribute
{
    public class NameAttribute : System.Attribute, IPluginAttribute, IApplicationAttribute, IModuleAttribute
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="name">Der Name</param>
        public NameAttribute(string name)
        {

        }
    }
}
