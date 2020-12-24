using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;

namespace WebExpress.UI.WebControl
{
    public class ControlFormularItemInputTag : ControlFormularItemInput
    {
        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Liefert oder setzt ein Platzhaltertext
        /// </summary>
        public string Placeholder { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularItemInputTag(string id = null)
            : base(id)
        {
            Name = ID;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.None);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="name">Der Name der TextBox</param>
        public ControlFormularItemInputTag(string id, string name)
            : base(id)
        {
            Name = name;
        }

        /// <summary>
        /// Initialisiert das Formularelement
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public override void Initialize(RenderContextFormular context)
        {
            if (context.Page.HasParam(Name))
            {
                Value = context?.Page.GetParamValue(Name);
            }

            var list = new List<string>();
            
            if (Value!=null)
            {
                list.AddRange(Value.Split(';', System.StringSplitOptions.RemoveEmptyEntries).Select(x => $"\"{ x.Trim() }\""));
            }
                        
            context.Page.AddScript(ID, $"const tags = new Tags('#tag_{ ID }', '{ Name }', [{ string.Join(',', list) }]);");
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContextFormular context)
        {
            //Classes.Add("form-control");

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

            var html = new HtmlElementTextContentDiv() // HtmlElementFieldInput form-control 
            {
                ID = $"tag_{ID}",
                Class = Css.Concatenate("form-control simpletags", GetClasses()),
                Style = GetStyles(),
                Role = "tags"
            };


            return html;
        }

        /// <summary>
        /// Prüft das Eingabeelement auf Korrektheit der Daten
        /// </summary>
        public override void Validate()
        {
            //if (Disabled)
            //{
            //    return;
            //}

            //if (Required && string.IsNullOrWhiteSpace(base.Value))
            //{
            //    ValidationResults.Add(new ValidationResult() { Type = TypesInputValidity.Error, Text = "Das Textfeld darf nicht leer sein!" });

            //    return;
            //}

            //if (!string.IsNullOrWhiteSpace(MinLength?.ToString()) && Convert.ToInt32(MinLength) > base.Value.Length)
            //{
            //    ValidationResults.Add(new ValidationResult() { Type = TypesInputValidity.Error, Text = "Der Text entsprcht nicht der minimalen Länge von " + MinLength + "!" });
            //}

            //if (!string.IsNullOrWhiteSpace(MaxLength?.ToString()) && Convert.ToInt32(MaxLength) < base.Value.Length)
            //{
            //    ValidationResults.Add(new ValidationResult() { Type = TypesInputValidity.Error, Text = "Der Text ist größer als die maximalen Länge von " + MaxLength + "!" });
            //}

            //base.Validate();
        }
    }
}

