namespace WebExpress.WebAttribute
{
    public class ContextPathAttribute : System.Attribute, IApplicationAttribute, IModuleAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contetxPath">Der Kontextpfad</param>
        public ContextPathAttribute(string contetxPath)
        {

        }
    }
}
