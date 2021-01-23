using System.Collections.Generic;
using WebExpress.UI.WebControl;

namespace WebExpress.UI.Component
{
    public interface IComponentDynamic
    {
        /// <summary>
        /// Erstellt Komponenten eines gemeinsammen Typs T
        /// </summary>
        /// <returns>Die erzeugten Komponenten</returns>
        IEnumerable<T> Create<T>() where T : IControl;
    }
}
