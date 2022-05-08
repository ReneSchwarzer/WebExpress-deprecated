using System;

namespace WebExpress.UI.WebComponent
{
    /// <summary>
    /// Repräsentiert ein Komponeteneintrag im Komponentenverzeichnis
    /// </summary>
    internal class ComponentItem
    {
        /// <summary>
        /// Der zur Komponente zugehörige Kontext
        /// </summary>
        public IComponentContext Context { get; set; }

        /// <summary>
        /// Der Komoponententyp
        /// </summary>
        public Type Component { get; set; }

        /// <summary>
        /// Die Reigenfolge der Komponente
        /// </summary>
        public int Order { get; set; }
    }
}
