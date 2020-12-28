using System;
using System.Collections.Generic;
using WebExpress.Html;
using WebExpress.Message;

namespace WebExpress.UI.WebControl
{
    public class ControlModalForm : ControlModal
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
        /// <param name="id">Die ID</param>
        public ControlModalForm(string id)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="name">Der Formularname</param>
        public ControlModalForm(string id, string name)
            : base(id, string.Empty)
        {
            Init();

            Name = name;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="name">Der Name</param>
        /// <param name="content">Der Inhalt</param>
        public ControlModalForm(string id, string name, params Control[] content)
            : base(id, string.Empty, content)
        {
            Init();

            Name = name;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="name">Der Name</param>
        /// <param name="content">Der Inhalt</param>
        public ControlModalForm(string id, string name, IEnumerable<Control> content)
            : base(id, string.Empty, content)
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
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Classes.Add("modal");

            if (Fade)
            {
                Classes.Add("fade");
            }

            var headerText = new HtmlElementSectionH4(Header)
            {
                Class = "modal-title"
            };

            var headerButtonLabel = new HtmlElementTextSemanticsSpan(new HtmlText("&times;"))
            {
            };
            headerButtonLabel.AddUserAttribute("aria-hidden", "true");

            var headerButton = new HtmlElementFieldButton(headerButtonLabel)
            {
                Class = "close"
            };
            headerButton.AddUserAttribute("aria-label", "close");
            headerButton.AddUserAttribute("data-dismiss", "modal");

            var header = new HtmlElementTextContentDiv(headerText, headerButton)
            {
                Class = "modal-header"
            };

            var body = new HtmlElementTextContentDiv()
            {
                Class = "modal-body"
            };

            //foreach (var v in Content)
            //{
            //    body.Elements.Add(new FormularLabelGroup(v as FormularItem)
            //    {

            //    }.ToHtml(page, this));
            //}

            var footerButtonOK = new HtmlElementFieldButton(new HtmlText("OK"))
            {
                Type = "submit",
                Class = "btn btn-success"
            };
            //footerButtonOK.AddUserAttribute("data-dismiss", "modal");

            var footerButtonCancel = new HtmlElementFieldButton(new HtmlText("Abbrechen"))
            {
                Type = "button",
                Class = "btn btn-danger"
            };
            footerButtonCancel.AddUserAttribute("data-dismiss", "modal");

            var footer = new HtmlElementTextContentDiv(footerButtonOK, footerButtonCancel)
            {
                Class = "modal-footer"
            };

            var form = new HtmlElementFormForm(header, body, footer)
            {
                Action = "#" + ID,
                Method = "post",
                Name = "form_" + ID
            };

            var content = new HtmlElementTextContentDiv(form)
            {
                Class = "modal-content"
            };

            var dialog = new HtmlElementTextContentDiv(content)
            {
                Class = "modal-dialog",
                Role = "document"
            };

            var html = new HtmlElementTextContentDiv(dialog)
            {
                ID = ID,
                Class = GetClasses(),
                Style = GetStyles(),
                Role = "dialog"
            };

            if (!string.IsNullOrWhiteSpace(OnShownCode))
            {
                var shown = "$('#" + ID + "').on('shown.bs.modal', function(e) { " + OnShownCode + " });";
                context.Page.AddScript(ID + "_shown", shown);
            }

            if (!string.IsNullOrWhiteSpace(OnHiddenCode))
            {
                var hidden = "$('#" + ID + "').on('hidden.bs.modal', function() { " + OnHiddenCode + " });";
                context.Page.AddScript(ID + "_hidden", hidden);
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
