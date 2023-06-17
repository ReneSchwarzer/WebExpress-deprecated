using System.Collections.Generic;
using WebExpress.Html;

namespace WebExpress.UI.WebControl
{
    public class ControlFormItemInputGroup : ControlFormItemInput
    {
        /// <summary>
        /// Returns or sets the group.
        /// </summary>
        public ControlFormItemGroup Group { get; private set; }

        /// <summary>
        /// Determines whether the inputs are valid.
        /// </summary>
        public override ICollection<ValidationResult> ValidationResults => Group != null ? Group.ValidationResults : new List<ValidationResult>();

        /// <summary>
        /// Returns the most serious validation result.
        /// </summary>
        public override TypesInputValidity ValidationResult => Group != null ? Group.ValidationResult : TypesInputValidity.Default;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlFormItemInputGroup(string id = null)
            : base(id)
        {
            Name = Id;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.None);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="group">The name.</param>
        public ControlFormItemInputGroup(string id, ControlFormItemGroup group)
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
        /// Checks the input element for correctness of the data.
        /// </summary>
        /// <param name="context">The context in which the inputs are validated.</param>
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
