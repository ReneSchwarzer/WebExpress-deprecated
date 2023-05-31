using System;
using System.Collections.Generic;
using System.Linq;

namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Identifies a control that is to be filled in by the user.
    /// </summary>
    public abstract class ControlFormItemInput : ControlFormItem, IControlFormLabel, IFormularValidation
    {
        /// <summary>
        /// Event to validate the input values.
        /// </summary>
        public event EventHandler<ValidationEventArgs> Validation;

        /// <summary>
        /// Returns or sets the icon.
        /// </summary>
        public PropertyIcon Icon { get; set; }

        /// <summary>
        /// Returns or sets the label.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Returns or sets an optional help text.
        /// </summary>
        public string Help { get; set; }

        /// <summary>
        /// Returns or sets whether the input element is disabled.
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// Returns or sets the elements that are displayed in front of the control.
        /// </summary>
        public List<Control> Prepend { get; private set; }

        /// <summary>
        /// Returns or sets the elements that are displayed after the control.
        /// </summary>
        public List<Control> Append { get; private set; }

        /// <summary>
        /// Returns or sets whether the form element has been validated.
        /// </summary>
        private bool IsValidated { get; set; }

        /// <summary>
        /// Returns or sets an object that is linked to the control.
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// Determines whether the inputs are valid.
        /// </summary>
        public virtual ICollection<ValidationResult> ValidationResults { get; } = new List<ValidationResult>();

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
        /// Returns or sets the value.
        /// </summary>
        public virtual string Value { get; set; } = string.Empty;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
        public ControlFormItemInput(string id)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private void Init()
        {
            Prepend = new List<Control>();
            Append = new List<Control>();
            IsValidated = false;
        }

        /// <summary>
        /// Initializes the form.emement
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        public override void Initialize(RenderContextFormular context)
        {
        }

        /// <summary>
        /// Raises the validation event.
        /// </summary>
        /// <param name="e">The event argument.</param>
        protected virtual void OnValidation(ValidationEventArgs e)
        {
            Validation?.Invoke(this, e);
        }

        /// <summary>
        /// Checks the input element for correctness of the data.
        /// </summary>
        /// <param name="context">The context in which the inputs are validated.</param>
        public virtual void Validate(RenderContextFormular context)
        {
            IsValidated = true;

            if (ValidationResults is List<ValidationResult> validationResults)
            {
                validationResults.Clear();

                if (!Disabled)
                {
                    var args = new ValidationEventArgs() { Value = Value, Context = context };
                    OnValidation(args);

                    validationResults.AddRange(args.Results);
                }
            }
        }
    }
}
