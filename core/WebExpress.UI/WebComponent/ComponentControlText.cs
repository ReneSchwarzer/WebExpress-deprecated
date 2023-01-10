using WebExpress.UI.WebControl;
using WebExpress.WebPage;

namespace WebExpress.UI.WebComponent
{
    public class ComponentControlText : ControlText, IComponent
    {
        /// <summary>
        /// Liefert der Kontext
        /// </summary>
        public IComponentContext Context { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Die ID der Komponente oder null</param>
        public ComponentControlText(string id = null)
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
