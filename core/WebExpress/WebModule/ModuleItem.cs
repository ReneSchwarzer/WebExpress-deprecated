namespace WebExpress.WebModule
{
    /// <summary>
    /// Represents a module entry in the module directory.
    /// </summary>
    internal class ModuleItem
    {
        /// <summary>
        /// The context associated with the module.
        /// </summary>
        public IModuleContext Context { get; set; }

        /// <summary>
        /// The module.
        /// </summary>
        public IModule Module { get; set; }
    }
}
