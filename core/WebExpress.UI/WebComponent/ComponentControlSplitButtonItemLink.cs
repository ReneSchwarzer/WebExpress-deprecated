using WebExpress.UI.WebControl;

namespace WebExpress.UI.WebComponent
{
    public class ComponentControlSplitButtonItemLink : ControlSplitButtonItemLink, IComponent
    {
        /// <summary>
        /// Liefert der Kontext
        /// </summary>
        public IComponentContext Context { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID der Komponente oder null</param>
        public ComponentControlSplitButtonItemLink(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public virtual void Initialization(IComponentContext context)
        {
            Context = context;
        }
    }
}
