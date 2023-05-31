using System.Collections.Generic;
using System.Linq;

namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Grouping of controls.
    /// </summary>
    public abstract class ControlFormItemGroup : ControlFormItem, IFormularValidation
    {
        /// <summary>
        /// Returns or sets the form items.
        /// </summary>
        public ICollection<ControlFormItem> Items { get; } = new List<ControlFormItem>();

        /// <summary>
        /// Determines whether the inputs are valid.
        /// </summary>
        public ICollection<ValidationResult> ValidationResults { get; } = new List<ValidationResult>();

        /// <summary>
        /// Returns or sets whether the form element has been validated.
        /// </summary>
        private bool IsValidated { get; set; }

        /// <summary>
        /// Returns the most serious validation result.
        /// </summary>
        public virtual TypesInputValidity ValidationResult
        {
            get
            {
                var buf = ValidationResults;

                if (buf.Where(x => x.Type == TypesInputValidity.Error).Any())
                {
                    return TypesInputValidity.Error;
                }
                else if (buf.Where(x => x.Type == TypesInputValidity.Warning).Any())
                {
                    return TypesInputValidity.Warning;
                }
                else if (buf.Where(x => x.Type == TypesInputValidity.Success).Any())
                {
                    return TypesInputValidity.Success;
                }

                return IsValidated ? TypesInputValidity.Success : TypesInputValidity.Default;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
        public ControlFormItemGroup(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
        ///<param name="item">The form item.</param> 
        public ControlFormItemGroup(string id, params ControlFormItem[] item)
            : base(id)
        {
            (Items as List<ControlFormItem>).AddRange(item);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        ///<param name="item">The form item.</param> 
        public ControlFormItemGroup(params ControlFormItem[] item)
            : base(null)
        {
            (Items as List<ControlFormItem>).AddRange(item);
        }

        /// <summary>
        /// Initializes the form element.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        public override void Initialize(RenderContextFormular context)
        {
            var groupContex = new RenderContextFormularGroup(context, this);

            foreach (var item in Items)
            {
                item.Initialize(groupContex);
            }
        }

        /// <summary>
        /// Checks the input element for correctness of the data.
        /// </summary>
        /// <param name="context">The context in which the inputs are validated.</param>
        public virtual void Validate(RenderContextFormular context)
        {
            var validationResults = ValidationResults as List<ValidationResult>;

            validationResults.Clear();

            foreach (var v in Items.Where(x => x is IFormularValidation).Select(x => x as IFormularValidation))
            {
                v.Validate(context);

                validationResults.AddRange(v.ValidationResults);
            }
        }
    }
}
