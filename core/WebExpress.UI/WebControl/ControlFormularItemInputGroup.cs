using System.Collections.Generic;
using WebExpress.Html;

namespace WebExpress.UI.WebControl
{
    public class ControlFormularItemInputGroup : ControlFormularItemInput
    {
        /// <summary>
        /// Liefert oder setzt die Gruppe
        /// </summary>
        public ControlFormularItemGroup Group { get; private set; }

        /// <summary>
        /// Bestimmt ob die Eingabe gültig sind
        /// </summary>
        public override ICollection<ValidationResult> ValidationResults => Group != null ? Group.ValidationResults : new List<ValidationResult>();

        /// <summary>
        /// Ermittelt das schwerwiegenste Validierungsergebnis
        /// </summary>
        public override TypesInputValidity ValidationResult => Group != null ? Group.ValidationResult : TypesInputValidity.Default;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlFormularItemInputGroup(string id = null)
            : base(id)
        {
            Name = Id;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.None);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="group">Der Name The text.Box</param>
        public ControlFormularItemInputGroup(string id, ControlFormularItemGroup group)
            : base(id)
        {
            Name = id;
            Group = group;
        }

        /// <summary>
        /// Initializes the form element.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        public override void Initialize(RenderContextFormular context)
        {
            if (Group != null)
            {
                Group.Initialize(context);
            }
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContextFormular context)
        {
            return Group?.Render(context);
        }

        /// <summary>
        /// Prüft das Eingabeelement auf Korrektheit der Daten
        /// </summary>
        /// <param name="context">Der Kontext, indem die Eingaben validiert werden</param>
        public override void Validate(RenderContextFormular context)
        {
            if (Disabled)
            {
                return;
            }

            Group.Validate(context);
        }
    }
}
