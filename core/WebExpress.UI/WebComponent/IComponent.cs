using WebExpress.UI.WebControl;
using WebExpress.WebPage;

namespace WebExpress.UI.WebComponent
{
    public interface IComponent : IControl
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
    }
}
