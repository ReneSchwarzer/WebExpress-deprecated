using System;
using WebExpress.Html;
using WebExpress.Internationalization;

namespace WebExpress.UI.WebControl
{
    public class ControlFormItemInputCheckbox : ControlFormItemInput
    {
        /// <summary>
        /// Returns or sets whether the checkbox should be displayed on a new line.
        /// </summary>
        public bool Inline { get; set; }

        /// <summary>
        /// Returns or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Returns or sets a search pattern that checks the content.
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlFormItemInputCheckbox(string id = null)
            : base(id)
        {
            Value = "false";
        }

        /// <summary>
        /// Initializes the form element.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        public override void Initialize(RenderContextFormular context)
        {
            var value = context.Request.GetParameter(Name)?.Value;

            Value = string.IsNullOrWhiteSpace(value) || !value.Equals("on", StringComparison.OrdinalIgnoreCase) ? "false" : "true";
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContextFormular context)
        {
            var html = new HtmlElementTextContentDiv
            (
                new HtmlElementFieldLabel
                (
                    new HtmlElementFieldInput()
                    {
                        Name = Name,
                        Pattern = Pattern,
                        Type = "checkbox",
                        Disabled = Disabled,
                        //Role = Role,
                        Checked = Value.Equals("true")
                    },
                    new HtmlText(string.IsNullOrWhiteSpace(Description) ? string.Empty : "&nbsp;" + context.I18N(Description))
                )
                {
                }
            )
            {
                Class = Css.Concatenate("checkbox", GetClasses()),
                Style = GetStyles(),
            };

            return html;
        }

        /// <summary>
        /// Checks the input element for correctness of the data.
        /// </summary>
        /// <param name="context">The context in which the inputs are validated.</param>
        public override void Validate(RenderContextFormular context)
        {
            base.Validate(context);
        }
    }
}
