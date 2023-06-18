using System;
using System.Linq;
using WebExpress.WebHtml;
using WebExpress.WebComponent;
using WebExpress.WebUri;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.UI.WebControl
{
    public class ControlFormItemInputTextBox : ControlFormItemInput
    {
        /// <summary>
        /// Determines whether the control is automatically initialized.
        /// </summary>
        public bool AutoInitialize { get; set; }

        /// <summary>
        /// Determines whether it is a multi-line text box.
        /// </summary>
        public TypesEditTextFormat Format { get; set; }

        /// <summary>
        /// Returns or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Returns or sets a placeholder text.
        /// </summary>
        public string Placeholder { get; set; }

        /// <summary>
        /// Returns or sets the minimum length.
        /// </summary>
        public int? MinLength { get; set; }

        /// <summary>
        /// Returns or sets the maximum length.
        /// </summary>
        public int? MaxLength { get; set; }

        /// <summary>
        /// Returns or sets whether inputs are enforced.
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Returns or sets a search pattern that checks the content.
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// Returns or sets the height of the text fiel (for Multiline and WYSIWYG)
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlFormItemInputTextBox(string id = null)
            : base(id)
        {
            Name = Id;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.None);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="name">The name of the text box.</param>
        public ControlFormItemInputTextBox(string id, string name)
            : base(id)
        {
            Name = name;
        }

        /// <summary>
        /// Initializes the form element.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        public override void Initialize(RenderContextFormular context)
        {
            base.Initialize(context);

            Rows = 8;
            AutoInitialize = true;

            Value = context?.Request.GetParameter(Name)?.Value;

            if (Format == TypesEditTextFormat.Wysiwyg)
            {
                var module = ComponentManager.ModuleManager.GetModule(context.ApplicationContext, typeof(Module));
                if (module != null)
                {
                    context.VisualTree.CssLinks.Add(UriResource.Combine(module.ContextPath, "/assets/css/summernote-bs5.min.css"));
                    context.VisualTree.HeaderScriptLinks.Add(UriResource.Combine(module.ContextPath, "/assets/js/summernote-bs5.min.js"));
                }
            }
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContextFormular context)
        {
            var id = Id ?? Guid.NewGuid().ToString();

            Classes.Add("form-control");

            if (Disabled)
            {
                Classes.Add("disabled");
            }

            switch (ValidationResult)
            {
                case TypesInputValidity.Warning:
                    Classes.Add("input-warning");
                    break;
                case TypesInputValidity.Error:
                    Classes.Add("input-error");
                    break;
            }

            if (AutoInitialize && Format == TypesEditTextFormat.Wysiwyg && !string.IsNullOrWhiteSpace(Id))
            {
                var initializeCode = $"$(document).ready(function() {{ $('#{id}').summernote({{ tabsize: 2, height: '{Rows}rem', lang: 'de-DE' }}); }});";

                context.AddScript(id, initializeCode);

                AutoInitialize = false;
            }

            return Format switch
            {
                TypesEditTextFormat.Multiline => new HtmlElementFormTextarea()
                {
                    Id = Id,
                    Value = Value,
                    Name = Name,
                    Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                    Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                    Role = Role,
                    Placeholder = I18N(context.Culture, Placeholder),
                    Rows = Rows.ToString()
                },
                TypesEditTextFormat.Wysiwyg => new HtmlElementFormTextarea()
                {
                    Id = id,
                    Value = Value,
                    Name = Name,
                    Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                    Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                    Role = Role,
                    Placeholder = I18N(context.Culture, Placeholder),
                    Rows = Rows.ToString()
                },
                _ => new HtmlElementFieldInput()
                {
                    Id = Id,
                    Value = Value,
                    Name = Name,
                    MinLength = MinLength?.ToString(),
                    MaxLength = MaxLength?.ToString(),
                    Required = Required,
                    Pattern = Pattern,
                    Type = "text",
                    Disabled = Disabled,
                    Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                    Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                    Role = Role,
                    Placeholder = I18N(context.Culture, Placeholder)
                },
            };
        }

        /// <summary>
        /// Checks the input element for correctness of the data.
        /// </summary>
        /// <param name="context">The context in which the inputs are validated.</param>
        public override void Validate(RenderContextFormular context)
        {
            base.Validate(context);

            if (Disabled)
            {
                return;
            }

            if (Required && string.IsNullOrWhiteSpace(base.Value))
            {
                ValidationResults.Add(new ValidationResult(TypesInputValidity.Error, "webexpress.ui:form.inputtextbox.validation.required"));

                return;
            }

            if (!string.IsNullOrWhiteSpace(MinLength?.ToString()) && Convert.ToInt32(MinLength) > base.Value.Length)
            {
                ValidationResults.Add(new ValidationResult(TypesInputValidity.Error, string.Format(I18N(context.Culture, "webexpress.ui:form.inputtextbox.validation.min"), MinLength)));
            }

            if (!string.IsNullOrWhiteSpace(MaxLength?.ToString()) && Convert.ToInt32(MaxLength) < base.Value.Length)
            {
                ValidationResults.Add(new ValidationResult(TypesInputValidity.Error, string.Format(I18N(context.Culture, "webexpress.ui:form.inputtextbox.validation.max"), MaxLength)));
            }
        }
    }
}
