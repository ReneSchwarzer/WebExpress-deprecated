namespace WebExpress.WebAttribute
{
    public class WebExDataPathAttribute : System.Attribute, IApplicationAttribute, IModuleAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataPath">Der Pfad für die Daten</param>
        public WebExDataPathAttribute(string dataPath)
        {

        }
    }
}
