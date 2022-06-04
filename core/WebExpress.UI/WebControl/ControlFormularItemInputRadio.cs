using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;

namespace WebExpress.UI.WebControl
{
    public class ControlFormularItemInputRadio : ControlFormularItemInput
    {
        /// <summary>
        /// Liefert oder setzt den Wert der Optiopn
        /// </summary>
        public string Option { get; set; }

        ///// <summary>
        ///// Liefert oder setzt den Wert der RadioButtons
        ///// </summary>
        //public new string Value
        //{
        //    get => GetParam(Name);
        //    set
        //    {
        //        var v = GetParam(Name);

        //        if (string.IsNullOrWhiteSpace(v))
        //        {
        //            AddParam(Name, value, Formular.Scope);
        //        }
        //    }
        //}

        /// <summary>
        /// Liefert oder setzt ob die Checkbox in einer neuen Zeile angezeigt werden soll
        /// </summary>
        public bool Inline { get; set; }

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        public string Description { get; set; }

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
        /// <param name="id">Die ID</param>
        public ControlFormularItemInputRadio(string id = null)
            : base(id)
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="name">Der Name der TextBox</param>
        public ControlFormularItemInputRadio(string id, string name)
            : this(id)
        {
            Name = name;
        }

        /// <summary>
        /// Initialisiert das Formularelement
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public override void Initialize(RenderContextFormular context)
        {
            //AddParam(name, Formular.Scope);
            //Value = GetParam(Name);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContextFormular context)
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
                        ID = Id,
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
                    new HtmlText(string.IsNullOrWhiteSpace(Description) ? string.Empty : "&nbsp;" + Description)
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
        /// <param name="context">Der Kontext, indem die Eingaben validiert werden</param>
        public override void Validate(RenderContextFormular context)
        {
            base.Validate(context);
        }
    }
}
