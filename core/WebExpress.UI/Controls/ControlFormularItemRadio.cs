using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;

namespace WebExpress.UI.Controls
{
    public class ControlFormularItemRadio : ControlFormularItemInput
    {
        /// <summary>
        /// Liefert oder setzt den Wert der Optiopn
        /// </summary>
        public string Option { get; set; }

        /// <summary>
        /// Liefert oder setzt den Wert der RadioButtons
        /// </summary>
        public new string Value
        {
            get => GetParam(Name);
            set
            {
                var v = GetParam(Name);

                if (string.IsNullOrWhiteSpace(v))
                {
                    AddParam(Name, value, Formular.Scope);
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
        /// Liefert oder setzt ob der Radiobutton ausgewählt ist
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="formular">Das zugehörige Formular</param>
        /// <param name="id">Die ID</param>
        public ControlFormularItemRadio(IControlFormular formular, string id)
            : base(formular, id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="formular">Das zugehörige Formular</param>
        /// <param name="id">Die ID</param>
        /// <param name="name">Der Name der TextBox</param>
        public ControlFormularItemRadio(IControlFormular formular, string id, string name)
            : this(formular, id)
        {
            Name = name;

            AddParam(name, Formular.Scope);
            Value = GetParam(Name);
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
            if (!string.IsNullOrWhiteSpace(Value))
            {
                Checked = Value == Option;
            }

            var c = new List<string>
            {
                "radio"
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
                        ID = ID,
                        Name = Name,
                        Pattern = Pattern,
                        Type = "radio",
                        Disabled = Disabled,
                        Checked = Checked,
                        Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                        Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                        Role = Role,
                        Value = Option
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
