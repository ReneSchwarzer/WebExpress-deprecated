using WebExpress.Application;
using WebExpress.WebResource;

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
        public ApplicationItem Application { get; set; }
    }
}
