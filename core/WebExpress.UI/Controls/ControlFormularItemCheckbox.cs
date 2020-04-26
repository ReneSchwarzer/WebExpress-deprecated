using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;

namespace WebExpress.UI.Controls
{
    public class ControlFormularItemCheckbox : ControlFormularItemInput
    {
        /// <summary>
        /// Liefert oder setzt den Wert der TextBox
        /// </summary>
        public new bool Value
        {
            get => GetParam(Name) == "t" ? true : false;
            set
            {
                var v = GetParam(Name);

                if (string.IsNullOrWhiteSpace(v))
                {
                    AddParam(Name, value ? "t" : "f", Formular.Scope);
                }
            }
        }

        /// <summary>
        /// Liefert oder setzt ob die Checkbox in einer neuen Zeile angezeigt werden soll
        /// </summary>
        public bool Inline { get; set; }

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        public string Discription { get; set; }

        /// <summary>
        /// Liefert oder setzt ein Suchmuster, welches den Inhalt prüft
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="formular">Das zugehörige Formular</param>
        /// <param name="id">Die ID</param>
        public ControlFormularItemCheckbox(IControlFormular formular, string id = null)
            : base(formular, id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            var c = new List<string>
            {
                "checkbox"
            };

            if (Inline)
            {
                c.Add("form-check-inline");
            }

            if (Disabled)
            {
                c.Add("disabled");
            }

            var html = new HtmlElementTextContentDiv
            (
                new HtmlElementFieldLabel
                (
                    new HtmlElementFieldInput()
                    {
                        Name = Name,
                        Pattern = Pattern,
                        Type = "checkbox",
                        Disabled = Disabled,
                        Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                        Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                        Role = Role,
                        Checked = Value
                    },
                    new HtmlText(string.IsNullOrWhiteSpace(Discription) ? string.Empty : "&nbsp;" + Discription)
                )
                {
                }
            )
            {
                Class = string.Join(" ", c.Where(x => !string.IsNullOrWhiteSpace(x)))
            };

            return html;
        }

        /// <summary>
        /// Prüft das Eingabeelement auf Korrektheit der Daten
        /// </summary>
        public override void Validate()
        {
            base.Validate();
        }
    }
}
