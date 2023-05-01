namespace WebExpress.WebAttribute
{
    public class WebExAssetPathAttribute : System.Attribute, WebExIApplicationAttribute, WebExIModuleAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="assetPath">Der Pfad für Assets</param>
        public WebExAssetPathAttribute(string assetPath)
        {

        }
    }
}
