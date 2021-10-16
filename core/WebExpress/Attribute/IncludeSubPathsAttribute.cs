namespace WebExpress.Attribute
{
    public class IncludeSubPathsAttribute : System.Attribute, IResourceAttribute
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="includeSubPaths">Alle Subpfade werden mit berücksichtigt</param>
        public IncludeSubPathsAttribute(bool includeSubPaths)
        {

        }
    }
}
