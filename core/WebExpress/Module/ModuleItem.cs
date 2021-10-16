using WebExpress.Application;

namespace WebExpress.Module
{
    /// <summary>
    /// Repräsentiert ein Moduleintrag im Mudulverzeichnis
    /// </summary>
    internal class ModuleItem
    {
        /// <summary>
        /// Der zum Modul zugehörige Kontext
        /// </summary>
        public IModuleContext Context { get; set; }

        /// <summary>
        /// Das Modul
        /// </summary>
        public IModule Module { get; set; }

        /// <summary>
        /// Die zugehörige Anwendung
        /// </summary>
        public IApplicationContext Application { get; set; }
    }
}
