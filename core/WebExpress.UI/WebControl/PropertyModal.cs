using WebExpress.WebUri;

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
        /// Die Größe des Modals
        /// </summary>
        public TypeModalSize Size { get; protected set; }

        /// <summary>
        /// Die Uri
        /// </summary>
        public IUri Uri { get; protected set; }

        /// <summary>
        /// Die Weiterleitungs-Uri
        /// </summary>
        public IUri RedirectUri { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">Der Typ</param>
        /// <param name="modal">Das benutzerdefinierte Modal oder null</param>
        public PropertyModal()
        {
            Type = TypeModal.None;
            Size = TypeModalSize.Default;
            Modal = null;
            Uri = null;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">Der Typ</param>
        /// <param name="size">Die Größe des Modals</param>
        public PropertyModal(TypeModal type, TypeModalSize size = TypeModalSize.Default)
        {
            Type = type;
            Size = size;
            Modal = null;
            Uri = null;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">Der Typ</param>
        /// <param name="modal">Das benutzerdefinierte Modal</param>
        /// <param name="size">Die Größe des Modals</param>
        public PropertyModal(TypeModal type, ControlModal modal, TypeModalSize size = TypeModalSize.Default)
        {
            Type = type;
            Size = size;
            Modal = modal;
            Uri = null;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">Der Typ</param>
        /// <param name="uri">Die Uri</param>
        /// <param name="size">Die Größe des Modals</param>
        public PropertyModal(TypeModal type, IUri uri, TypeModalSize size = TypeModalSize.Default)
        {
            Type = type;
            Size = size;
            Modal = null;
            Uri = uri;
        }
    }
}
