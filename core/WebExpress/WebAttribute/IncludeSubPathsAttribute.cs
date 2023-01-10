namespace WebExpress.WebAttribute
{
    /// <summary>
    /// Bestimmt, ob alle Ressourcen unterhalb des angegebenen Pfades (inkl. Segment) mitverarbeitet werden.
    /// </summary>
    public class IncludeSubPathsAttribute : System.Attribute, IResourceAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="includeSubPaths">Alle Subpfade werden mit berücksichtigt</param>
        public IncludeSubPathsAttribute(bool includeSubPaths)
        {

        }
    }
}
