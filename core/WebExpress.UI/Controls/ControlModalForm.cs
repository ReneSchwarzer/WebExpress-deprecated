using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Messages;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlModalForm : ControlModal, IControlFormular
    {
        /// <summary>
        /// Event zum Validieren der Eingabewerte
        /// </summary>
        public event EventHandler<ValidationEventArgs> Validation;

        /// <summary>
        /// Liefert oder setzt den Formularnamen
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Liefert oder setzt die Ziel-Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Liefert oder setzt das Layout
        /// </summary>
        public TypesLayoutForm Layout { get; set; }

        /// <summary>
        /// Liefert oder setzt den Gültigkeitsbereich der Formulardaten
        /// </summary>
        public ParameterScope Scope { get; set; }

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
        public ControlModalForm(IPage page, string id)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="name">Der Formularname</param>
        public ControlModalForm(IPage page, string id, string name)
            : base(page, id, string.Empty)
        {
            Init();

            Name = name;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="name">Der Name</param>
        /// <param name="content">Der Inhalt</param>
        public ControlModalForm(IPage page, string id, string name, params Control[] content)
            : base(page, id, string.Empty, content)
        {
            Init();

            Name = name;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="name">Der Name</param>
        /// <param name="content">Der Inhalt</param>
        public ControlModalForm(IPage page, string id, string name, IEnumerable<Control> content)
            : base(page, id, string.Empty, content)
        {
            Init();

            Name = name;
        }

        /// <summary>
        /// Initialisierung
        private void Init()
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            var classes = new List<string>
            {
                Class,
                "modal"
            };

            if (Fade)
            {
                classes.Add("fade");
            }

            var headerText = new HtmlElementH4(Header)
            {
                Class = "modal-title"
            };

            var headerButtonLabel = new HtmlElementSpan(new HtmlText("&times;"))
            {
            };
            headerButtonLabel.AddUserAttribute("aria-hidden", "true");

            var headerButton = new HtmlElementButton(headerButtonLabel)
            {
                Class = "close"
            };
            headerButton.AddUserAttribute("aria-label", "close");
            headerButton.AddUserAttribute("data-dismiss", "modal");

            var header = new HtmlElementDiv(headerText, headerButton)
            {
                Class = "modal-header"
            };

            var body = new HtmlElementDiv(from x in Content select x.ToHtml())
            {
                Class = "modal-body"
            };

            var footerButtonOK = new HtmlElementButton(new HtmlText("OK"))
            {
                Type = "submit",
                Class = "btn btn-success"
            };
            //footerButtonOK.AddUserAttribute("data-dismiss", "modal");

            var footerButtonCancel = new HtmlElementButton(new HtmlText("Abbrechen"))
            {
                Type = "button",
                Class = "btn btn-danger"
            };
            footerButtonCancel.AddUserAttribute("data-dismiss", "modal");

            var footer = new HtmlElementDiv(footerButtonOK, footerButtonCancel)
            {
                Class = "modal-footer"
            };

            var form = new HtmlElementForm(header, body, footer)
            {
                Action = "#" + ID,
                Method = "post",
                Name = "form_" + ID
            };

            var content = new HtmlElementDiv(form)
            {
                Class = "modal-content"
            };

            var dialog = new HtmlElementDiv(content)
            {
                Class = "modal-dialog",
                Role = "document"
            };

            var html = new HtmlElementDiv(dialog)
            {
                ID = ID,
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = Style,
                Role = "dialog"
            };

            if (!string.IsNullOrWhiteSpace(OnShownCode))
            {
                var shown = "$('#" + ID + "').on('shown.bs.modal', function(e) { " + OnShownCode + " });";
                Page.AddScript(ID + "_shown", shown);
            }

            if (!string.IsNullOrWhiteSpace(OnHiddenCode))
            {
                var hidden = "$('#" + ID + "').on('hidden.bs.modal', function() { " + OnHiddenCode + " });";
                Page.AddScript(ID + "_hidden", hidden);
            }

            return html;
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Daten des Formulars zu laden sind
        /// </summary>
        protected virtual void OnLoad()
        {

        }

        /// <summary>
        /// Wird aufgerufen, wenn die Daten des Formulars zu speichern sind
        /// </summary>
        protected virtual void OnSave()
        {

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

            //foreach (var v in Items.Where(x => x is ControlFormularItemInput).Select(x => x as ControlFormularItemInput))
            //{
            //    v.Validate();

            //    if (v.ValidationResult == TypesInputValidity.Error)
            //    {
            //        valid = false;
            //    }
            //}

            //var args = new ValidationEventArgs() { Value = null };
            //OnValidation(args);

            //ValidationResults.AddRange(args.Results);

            Valid = valid;
        }
    }
}
