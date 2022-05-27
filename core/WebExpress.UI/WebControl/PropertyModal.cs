namespace WebExpress.UI.WebControl
{
    public class PropertyModal
    {
        /// <summary>
        /// Der Typ
        /// </summary>
        public TypeModal Type { get; protected set; }

        /// <summary>
        /// Das benutzerdefinierte Modal
        /// </summary>
        public ControlModal Modal { get; protected set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="type">Der Typ</param>
        /// <param name="modal">Das benutzerdefinierte Modal oder null</param>
        public PropertyModal()
        {
            Type = TypeModal.None;
            Modal = null;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="type">Der Typ</param>
        /// <param name="modal">Das benutzerdefinierte Modal oder null</param>
        public PropertyModal(TypeModal type, ControlModal modal = null)
        {
            Type = type;
            Modal = modal;
        }
    }
}
