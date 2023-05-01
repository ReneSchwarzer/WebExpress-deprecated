namespace WebExpress.WebAttribute
{
    public class WebExDataPathAttribute : System.Attribute, WebExIApplicationAttribute, WebExIModuleAttribute
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
