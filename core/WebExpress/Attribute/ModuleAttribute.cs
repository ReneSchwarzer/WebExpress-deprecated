namespace WebExpress.Attribute
{
    /// <summary>
    /// Angabe der ModulID
    /// </summary>
    public class ModuleAttribute : System.Attribute, IResourceAttribute, IModuleAttribute
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="moduleID">Die ID des Moduls</param>
        public ModuleAttribute(string moduleID)
        {

        }
    }
}
