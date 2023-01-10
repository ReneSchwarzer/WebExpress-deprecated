namespace WebExpress.WebAttribute
{
    public class ApplicationAttribute : System.Attribute, IModuleAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="applicationID">Die ID der Anwendung</param>
        public ApplicationAttribute(string applicationID)
        {

        }
    }
}
