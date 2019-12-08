using System;
using System.Collections.Generic;
using System.Linq;
using WebServer.Html;

namespace WebExpress.UI.Controls
{
    public class ControlFormularItemDatepicker : ControlFormularItemInput
    {
        /// <summary>
        /// Das Steuerelement wird automatisch initialisiert
        /// </summary>
        public bool AutoInitialize { get; set; }

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        public string Discription { get; set; }

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
        /// Liefert den Initialisierungscode (JQuerry)
        /// </summary>
        public string InitializeCode => "$('#" + ID + " input').datepicker({ startDate: -3 });";

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="formular">Das zugehörige Formular</param>#
        /// <param name="id">Die ID</param>
        public ControlFormularItemDatepicker(IControlFormular formular, string id = null)
            : base(formular, !string.IsNullOrWhiteSpace(id) ? id : "datepicker")
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            AutoInitialize = true;

            //AddParam(Name, Formular.Scope);
            //Value = Page.GetParam(Name);
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

            var classes = new List<string>
            {
                Class,
                "form-control"
            };

            if (Disabled)
            {
                classes.Add("disabled");
            }

            if (AutoInitialize)
            {
                Page.AddScript(ID, InitializeCode);
                AutoInitialize = false;
            }

            var html = new HtmlElementInput()
            {
                ID = ID,
                Name = Name,
                DataProvide = "datepicker",
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Value = Value
            };
            html.AddUserAttribute("data-date-format", "dd.mm.yyyy");
            html.AddUserAttribute("data-date-autoclose", "true");
            html.AddUserAttribute("data-date-language", "de");

            return html;
        }

        /// <summary>
        /// Prüft das Eingabeelement auf Korrektheit der Daten
        /// </summary>
        public override void Validate()
        {
            if (!string.IsNullOrWhiteSpace(Value))
            {
                try
                {
                    var date = Convert.ToDateTime(Value);
                }
                catch
                {
                    ValidationResults.Add(new ValidationResult() { Type = TypesInputValidity.Error, Text = "Der angegebene Wert kann nicht in ein Datum konvertiert werden!" });
                }
            }

            base.Validate();
        }
    }
}
