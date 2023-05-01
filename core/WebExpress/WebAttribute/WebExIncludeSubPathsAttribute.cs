namespace WebExpress.WebAttribute
{
    /// <summary>
    /// Bestimmt, ob alle Ressourcen unterhalb des angegebenen Pfades (inkl. Segment) mitverarbeitet werden.
    /// </summary>
    public class WebExIncludeSubPathsAttribute : System.Attribute, IResourceAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="includeSubPaths">Alle Subpfade werden mit berücksichtigt</param>
        public WebExIncludeSubPathsAttribute(bool includeSubPaths)
        {

        }
    }
}
