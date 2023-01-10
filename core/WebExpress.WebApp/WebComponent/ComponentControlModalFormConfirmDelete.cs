using WebExpress.UI.WebComponent;
using WebExpress.WebApp.WebControl;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebComponent
{
    public class ComponentControlModalFormConfirmDelete : ControlModalFormularConfirmDelete, IComponent
    {
        /// <summary>
        /// Liefert der Kontext
        /// </summary>
        public IComponentContext Context { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Die ID der Komponente oder null</param>
        public ComponentControlModalFormConfirmDelete(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public virtual void Initialization(IComponentContext context, IPage page)
        {
            Context = context;
        }
    }
}
