using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Module;
using WebExpress.Uri;

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
            var module = ModuleManager.GetModule(context.ApplicationID, "webexpress.ui");
            if (module != null)
            {
                context.VisualTree.HeaderScriptLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/js/simpletags.js")));
                context.VisualTree.CssLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/css/simpletags.css")));
            }

            Value = context?.Request.GetParameter(Name)?.Value;
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContextFormular context)
        {
            var list = new List<string>();

            if (Value != null)
            {
                list.AddRange(Value.Split(';', System.StringSplitOptions.RemoveEmptyEntries).Select(x => $"\"{ x.Trim() }\""));
            }

            context.VisualTree.AddScript(ID, $"new Tags('#tag_{ ID }', '{ Name }', [{ string.Join(',', list) }]);");

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
        }
    }
}

