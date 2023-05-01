namespace WebExpress.WebAttribute
{
    /// <summary>
    /// Angabe der ModulID
    /// </summary>
    public class WebExModuleAttribute : System.Attribute, IResourceAttribute, WebExIModuleAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="moduleID">Die ID des Moduls</param>
        public WebExModuleAttribute(string moduleID)
        {

        }
    }
}
