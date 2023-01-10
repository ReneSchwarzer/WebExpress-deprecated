namespace WebExpress.WebAttribute
{
    public class DataPathAttribute : System.Attribute, IApplicationAttribute, IModuleAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataPath">Der Pfad für die Daten</param>
        public DataPathAttribute(string dataPath)
        {

        }
    }
}
