using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Messages;
using WebExpress.Pages;
using WebServer.Html;

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
        /// Liefert oder setzt die Ziel-Url
        /// </summary>
        public Path Url { get; set; }

        /// <summary>
        /// Liefert oder setzt die Weiterleitungs-Url
        /// </summary>
        public Path RedirectUrl { get; set; }

        /// <summary>
        /// Liefert oder setzt die Abbruchs-Url
        /// </summary>
        public Path BackUrl { get; set; }

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
                Icon = Icon.Times,
                Layout = TypesLayoutButton.Danger,
                Color = TypesTextColor.White,
                HorizontalAlignment = TypesHorizontalAlignment.Right,
                Url = Url
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
            CancelButton.Url = RedirectUrl;

            var classes = new List<string>
            {
                Class
            };

            switch (Layout)
            {
                case TypesLayoutForm.Inline:
                    classes.Add("form-inline");
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

            var html = new HtmlElementForm()
            {
                ID = ID,
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = Style,
                Name = Name.ToLower() != "form" ? "form_" + Name.ToLower() : Name.ToLower(),
                Action = Url?.ToString(),
                Method = "post"
            };

            html.Elements.AddRange(Content.Select(x => x.ToHtml()));

            foreach (var v in ValidationResults)
            {
                var layout = TypesLayoutAlert.Default;

                switch (v.Type)
                {
                    case TypesInputValidity.Error:
                        layout = TypesLayoutAlert.Danger;
                        break;
                    case TypesInputValidity.Warning:
                        layout = TypesLayoutAlert.Warning;
                        break;
                    case TypesInputValidity.Success:
                        layout = TypesLayoutAlert.Success;
                        break;
                }

                html.Elements.Add(new ControlAlert(Page)
                {
                    Layout = layout,
                    Text = v.Text,
                    Dismissible = true,
                    Fade = true
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
