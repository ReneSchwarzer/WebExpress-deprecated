using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.Uri;
using WebExpress.WebPage;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.UI.WebControl
{
    public class ControlFormular : Control, IControlFormular
    {
        /// <summary>
        /// Liefert oder setzt das Layout
        /// </summary>
        public virtual TypeLayoutFormular Layout { get; set; } = TypeLayoutFormular.Vertical;

        /// <summary>
        /// Event zum Validieren der Eingabewerte
        /// </summary>
        public event EventHandler<ValidationEventArgs> Validation;

        /// <summary>
        /// Event nach Abschluss der Validation
        /// </summary>
        public event EventHandler<ValidationResultEventArgs> Validated;

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
        /// Liefert oder setzt die Weiterleitungs-Uri
        /// </summary>
        public IUri RedirectUri { get; set; }

        /// <summary>
        /// Liefert oder setzt die Abbruchs-Uri
        /// </summary>
        public IUri BackUri { get => CancelButton.Uri; set => CancelButton.Uri = value; }

        /// <summary>
        /// Liefert oder setzt die Submit-Schaltfläche
        /// </summary>
        public ControlFormularItemButton SubmitButton { get; } = new ControlFormularItemButton();

        /// <summary>
        /// Liefert oder setzt die Abbrechen-Schaltfläche
        /// </summary>
        public ControlButtonLink CancelButton { get; } = new ControlButtonLink();

        /// <summary>
        /// Speichern und weiter Schaltfläche anzeigen
        /// </summary>
        public bool EnableCancelButton { get; set; } = true;

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
        public ControlFormular(string id = null)
            : base(id)
        {
            Name = ID != null ? ID.StartsWith("formular") ? ID : $"formular_{ ID }" : "formular";

            SubmitButton.Name = "submit_" + ID?.ToLower();
            SubmitButton.Icon = new PropertyIcon(TypeIcon.Save);
            SubmitButton.Color = new PropertyColorButton(TypeColorButton.Success);
            SubmitButton.Type = "submit";
            SubmitButton.Value = "1";
            SubmitButton.Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.None);

            CancelButton.Icon = new PropertyIcon(TypeIcon.Times);
            CancelButton.BackgroundColor = new PropertyColorButton(TypeColorButton.Secondary);
            CancelButton.TextColor = new PropertyColorText(TypeColorText.White);
            CancelButton.HorizontalAlignment = TypeHorizontalAlignment.Right;
            CancelButton.Uri = BackUri != null && !BackUri.Empty ? BackUri : Uri;

            SubmitButton.Click += OnSubmitButtonClick;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="items">Die Steuerelemente, welche dem Formular zugeordnet werden</param>
        public ControlFormular(string id, params ControlFormularItem[] items)
            : this(id)
        {
            if (items != null)
            {
                (Items as List<ControlFormularItem>).AddRange(items);
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="items">Die Steuerelemente, welche dem Formular zugeordnet werden</param>
        public ControlFormular(params ControlFormularItem[] items)
            : this(null, items)
        {
        }

        /// <summary>
        /// Wird ausgelöst, wenn auf die Submirschaltfläche geklickt wurde
        /// </summary>
        /// <param name="sender">Der Auslöser</param>
        /// <param name="args">Die Eventargumente</param>
        private void OnSubmitButtonClick(object sender, FormularEventArgs args)
        {
            Validate(args.Context);

            if (Valid)
            {
                OnProcess(args.Context);

                if (!string.IsNullOrWhiteSpace(RedirectUri?.ToString()))
                {
                    args.Context.Page.Redirecting(RedirectUri);
                }
            }
        }

        /// <summary>
        /// Initialisiert das Formular
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public virtual void Initialize(RenderContextFormular context)
        {
            var renderContext = new RenderContextFormular(context, this);

            if (string.IsNullOrWhiteSpace(SubmitButton.Text))
            {
                SubmitButton.Text = context.I18N("webexpress.ui", "form.submit.label");
            }
            else
            {
                SubmitButton.Text = context.I18N(SubmitButton.Text);
            }

            if (EnableCancelButton && string.IsNullOrWhiteSpace(CancelButton.Text))
            {
                CancelButton.Text = context.I18N("webexpress.ui", "form.cancel.label");
            }
            else
            {
                CancelButton.Text = context.I18N(CancelButton.Text);
            }
        }

        /// <summary>
        /// Vorverarbeitung des Formulars
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public virtual void PreProcess(RenderContextFormular context)
        {

        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var renderContext = new RenderContextFormular(context, this);
            var formName = Name != null ? Name.StartsWith("formular") ? Name : $"formular_{ Name }" : "formular";

            Initialize(renderContext);

            (Items as List<ControlFormularItem>).ForEach(x => x?.Initialize(renderContext));

            OnInitialize(renderContext);

            SubmitButton.Initialize(renderContext);

            // Prüfe ob Formular abgeschickt wurde
            if (!context.Request.HasParameter(formName))
            {
                OnFill(renderContext);
            }

            PreProcess(renderContext);

            var button = SubmitButton.Render(renderContext);
            var cancel = CancelButton.Render(renderContext);

            var html = new HtmlElementFormForm()
            {
                ID = ID,
                Class = GetClasses(),
                Style = GetStyles(),
                Role = Role,
                Name = formName.ToLower(),
                Action = Uri?.ToString(),
                Method = "post",
                Enctype = TypeEnctype.None
            };

            foreach (var v in ValidationResults)
            {
                var bgColor = new PropertyColorBackgroundAlert(TypeColorBackground.Default);

                switch (v.Type)
                {
                    case TypesInputValidity.Error:
                        bgColor = new PropertyColorBackgroundAlert(TypeColorBackground.Danger);
                        break;
                    case TypesInputValidity.Warning:
                        bgColor = new PropertyColorBackgroundAlert(TypeColorBackground.Warning);
                        break;
                    case TypesInputValidity.Success:
                        bgColor = new PropertyColorBackgroundAlert(TypeColorBackground.Default);
                        break;
                }

                html.Elements.Add(new ControlAlert()
                {
                    BackgroundColor = bgColor,
                    Text = I18N(context.Culture, v.Text),
                    Dismissible = TypeDismissibleAlert.Dismissible,
                    Fade = TypeFade.FadeShow
                }.Render(renderContext));
            }

            //html.Elements.Add(new ControlFormularItemInputHidden(formName)
            //{
            //    Value = Name

            //}.Render(renderContext));

            foreach (var item in Items.Where(x => x is ControlFormularItemInputHidden))
            {
                html.Elements.Add(item.Render(renderContext));
            }

            var group = null as ControlFormularItemGroup;

            group = Layout switch
            {
                TypeLayoutFormular.Horizontal => new ControlFormularItemGroupHorizontal(),
                TypeLayoutFormular.Mix => new ControlFormularItemGroupMix(),
                _ => new ControlFormularItemGroupVertical(),
            };
            foreach (var item in Items.Where(x => !(x is ControlFormularItemInputHidden)))
            {
                group.Items.Add(item);
            }

            html.Elements.Add(group.Render(renderContext));
            html.Elements.Add(button);

            if (EnableCancelButton)
            {
                html.Elements.Add(cancel);
            }

            return html;
        }

        /// <summary>
        /// Fügt eine Formularsteuerelement hinzu
        /// </summary>
        /// <param name="item">Das Formularelement</param>
        public void Add(params ControlFormularItem[] item)
        {
            (Items as List<ControlFormularItem>).AddRange(item);
        }

        /// <summary>
        /// Löst das Verarbeiten-Event aus
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        protected virtual void OnProcess(RenderContextFormular context)
        {
            ProcessFormular?.Invoke(this, new FormularEventArgs() { Context = context });
        }

        /// <summary>
        /// Löst das Verarbeiten-Event aus
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        protected virtual void OnProcessAndNextFormular(RenderContextFormular context)
        {
            ProcessAndNextFormular?.Invoke(this, new FormularEventArgs() { Context = context });
        }

        /// <summary>
        /// Löst das Initialisierungs-Event aus
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        protected virtual void OnInitialize(RenderContextFormular context)
        {
            InitializeFormular?.Invoke(this, new FormularEventArgs() { Context = context });
        }

        /// <summary>
        /// Löst das Datenbereitstellungs-Event aus
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        protected virtual void OnFill(RenderContextFormular context)
        {
            FillFormular?.Invoke(this, new FormularEventArgs() { Context = context });
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
        /// Löst das Validated-Event aus
        /// </summary>
        /// <param name="e">Das Eventargument</param>
        protected virtual void OnValidated(ValidationResultEventArgs e)
        {
            Validated?.Invoke(this, e);
        }

        /// <summary>
        /// Prüft das Eingabeelement auf Korrektheit der Daten
        /// </summary>
        /// <param name="context">Der Kontext, indem die Eingaben validiert werden</param>
        public virtual void Validate(RenderContextFormular context)
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

            Valid = valid;

            var validatedArgs = new ValidationResultEventArgs(Valid);
            validatedArgs.Results.AddRange(validationResults);

            OnValidated(validatedArgs);
        }

        /// <summary>
        /// Weist an, die initialen Formaulardaten erneut zu laden
        /// </summary>
        public void Reset()
        {
            OnFill(null);
        }
    }
}
