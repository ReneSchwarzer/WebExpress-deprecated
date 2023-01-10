using System.Collections.Generic;
using WebExpress.UI.WebControl;
using WebExpress.WebPage;

namespace WebExpress.UI.WebComponent
{
    public interface IComponentDynamic
    {
        /// <summary>
        /// Liefert der Kontext
        /// </summary>
        IComponentContext Context { get; }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        void Initialization(IComponentContext context, IPage page);

        /// <summary>
        /// Erstellt Komponenten eines gemeinsammen Typs T
        /// </summary>
        /// <returns>Die erzeugten Steuerelement</returns>
        IEnumerable<T> Create<T>() where T : IControl;
    }
}
