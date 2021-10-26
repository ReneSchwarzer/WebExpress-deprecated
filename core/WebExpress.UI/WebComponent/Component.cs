using WebExpress.UI.WebControl;

namespace WebExpress.UI.WebComponent
{
    public class Component<T> : IComponent where T : Control, new()
    {
        /// <summary>
        /// Liefert der Kontext
        /// </summary>
        public ComponentContext Context { get; set; }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public void Initialization(IComponentContext context)
        {
        }
    }
}
