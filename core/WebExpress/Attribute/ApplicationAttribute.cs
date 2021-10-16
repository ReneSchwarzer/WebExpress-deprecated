namespace WebExpress.Attribute
{
    public class ApplicationAttribute : System.Attribute, IModuleAttribute
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="applicationID">Die ID der Anwendung</param>
        public ApplicationAttribute(string applicationID)
        {

        }
    }
}
