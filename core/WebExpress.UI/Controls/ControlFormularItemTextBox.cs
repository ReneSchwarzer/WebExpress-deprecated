using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.Scripts;

namespace WebExpress.UI.Controls
{
    public class ControlFormularItemTextBox : ControlFormularItemInput
    {
        /// <summary>
        /// Das Steuerelement wird automatisch initialisiert
        /// </summary>
        public bool AutoInitialize { get; set; }

        /// <summary>
        /// Bestimmt, ob es sich um ein mehrzeileige TextBox handelt
        /// </summary>
        public TypesEditTextFormat Format { get; set; }

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        public string Discription { get; set; }

        /// <summary>
        /// Liefert oder setzt ein Platzhaltertext
        /// </summary>
        public string Placeholder { get; set; }

        /// <summary>
        /// Liefert oder setzt die minimale Länge
        /// </summary>
        public string MinLength { get; set; }

        /// <summary>
        /// Liefert oder setzt die maximale Länge
        /// </summary>
        public string MaxLength { get; set; }

        /// <summary>
        /// Liefert oder setzt ob Eingaben erzwungen werden
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Liefert oder setzt ein Suchmuster, welches den Inhalt prüft
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// Liefert oder setzt die Höhe des Textfeldes (bei Multiline und WYSIWYG)
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Liefert den Initialisierungscode (JQuerry)
        /// </summary>
        public string InitializeCode => new JQuerry(!string.IsNullOrWhiteSpace(ID) ? ID : "summernote").ToString() + ".summernote({ tabsize: 2, height: " + Height + ", lang: 'de-DE' });";

        /// <summary>
        /// Liefert den Zerstörungscode (JQuerry)
        /// </summary>
        public string DestroyCode => new JQuerry(!string.IsNullOrWhiteSpace(ID) ? ID : "summernote").ToString() + ".summernote('destroy');";

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="formular">Das zugehörige Formular</param>
        /// <param name="id">Die ID</param>
        public ControlFormularItemTextBox(IControlFormular formular, string id = null)
            : base(formular, !string.IsNullOrWhiteSpace(id) ? id : "note")
        {
            Name = ID;

            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="formular">Das zugehörige Formular</param>
        /// <param name="id">Die ID</param>
        /// <param name="name">Der Name der TextBox</param>
        public ControlFormularItemTextBox(IControlFormular formular, string id, string name)
            : base(formular, !string.IsNullOrWhiteSpace(id) ? id : "note")
        {
            Name = name;

            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Height = 200;
            AutoInitialize = true;
            Format = TypesEditTextFormat.Default;
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            if (Page.HasParam(Name))
            {
                Value = Page.GetParam(Name);
            }

            Classes.Add("form-control");

            if (Disabled)
            {
                Classes.Add("disabled");
            }

            switch (ValidationResult)
            {
                case TypesInputValidity.Success:
                    Classes.Add("input-success");
                    break;
                case TypesInputValidity.Warning:
                    Classes.Add("input-warning");
                    break;
                case TypesInputValidity.Error:
                    Classes.Add("input-error");
                    break;
            }

            if (AutoInitialize && Format == TypesEditTextFormat.Wysiwyg)
            {
                Page.AddScript(ID, InitializeCode);
                AutoInitialize = false;
            }

            switch (Format)
            {
                case TypesEditTextFormat.Multiline:
                    return new HtmlElementFormTextarea()
                    {
                        ID = ID,
                        Value = Value,
                        Name = Name,
                        Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                        Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                        Role = Role,
                        Placeholder = Placeholder
                    };
                case TypesEditTextFormat.Wysiwyg:
                    return new HtmlElementFormTextarea()
                    {
                        ID = ID,
                        Value = Value,
                        Name = Name,
                        Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                        Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                        Role = Role,
                        Placeholder = Placeholder
                    };
                default:
                    return new HtmlElementFieldInput()
                    {
                        ID = ID,
                        Value = Value,
                        Name = Name,
                        MinLength = MinLength,
                        MaxLength = MaxLength,
                        Required = Required,
                        Pattern = Pattern,
                        Type = "text",
                        Disabled = Disabled,
                        Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                        Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                        Role = Role,
                        Placeholder = Placeholder
                    };

            }

        }

        /// <summary>
        /// Prüft das Eingabeelement auf Korrektheit der Daten
        /// </summary>
        public override void Validate()
        {
            if (Disabled)
            {
                return;
            }

            if (Required && string.IsNullOrWhiteSpace(base.Value))
            {
                ValidationResults.Add(new ValidationResult() { Type = TypesInputValidity.Error, Text = "Das Textfeld darf nicht leer sein!" });

                return;
            }

            if (!string.IsNullOrWhiteSpace(MinLength) && Convert.ToInt32(MinLength) > base.Value.Length)
            {
                ValidationResults.Add(new ValidationResult() { Type = TypesInputValidity.Error, Text = "Der Text entsprcht nicht der minimalen Länge von " + MinLength + "!" });
            }

            if (!string.IsNullOrWhiteSpace(MaxLength) && Convert.ToInt32(MaxLength) < base.Value.Length)
            {
                ValidationResults.Add(new ValidationResult() { Type = TypesInputValidity.Error, Text = "Der Text ist größer als die maximalen Länge von " + MaxLength + "!" });
            }

            base.Validate();
        }
    }
}
