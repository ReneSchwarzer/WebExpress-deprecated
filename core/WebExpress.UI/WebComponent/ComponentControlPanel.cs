using WebExpress.UI.WebControl;
using WebExpress.WebPage;

namespace WebExpress.UI.WebComponent
{
    public class ComponentControlPanel : ControlPanel, IComponent
    {
        /// <summary>
        /// Liefert der Kontext
        /// </summary>
        public IComponentContext Context { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID der Komponente oder null</param>
        public ComponentControlPanel(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="page">Die Seite, indem die Komonente aktiv ist</param>
        public virtual void Initialization(IComponentContext context, IPage page)
        {
            Context = context;
        }
    }
}
