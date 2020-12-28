using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.Uri;

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
        /// Event wird ausgelöst, wenn das Formular geladen werden soll
        /// </summary>
        public event EventHandler InitFormular;

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
        public string Name { get; set; } = "Form";

        /// <summary>
        /// Liefert oder setzt die Ziel-Uri
        /// </summary>
        public IUri Uri { get; set; }

        /// <summary>
        /// Liefert oder setzt die Weiterleitungs-Url
        /// </summary>
        public IUri RedirectUrl { get; set; }

        /// <summary>
        /// Liefert oder setzt die Abbruchs-Url
        /// </summary>
        public IUri BackUrl { get; set; }

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
        /// Liefert oder setzt die Submit-Schaltfläche
        /// </summary>
        public ControlFormularItemButton SubmitAndNextButton { get; } = new ControlFormularItemButton();

        /// <summary>
        /// Speichern und weiter Schaltfläche anzeigen
        /// </summary>
        public bool EnableSubmitAndNextButton { get; set; } = false;

        /// <summary>
        /// Liefert oder setzt den Gültigkeitsbereich der Formulardaten
        /// </summary>
        public ParameterScope Scope { get; set; } = ParameterScope.Local;

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
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="items">Die Steuerelemente, welche dem Formular zugeordnet werden</param>
        public ControlFormular(string id, params ControlFormularItem[] items)
            : base(id)
        {
            (Items as List<ControlFormularItem>).AddRange(items);

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
        /// Initialisiert das Formular
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public virtual void Initialize(RenderContext context)
        { 
            SubmitButton.Name = "submit_" + ID?.ToLower();
            SubmitButton.Icon = new PropertyIcon(TypeIcon.Save);
            SubmitButton.Color = new PropertyColorButton(TypeColorButton.Success);
            SubmitButton.Type = "submit";
            SubmitButton.Value = "1";
            SubmitButton.Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.None);

            SubmitAndNextButton.Name = "next_" + ID?.ToLower();
            SubmitAndNextButton.Icon = new PropertyIcon(TypeIcon.Forward);
            SubmitAndNextButton.Color = new PropertyColorButton(TypeColorButton.Success);
            SubmitAndNextButton.Type = "submit";
            SubmitAndNextButton.Value = "1";
            SubmitAndNextButton.Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.None);

            CancelButton.Icon = new PropertyIcon(TypeIcon.Times);
            CancelButton.BackgroundColor = new PropertyColorButton(TypeColorButton.Secondary);
            CancelButton.TextColor = new PropertyColorText(TypeColorText.White);
            CancelButton.HorizontalAlignment = TypeHorizontalAlignment.Right;
            CancelButton.Uri = Uri;
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Initialize(context);

            var renderContext = new RenderContextFormular(context, this);
            var items = Items as List<ControlFormularItem>;
            items.ForEach(x => x.Initialize(renderContext));

            CancelButton.Uri = RedirectUrl;
            
            if (string.IsNullOrWhiteSpace(SubmitButton.Text))
            { 
                SubmitButton.Text = context.I18N("webexpress", "form.submit.label");
            }
            
            if (string.IsNullOrWhiteSpace(SubmitAndNextButton.Text))
            { 
                SubmitAndNextButton.Text = context.I18N("webexpress", "form.next.label");
            }

            if (string.IsNullOrWhiteSpace(CancelButton.Text))
            {
                CancelButton.Text = context.I18N("webexpress", "form.cancel.label");
            }

            SubmitButton.Click += (s, e) =>
            {
                Validate();

                if (Valid)
                {
                    OnProcess();

                    if (!string.IsNullOrWhiteSpace(RedirectUrl?.ToString()))
                    {
                        context.Page.Redirecting(RedirectUrl);
                    }
                }
            };

            SubmitAndNextButton.Click += (s, e) =>
            {
                Validate();

                if (Valid)
                {
                    OnProcessAndNextFormular();
                }
            };

            SubmitButton.Initialize(renderContext);

            if (EnableSubmitAndNextButton)
            {
                SubmitAndNextButton?.Initialize(renderContext);
            }

            // Prüfe ob Formular abgeschickt wurde
            if (context.Page.HasParam(SubmitButton.Name))
            {
                OnInit();
            }

            var button = SubmitButton.Render(renderContext);
            var next = SubmitAndNextButton.Render(renderContext);
            var cancel = CancelButton.Render(renderContext);

            var html = new HtmlElementFormForm()
            {
                ID = ID,
                Class = GetClasses(),
                Style = GetStyles(),
                Role = Role,
                Name = Name.ToLower() != "form" ? "form_" + Name.ToLower() : Name.ToLower(),
                Action = Uri?.ToString(),
                Method = "post",
                Enctype = TypeEnctype.None
            };

            foreach (var v in ValidationResults)
            {
                var bgColor = (PropertyColorBackground)new PropertyColorBackgroundAlert(TypeColorBackground.Default);

                switch (v.Type)
                {
                    case TypesInputValidity.Error:
                        bgColor = new PropertyColorBackground(TypeColorBackground.Danger);
                        break;
                    case TypesInputValidity.Warning:
                        bgColor = new PropertyColorBackground(TypeColorBackground.Warning);
                        break;
                    case TypesInputValidity.Success:
                        bgColor = new PropertyColorBackground(TypeColorBackground.Default);
                        break;
                }

                html.Elements.Add(new ControlAlert()
                {
                    BackgroundColor = (PropertyColorBackgroundAlert)bgColor,
                    Text = v.Text,
                    Dismissible = TypeDismissibleAlert.Dismissible,
                    Fade = TypeFade.FadeShow
                }.Render(renderContext));
            }

            var group = null as ControlFormularItemGroup;

            switch (Layout)
            {
                case TypeLayoutFormular.Horizontal:
                    group = new ControlFormularItemGroupHorizontal();
                    break;
                case TypeLayoutFormular.Mix:
                    group = new ControlFormularItemGroupMix();
                    break;
                default:
                    group = new ControlFormularItemGroupVertical();
                    break;
            }

            foreach (var item in Items)
            {
                group.Items.Add(item);
            }

            html.Elements.Add(group.Render(renderContext));
            html.Elements.Add(button);

            if (EnableSubmitAndNextButton)
            {
                html.Elements.Add(next);
            }

            if (EnableCancelButton)
            {
                html.Elements.Add(cancel);
            }

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
        protected virtual void OnInit()
        {
            InitFormular?.Invoke(this, new EventArgs());
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
