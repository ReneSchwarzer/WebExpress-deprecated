using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.WebMessage;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlFormInline : Control, IControlForm
    {
        /// <summary>
        /// Event to validate the input values.
        /// </summary>
        public event EventHandler<ValidationEventArgs> Validation;

        /// <summary>
        /// Event is raised when the form has been initialized.
        /// </summary>
        public event EventHandler<FormularEventArgs> InitializeFormular;

        /// <summary>
        /// Event is raised when the form's data needs to be determined.
        /// </summary>
        public event EventHandler<FormularEventArgs> FillFormular;

        /// <summary>
        /// Event is raised when the form is about to be processed.
        /// </summary>
        public event EventHandler<FormularEventArgs> ProcessFormular;

        /// <summary>
        /// Event is raised when the form is to be processed and the next data is to be loaded.
        /// </summary>
        public event EventHandler<FormularEventArgs> ProcessAndNextFormular;

        /// <summary>
        /// Returns or sets the name of the form.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Returns or sets the target uri.
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Returns or sets the redirect uri.
        /// </summary>
        public string RedirectUri { get; set; }

        /// <summary>
        /// Returns or sets the abort uri.
        /// </summary>
        public string BackUri { get; set; }

        /// <summary>
        /// Returns or sets the submit button.
        /// </summary>
        public ControlFormItemButton SubmitButton { get; private set; }

        /// <summary>
        /// Returns or sets the scope of the form data.
        /// </summary>
        public ParameterScope Scope { get; set; }

        /// <summary>
        /// Returns or sets the form items.
        /// </summary>
        public IList<ControlFormItem> Items { get; } = new List<ControlFormItem>();

        /// <summary>
        /// Returns the validation results.
        /// </summary>
        public ICollection<ValidationResult> ValidationResults { get; } = new List<ValidationResult>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
        public ControlFormInline(string id = null)
            : base(id)
        {
            Name = Id != null ? Id.StartsWith("formular") ? Id : $"formular_{Id}" : "formular";

            SubmitButton = new ControlFormItemButton("submit-" + Name?.ToLower())
            {
                Name = "submit-" + Name?.ToLower(),
                Icon = new PropertyIcon(TypeIcon.Save),
                Color = new PropertyColorButton(TypeColorButton.Success),
                Type = TypeButton.Submit,
                Value = "1",
                Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.None)
            };
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
        /// <param name="items">The controls that are associated with the form.</param>
        public ControlFormInline(string id, params ControlFormItem[] items)
            : this(id)
        {
            (Items as List<ControlFormItem>).AddRange(items);

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="items">The controls that are associated with the form.</param>
        public ControlFormInline(params ControlFormItem[] items)
            : this(null, items)
        {
        }

        /// <summary>
        /// Initializes the form.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        public virtual void Initialize(RenderContextFormular context)
        {
            if (string.IsNullOrWhiteSpace(SubmitButton.Text))
            {
                SubmitButton.Text = context.I18N("webexpress.ui", "form.submit.label");
            }
        }

        /// <summary>
        /// Vorverarbeitung des Formulars
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        public virtual void PreProcess(RenderContextFormular context)
        {

        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var renderContext = new RenderContextFormular(context, this);
            var formName = Name != null ? Name.StartsWith("formular") ? Name : $"formular_{Name}" : "formular";

            Initialize(renderContext);
            (Items as List<ControlFormItem>).ForEach(x => x.Initialize(renderContext));
            OnInitialize(renderContext);
            SubmitButton.Initialize(renderContext);

            // Prüfe ob Formular abgeschickt wurde -> Fomular mit Daten füllen 
            if (!context.Request.HasParameter("submit-" + formName))
            {
                OnFill(renderContext);
            }

            PreProcess(renderContext);

            var button = SubmitButton.Render(renderContext);

            if (context.Request.HasParameter("formular-id"))
            {
                var value = context.Request.GetParameter("formular-id")?.Value;

                if (!string.IsNullOrWhiteSpace(Id) && value == Id)
                {
                    var valid = Validate(renderContext);

                    if (valid)
                    {
                        OnProcess(renderContext);

                        if (!string.IsNullOrWhiteSpace(RedirectUri?.ToString()))
                        {
                            renderContext.Page.Redirecting(RedirectUri);
                        }
                    }
                }
            }

            var html = new HtmlElementFormForm()
            {
                Id = Id,
                Class = Css.Concatenate("form-inline", GetClasses()),
                Style = GetStyles(),
                Role = Role,
                Name = formName.ToLower(),
                Action = Uri?.ToString(),
                Method = "post",
                Enctype = TypeEnctype.None
            };

            html.Elements.Add(new ControlFormItemInputHidden() { Name = "formular-id", Value = Id }.Render(renderContext));

            foreach (var item in Items)
            {
                if (item is ControlFormItemInput input)
                {
                    var icon = new ControlIcon() { Icon = input?.Icon };
                    var label = new ControlFormItemLabel(!string.IsNullOrEmpty(item.Id) ? item.Id + "_label" : string.Empty);
                    var help = new ControlFormItemHelpText(!string.IsNullOrEmpty(item.Id) ? item.Id + "_help" : string.Empty);
                    var fieldset = new HtmlElementFormFieldset() { Class = "form-group" };

                    label.Initialize(renderContext);
                    help.Initialize(renderContext);

                    label.Text = context.I18N(input?.Label);
                    label.FormularItem = item;
                    label.Classes.Add("me-2");
                    help.Text = context.I18N(input?.Help);
                    help.Classes.Add("ms-2");

                    if (icon.Icon != null)
                    {
                        icon.Classes.Add("me-2 pt-1");
                        fieldset.Elements.Add(new HtmlElementTextSemanticsSpan(icon.Render(renderContext), label.Render(renderContext))
                        {
                            Style = "display: flex;"
                        });
                    }
                    else
                    {
                        fieldset.Elements.Add(label.Render(renderContext));
                    }

                    fieldset.Elements.Add(item.Render(renderContext));

                    if (input != null)
                    {
                        fieldset.Elements.Add(help.Render(renderContext));
                    }

                    html.Elements.Add(fieldset);
                }
                else
                {
                    html.Elements.Add(item.Render(context));
                }
            }

            html.Elements.Add(button);

            return html;
        }

        /// <summary>
        /// Adds a form element.
        /// </summary>
        /// <param name="item">The form item.</param>
        public void Add(params ControlFormItem[] item)
        {
            (Items as List<ControlFormItem>).AddRange(item);
        }

        /// <summary>
        /// Raises the process event.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        protected virtual void OnProcess(RenderContextFormular context)
        {
            ProcessFormular?.Invoke(this, new FormularEventArgs() { Context = context });
        }

        /// <summary>
        /// Raises the process event.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        protected virtual void OnProcessAndNextFormular(RenderContextFormular context)
        {
            ProcessAndNextFormular?.Invoke(this, new FormularEventArgs() { Context = context });
        }

        /// <summary>
        /// Raises the store event.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        protected virtual void OnInitialize(RenderContextFormular context)
        {
            InitializeFormular?.Invoke(this, new FormularEventArgs() { Context = context });
        }

        /// <summary>
        /// Raises the data delivery event.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        protected virtual void OnFill(RenderContextFormular context)
        {
            FillFormular?.Invoke(this, new FormularEventArgs() { Context = context });
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
        /// <returns>True if all form items are valid, false otherwise.</returns>
        public virtual bool Validate(RenderContextFormular context)
        {
            var valid = true;
            var validationResults = ValidationResults as List<ValidationResult>;

            validationResults.Clear();

            foreach (var v in Items.Where(x => x is IFormularValidation).Select(x => x as IFormularValidation))
            {
                v.Validate(context);

                if (v.ValidationResult == TypesInputValidity.Error)
                {
                    valid = false;
                }

                validationResults.AddRange(v.ValidationResults);
            }

            var args = new ValidationEventArgs() { Value = null, Context = context };
            OnValidation(args);

            validationResults.AddRange(args.Results);

            if (args.Results.Where(x => x.Type == TypesInputValidity.Error).Any())
            {
                valid = false;
            }

            return valid;
        }

    }
}
