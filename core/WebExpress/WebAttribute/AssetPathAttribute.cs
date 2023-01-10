namespace WebExpress.WebAttribute
{
    public class AssetPathAttribute : System.Attribute, IApplicationAttribute, IModuleAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="assetPath">Der Pfad für Assets</param>
        public AssetPathAttribute(string assetPath)
        {

        }
    }
}
