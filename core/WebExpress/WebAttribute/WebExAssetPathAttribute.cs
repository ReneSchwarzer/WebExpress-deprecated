namespace WebExpress.WebAttribute
{
    public class WebExAssetPathAttribute : System.Attribute, IApplicationAttribute, IModuleAttribute
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
