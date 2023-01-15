using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.Uri;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlFormularInline : Control, IControlFormular
    {
        /// <summary>
        /// Event zum Validieren der Eingabewerte
        /// </summary>
        public event EventHandler<ValidationEventArgs> Validation;

        /// <summary>
        /// Event wird ausgelöst, wenn das Formular initialisiert wurde
        /// </summary>
        public event EventHandler<FormularEventArgs> InitializeFormular;

        /// <summary>
        /// Event wird ausgelöst, wenn die Daten des Formulars ermittelt werden müssen
        /// </summary>
        public event EventHandler<FormularEventArgs> FillFormular;

        /// <summary>
        /// Event wird ausgelöst, wenn das Formular verarbeitet werden soll
        /// </summary>
        public event EventHandler<FormularEventArgs> ProcessFormular;

        /// <summary>
        /// Event wird ausgelöst, wenn das Formular verarbeitet und die nächsten Daten geladen werden sollen
        /// </summary>
        public event EventHandler<FormularEventArgs> ProcessAndNextFormular;

        /// <summary>
        /// Liefert oder setzt den Formularnamen
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Liefert oder setzt die Ziel-Uri
        /// </summary>
        public IUri Uri { get; set; }

        /// <summary>
        /// Liefert oder setzt die Weiterleitungs-Url
        /// </summary>
        public IUri RedirectUri { get; set; }

        /// <summary>
        /// Liefert oder setzt die Abbruchs-Url
        /// </summary>
        public IUri BackUri { get; set; }

        /// <summary>
        /// Liefert oder setzt die Submit-Schaltfläche
        /// </summary>
        public ControlFormularItemButton SubmitButton { get; private set; }

        /// <summary>
        /// Liefert oder setzt den Gültigkeitsbereich der Formulardaten
        /// </summary>
        public ParameterScope Scope { get; set; }

        /// <summary>
        /// Liefert oder setzt die Formulareinträge
        /// </summary>
        public IList<ControlFormularItem> Items { get; } = new List<ControlFormularItem>();

        /// <summary>
        /// Liefert die Validierungsergebnisse
        /// </summary>
        public ICollection<ValidationResult> ValidationResults { get; } = new List<ValidationResult>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularInline(string id = null)
            : base(id)
        {
            Name = Id != null ? Id.StartsWith("formular") ? Id : $"formular_{Id}" : "formular";

            SubmitButton = new ControlFormularItemButton("submit-" + Name?.ToLower())
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
        /// <param name="id">Die ID</param>
        /// <param name="items">Die Steuerelemente, welche dem Formular zugeordnet werden</param>
        public ControlFormularInline(string id, params ControlFormularItem[] items)
            : this(id)
        {
            (Items as List<ControlFormularItem>).AddRange(items);

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="items">Die Steuerelemente, welche dem Formular zugeordnet werden</param>
        public ControlFormularInline(params ControlFormularItem[] items)
            : this(null, items)
        {
        }

        /// <summary>
        /// Initialisiert das Formular
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
            (Items as List<ControlFormularItem>).ForEach(x => x.Initialize(renderContext));
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
                ID = Id,
                Class = Css.Concatenate("form-inline", GetClasses()),
                Style = GetStyles(),
                Role = Role,
                Name = formName.ToLower(),
                Action = Uri?.ToString(),
                Method = "post",
                Enctype = TypeEnctype.None
            };

            html.Elements.Add(new ControlFormularItemInputHidden() { Name = "formular-id", Value = Id }.Render(renderContext));

            foreach (var item in Items)
            {
                if (item is ControlFormularItemInput input)
                {
                    var icon = new ControlIcon() { Icon = input?.Icon };
                    var label = new ControlFormularItemLabel(!string.IsNullOrEmpty(item.Id) ? item.Id + "_label" : string.Empty);
                    var help = new ControlFormularItemHelpText(!string.IsNullOrEmpty(item.Id) ? item.Id + "_help" : string.Empty);
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
        /// Fügt eine Textbox binzu
        /// </summary>
        /// <param name="item">Das Formularelement</param>
        public void Add(params ControlFormularItem[] item)
        {
            (Items as List<ControlFormularItem>).AddRange(item);
        }

        /// <summary>
        /// Löst das Verarbeiten-Event aus
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        protected virtual void OnProcess(RenderContextFormular context)
        {
            ProcessFormular?.Invoke(this, new FormularEventArgs() { Context = context });
        }

        /// <summary>
        /// Löst das Verarbeiten-Event aus
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        protected virtual void OnProcessAndNextFormular(RenderContextFormular context)
        {
            ProcessAndNextFormular?.Invoke(this, new FormularEventArgs() { Context = context });
        }

        /// <summary>
        /// Löst das Laden-Event aus
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        protected virtual void OnInitialize(RenderContextFormular context)
        {
            InitializeFormular?.Invoke(this, new FormularEventArgs() { Context = context });
        }

        /// <summary>
        /// Löst das Datenbereitstellungs-Event aus
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        protected virtual void OnFill(RenderContextFormular context)
        {
            FillFormular?.Invoke(this, new FormularEventArgs() { Context = context });
        }

        /// <summary>
        /// Löst das Validation-Event aus
        /// </summary>
        /// <param name="e">The event argument.</param>
        protected virtual void OnValidation(ValidationEventArgs e)
        {
            Validation?.Invoke(this, e);
        }

        /// <summary>
        /// Prüft das Eingabeelement auf Korrektheit der Daten
        /// </summary>
        /// <param name="context">Der Kontext, indem die Eingaben validiert werden</param>
        /// <returns>True wenn alle Formulareinträhe gültig sind, false sonst</returns>
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
