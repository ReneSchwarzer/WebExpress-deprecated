using System.Collections.Generic;

namespace WebExpress.UI.WebControl
{
    public interface IFormularValidation
    {
        /// <summary>
        /// Returns or sets whether the form element has been validated.
        /// </summary>
        //bool IsValidated { get; }

        /// <summary>
        /// Determines whether the inputs are valid.
        /// </summary>
        ICollection<ValidationResult> ValidationResults { get; }

        /// <summary>
        /// Returns the most serious validation result.
        /// </summary>
        TypesInputValidity ValidationResult { get; }

        /// <summary>
        /// Checks the input element for correctness of the data.
        /// </summary>
        /// <param name="context">The context in which the inputs are validated.</param>
        void Validate(RenderContextFormular context);
    }
}
