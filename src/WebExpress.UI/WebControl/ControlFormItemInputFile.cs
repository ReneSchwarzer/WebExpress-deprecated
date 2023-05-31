using System.Collections.Generic;
using WebExpress.Html;

namespace WebExpress.UI.WebControl
{
    public class ControlFormItemInputFile : ControlFormItemInput
    {
        /// <summary>
        /// Returns or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Returns or sets a placeholder text.
        /// </summary>
        public string Placeholder { get; set; }

        /// <summary>
        /// Returns or sets whether inputs are enforced.
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Returns or sets the accepted files.
        /// </summary>
        public ICollection<string> AcceptFile { get; set; } = new List<string>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
        public ControlFormItemInputFile(string id = null)
            : base(!string.IsNullOrWhiteSpace(id) ? id : "file")
        {
            Name = Id;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.None);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
        /// <param name="name">The name.</param>
        public ControlFormItemInputFile(string id, string name)
            : base(!string.IsNullOrWhiteSpace(id) ? id : "file")
        {
            Name = name;
        }

        /// <summary>
        /// Initializes the form element.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        public override void Initialize(RenderContextFormular context)
        {
            Value = context?.Request.GetParameter(Name)?.Value;
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContextFormular context)
        {
            switch (ValidationResult)
            {
                case TypesInputValidity.Warning:
                    Classes.Add("input-warning");
                    break;
                case TypesInputValidity.Error:
                    Classes.Add("input-error");
                    break;
            }

            var html = new HtmlElementFieldInput()
            {
                Id = Id,
                Value = Value,
                Name = Name,
                Type = "file",
                Class = Css.Concatenate("form-control-file", GetClasses()),
                Style = GetStyles(),
                Role = Role,
                Placeholder = Placeholder
            };

            html.AddUserAttribute("accept", string.Join(",", AcceptFile));

            return html;
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

            if (Required && string.IsNullOrWhiteSpace(base.Value))
            {
                ValidationResults.Add(new ValidationResult(TypesInputValidity.Error, "webexpress.ui:form.inputfile.validation.required"));

                return;
            }

            base.Validate(context);
        }
    }
}
