namespace WebExpress.WebAttribute
{
    public class StatusCodeAttribute : System.Attribute, IApplicationAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="status">Der Statuscode</param>
        public StatusCodeAttribute(int status)
        {

        }
    }
}
