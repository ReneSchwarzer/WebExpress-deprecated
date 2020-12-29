using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.Uri;

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
        public event EventHandler InitializeFormular;

        /// <summary>
        /// Event wird ausgelöst, wenn die Daten des Formulars ermittelt werden müssen
        /// </summary>
        public event EventHandler FillFormular;

        /// <summary>
        /// Event wird ausgelöst, wenn das Formular verarbeitet werden soll
        /// </summary>
        public event EventHandler ProcessFormular;

        /// <summary>
        /// Event wird ausgelöst, wenn das Formular verarbeitet und die nächsten Daten geladen werden sollen
        /// </summary>
        public event EventHandler ProcessAndNextFormular;

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
        public ControlFormularItemButton SubmitButton { get; } = new ControlFormularItemButton();

        /// <summary>
        /// Liefert oder setzt den Gültigkeitsbereich der Formulardaten
        /// </summary>
        public ParameterScope Scope { get; set; }

        /// <summary>
        /// Liefert oder setzt die Formulareinträge
        /// </summary>
        public ICollection<ControlFormularItem> Items { get; } = new List<ControlFormularItem>();

        /// <summary>
        /// Bestimmt ob die Eingabe gültig sind
        /// </summary>
        public bool Valid { get; private set; }

        /// <summary>
        /// Liefert die Validierungsergebnisse
        /// </summary>
        public ICollection<ValidationResult> ValidationResults { get; } = new List<ValidationResult>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularInline(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="items">Die Steuerelemente, welche dem Formular zugeordnet werden</param>
        public ControlFormularInline(string id, params ControlFormularItem[] items)
            : base(id)
        {
            (Items as List<ControlFormularItem>).AddRange(items);

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="items">Die Steuerelemente, welche dem Formular zugeordnet werden</param>
        public ControlFormularInline(params ControlFormularItem[] items)
            : this(null, items)
        {
        }

        /// <summary>
        /// Initialisiert das Formular
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public virtual void Initialize(RenderContext context)
        {
            var renderContext = new RenderContextFormular(context, this);

            Scope = ParameterScope.Local;
            Name = "Form";

            SubmitButton.Name = "submit_" + ID?.ToLower();
            SubmitButton.Icon = new PropertyIcon(TypeIcon.Save);
            SubmitButton.Color = new PropertyColorButton(TypeColorButton.Success);
            SubmitButton.Type = "submit";
            SubmitButton.Value = "1";
            SubmitButton.Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.None);           
        }

        /// <summary>
        /// Vorverarbeitung des Formulars
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public virtual void PreProcess(RenderContext context)
        {
            if (string.IsNullOrWhiteSpace(SubmitButton.Text))
            {
                SubmitButton.Text = context.I18N("webexpress", "form.submit.label");
            }

            SubmitButton.Click += (s, e) =>
            {
                Validate();

                if (Valid)
                {
                    OnProcess();

                    if (!string.IsNullOrWhiteSpace(RedirectUri?.ToString()))
                    {
                        context.Page.Redirecting(RedirectUri);
                    }
                }
            };
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var renderContext = new RenderContextFormular(context, this);
            var formName = $"form_{ Name }";

            Initialize(context);
            (Items as List<ControlFormularItem>).ForEach(x => x.Initialize(renderContext));
            OnInitialize();
            SubmitButton.Initialize(renderContext);

            // Prüfe ob Formular abgeschickt wurde -> Fomular mit Daten füllen 
            if (!context.Page.HasParam(formName))
            {
                OnFill();
            }

            PreProcess(renderContext);

            var button = SubmitButton.Render(renderContext);

            var html = new HtmlElementFormForm()
            {
                ID = ID,
                Class = Css.Concatenate("form-inline", GetClasses()),
                Style = GetStyles(),
                Role = Role,
                Name = Name.ToLower() != "form" ? "form_" + Name.ToLower() : Name.ToLower(),
                Action = Uri?.ToString(),
                Method = "post",
                Enctype = TypeEnctype.None
            };

            html.Elements.Add(new ControlFormularItemInputHidden(formName)
            {
                Value = Name

            }.Render(renderContext));

            foreach (var item in Items)
            {
                var input = item as ControlFormularItemInput;

                if (input != null)
                {
                    var icon = new ControlIcon() { Icon = input?.Icon };
                    var label = new ControlFormularItemLabel(!string.IsNullOrEmpty(item.ID) ? item.ID + "_label" : string.Empty);
                    var help = new ControlFormularItemHelpText(!string.IsNullOrEmpty(item.ID) ? item.ID + "_help" : string.Empty);
                    var fieldset = new HtmlElementFormFieldset() { Class = "form-group" };

                    label.Initialize(renderContext);
                    help.Initialize(renderContext);

                    label.Text = context.I18N(input?.Label);
                    label.FormularItem = item;
                    label.Classes.Add("mr-2");
                    help.Text = context.I18N(input?.Help);
                    help.Classes.Add("ml-2");

                    if (icon.Icon != null)
                    {
                        icon.Classes.Add("mr-2 pt-1");
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
        protected virtual void OnProcess()
        {
            ProcessFormular?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Löst das Verarbeiten-Event aus
        /// </summary>
        protected virtual void OnProcessAndNextFormular()
        {
            ProcessAndNextFormular?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Löst das Laden-Event aus
        /// </summary>
        protected virtual void OnInitialize()
        {
            InitializeFormular?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Löst das Datenbereitstellungs-Event aus
        /// </summary>
        protected virtual void OnFill()
        {
            FillFormular?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Löst das Validation-Event aus
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected virtual void OnValidation(ValidationEventArgs e)
        {
            Validation?.Invoke(this, e);
        }

        /// <summary>
        /// Prüft das Eingabeelement auf Korrektheit der Daten
        /// </summary>
        public virtual void Validate()
        {
            var valid = true;
            var validationResults = ValidationResults as List<ValidationResult>;

            validationResults.Clear();

            foreach (var v in Items.Where(x => x is IFormularValidation).Select(x => x as IFormularValidation))
            {
                v.Validate();

                if (v.ValidationResult == TypesInputValidity.Error)
                {
                    valid = false;
                }

                validationResults.AddRange(v.ValidationResults);
            }

            var args = new ValidationEventArgs() { Value = null };
            OnValidation(args);

            validationResults.AddRange(args.Results);

            if (args.Results.Where(x => x.Type == TypesInputValidity.Error).Count() > 0)
            {
                valid = false;
            }

            Valid = valid;
        }

    }
}
