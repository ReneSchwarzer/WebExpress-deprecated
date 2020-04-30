using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Messages;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlPanelFormular : ControlPanel, IControlFormular
    {
        /// <summary>
        /// Event zum Validieren der Eingabewerte
        /// </summary>
        public event EventHandler<ValidationEventArgs> Validation;

        /// <summary>
        /// Liefert oder setzt das Layout
        /// </summary>
        public TypesLayoutForm Layout { get; set; }

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
        public List<ControlFormularItem> Items { get; set; }

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
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlPanelFormular(IPage page, string id = null)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Items = new List<ControlFormularItem>();
            ValidationResults = new List<ValidationResult>();

            EnableCancelButton = true;
            Scope = ParameterScope.Local;
            Name = "Form";

            SubmitButton = new ControlFormularItemButton(this)
            {
                Name = "submit_" + Name.ToLower(),
                Text = "Speichern",
                Icon = "fas fa-save",
                Layout = TypesLayoutButton.Success,
                Type = "submit",
                Value = "1"
            };

            SubmitAndNextButton = new ControlFormularItemButton(this)
            {
                Name = "next_" + Name.ToLower(),
                Text = "Speichern und weiter",
                Icon = "fas fa-forward",
                Layout = TypesLayoutButton.Success,
                Type = "submit",
                Value = "1"
            };

            CancelButton = new ControlButtonLink(Page)
            {
                Text = "Abbrechen",
                Icon = TypeIcon.Times,
                Layout = TypesLayoutButton.Danger,
                Color = new PropertyColorText(TypeColorText.White),
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
                        Page.Redirecting(RedirectUrl);
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
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            CancelButton.Uri = RedirectUrl;

            switch (Layout)
            {
                case TypesLayoutForm.Inline:
                    Classes.Add("form-inline");
                    break;
            }

            // Prüfe ob Formular abgeschickt wurde
            if (string.IsNullOrWhiteSpace(GetParam(SubmitButton.Name)))
            {
                OnInit();
            }

            var button = SubmitButton.ToHtml();
            var next = SubmitAndNextButton.ToHtml();
            var cancel = CancelButton.ToHtml();

            var html = new HtmlElementFormForm()
            {
                ID = ID,
                Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role,
                Name = Name.ToLower() != "form" ? "form_" + Name.ToLower() : Name.ToLower(),
                Action = Uri?.ToString(),
                Method = "post"
            };

            html.Elements.AddRange(Content.Select(x => x.ToHtml()));

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
                        bgColor = new PropertyColorBackgroundAlert(TypeColorBackground.Success);
                        break;
                }

                html.Elements.Add(new ControlAlert(Page)
                {
                    BackgroundColor = bgColor,
                    Text = v.Text,
                    Dismissible = TypesDismissibleAlert.Dismissible,
                    Fade = TypesFade.FadeShow
                }.ToHtml());
            }

            foreach (var v in Items)
            {
                html.Elements.Add(new ControlFormularLabelGroup(this) { Item = v }.ToHtml());
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
        public void Add(ControlFormularItem item)
        {
            Items.Add(item);
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
