using WebExpress.UI.WebControl;

namespace WebExpress.UI.WebComponent
{
    public interface IComponent : IControl
    {
        /// <summary>
        /// Liefert der Kontext
        /// </summary>
        IComponentContext Context { get; }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        void Initialization(IComponentContext context);
    }
}
