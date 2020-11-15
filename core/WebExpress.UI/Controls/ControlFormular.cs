using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebExpress.Html;
using WebExpress.Messages;

namespace WebExpress.UI.Controls
{
    public class ControlFormular : Control
    {
        /// <summary>
        /// Event zum Validieren der Eingabewerte
        /// </summary>
        public event EventHandler<ValidationEventArgs> Validation;

        /// <summary>
        /// Liefert oder setzt das Layout
        /// </summary>
        public virtual TypeLayoutFormular Layout
        {
            get => (TypeLayoutFormular)GetProperty(TypeLayoutFormular.Default);
            set => SetProperty(value, () => value.ToClass());
        }

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
        public string Name { get; set; }

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
        public ControlFormularItemButton SubmitButton { get; protected set; }

        /// <summary>
        /// Liefert oder setzt die Abbrechen-Schaltfläche
        /// </summary>
        public ControlButtonLink CancelButton { get; set; }

        /// <summary>
        /// Speichern und weiter Schaltfläche anzeigen
        /// </summary>
        public bool EnableCancelButton { get; set; }

        /// <summary>
        /// Liefert oder setzt die Submit-Schaltfläche
        /// </summary>
        public ControlFormularItemButton SubmitAndNextButton { get; protected set; }

        /// <summary>
        /// Speichern und weiter Schaltfläche anzeigen
        /// </summary>
        public bool EnableSubmitAndNextButton { get; set; }

        /// <summary>
        /// Liefert oder setzt den Gültigkeitsbereich der Formulardaten
        /// </summary>
        public ParameterScope Scope { get; set; }

        /// <summary>
        /// Liefert oder setzt die Formulareinträge
        /// </summary>
        public List<ControlFormularItem> Items { get; private set; } = new List<ControlFormularItem>();

        /// <summary>
        /// Bestimmt ob die Eingabe gültig sind
        /// </summary>
        public bool Valid { get; private set; }

        /// <summary>
        /// Bestimmt ob die Eingabe gültig sind
        /// </summary>
        public List<ValidationResult> ValidationResults { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormular(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="items">Die Steuerelemente, welche dem Formular zugeordnet werden</param>
        public ControlFormular(string id, params ControlFormularItem[] items)
            : base(id)
        {
            Items.AddRange(items);

            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="items">Die Steuerelemente, welche dem Formular zugeordnet werden</param>
        public ControlFormular(params ControlFormularItem[] items)
            : base(null as string)
        {
            Items.AddRange(items);

            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            ValidationResults = new List<ValidationResult>();

            EnableCancelButton = true;
            Scope = ParameterScope.Local;
            Name = "Form";
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var renderContext = new RenderContextFormular(context, this);
            SubmitButton?.Initialize(renderContext);

            if (EnableSubmitAndNextButton)
            {
                SubmitAndNextButton?.Initialize(renderContext);
            }

            Items.ForEach(x => x.Initialize(renderContext));

            if (CancelButton != null)
            {
                CancelButton.Uri = RedirectUrl;
            }

            SubmitButton = new ControlFormularItemButton()
            {
                Name = "submit_" + Name.ToLower(),
                Text = context.I18N(Assembly.GetExecutingAssembly(), "webexpress.ui.form.submit.label"),
                Icon = new PropertyIcon(TypeIcon.Save),
                Color = LayoutSchema.SubmitButtonBackground,
                Type = "submit",
                Value = "1",
                Margin = new PropertySpacingMargin(Layout == TypeLayoutFormular.Inline ? PropertySpacing.Space.Two : PropertySpacing.Space.None, PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.None)
            };

            SubmitAndNextButton = new ControlFormularItemButton()
            {
                Name = "next_" + Name.ToLower(),
                Text = context.I18N(Assembly.GetExecutingAssembly(), "webexpress.ui.form.next.label"),
                Icon = new PropertyIcon(TypeIcon.Forward),
                Color = LayoutSchema.NextButtonBackground,
                Type = "submit",
                Value = "1",
                Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.None)
            };

            CancelButton = new ControlButtonLink()
            {
                Text = context.I18N(Assembly.GetExecutingAssembly(), "webexpress.ui.form.cancel.label"),
                Icon = new PropertyIcon(TypeIcon.Times),
                BackgroundColor = LayoutSchema.CancelButtonBackground,
                TextColor = new PropertyColorText(TypeColorText.White),
                HorizontalAlignment = TypeHorizontalAlignment.Right,
                Uri = Uri
            };

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

            // Prüfe ob Formular abgeschickt wurde
            if (string.IsNullOrWhiteSpace(context.Page.GetParam(SubmitButton.Name)))
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
                Method = "post"
            };

            foreach (var v in ValidationResults)
            {
                var bgColor = (PropertyColorBackground)new PropertyColorBackgroundAlert(TypeColorBackground.Default);

                switch (v.Type)
                {
                    case TypesInputValidity.Error:
                        bgColor = LayoutSchema.ValidationErrorBackground;
                        break;
                    case TypesInputValidity.Warning:
                        bgColor = LayoutSchema.ValidationWarningBackground;
                        break;
                    case TypesInputValidity.Success:
                        bgColor = LayoutSchema.ValidationSuccessBackground;
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

            foreach (var v in Items)
            {
                html.Elements.Add(new ControlFormularItemLabelGroup(v)
                {
                    //Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.None)
                }.Render(renderContext));
            }

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
            Items.AddRange(item);
        }

        /// <summary>
        /// Löst da Verarbeiten-Event aus
        /// </summary>
        protected virtual void OnProcess()
        {
            ProcessFormular?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Löst da Verarbeiten-Event aus
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

            ValidationResults.Clear();

            foreach (var v in Items.Where(x => x is ControlFormularItemInput).Select(x => x as ControlFormularItemInput))
            {
                v.Validate();

                if (v.ValidationResult == TypesInputValidity.Error)
                {
                    valid = false;
                }

                ValidationResults.AddRange(v.ValidationResults);
            }

            var args = new ValidationEventArgs() { Value = null };
            OnValidation(args);

            ValidationResults.AddRange(args.Results);

            if (args.Results.Where(x => x.Type == TypesInputValidity.Error).Count() > 0)
            {
                valid = false;
            }

            Valid = valid;
        }

    }
}
