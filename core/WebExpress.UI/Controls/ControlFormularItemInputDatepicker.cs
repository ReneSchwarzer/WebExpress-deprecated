using System;
using System.Linq;
using WebExpress.Html;

namespace WebExpress.UI.Controls
{
    public class ControlFormularItemInputDatepicker : ControlFormularItemInput
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
        /// <param name="id">Die ID</param>
        public ControlFormularItemInputDatepicker(string id = null)
            : base(!string.IsNullOrWhiteSpace(id) ? id : "datepicker")
        {
        }

        /// <summary>
        /// Initialisiert das Formularelement
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public override void Initialize(RenderContextFormular context)
        {
            AutoInitialize = true;
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContextFormular context)
        {
            if (context.Page.HasParam(Name))
            {
                Value = context.Page.GetParam(Name);
            }

            Classes.Add("form-control");

            if (Disabled)
            {
                Classes.Add("disabled");
            }

            if (AutoInitialize)
            {
                context.Page.AddScript(ID, InitializeCode);
                AutoInitialize = false;
            }

            var html = new HtmlElementFieldInput()
            {
                ID = ID,
                Name = Name,
                DataProvide = "datepicker",
                Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role,
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
