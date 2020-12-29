using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.Module;
using WebExpress.Uri;

namespace WebExpress.UI.WebControl
{
    public class ControlFormularItemInputMoveSelector : ControlFormularItemInput
    {
        /// <summary>
        /// Liefert die Einträge
        /// </summary>
        public ICollection<ControlFormularItemInputMoveSelectorItem> Options { get; } = new List<ControlFormularItemInputMoveSelectorItem>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularItemInputMoveSelector(string id = null)
            : base(id)
        {
            Name = ID;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="items">Die Einträge</param>
        public ControlFormularItemInputMoveSelector(string id, params ControlFormularItemInputMoveSelectorItem[] items)
            : this(id)
        {
            (Options as List<ControlFormularItemInputMoveSelectorItem>).AddRange(items);
        }

        /// <summary>
        /// Initialisiert das Formularelement
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public override void Initialize(RenderContextFormular context)
        {
            var module = ModuleManager.GetModule("webexpress");
            if (module != null)
            {
                context.Page.CssLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/css/moveselector.css")));
                context.Page.HeaderScriptLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/js/moveselector.js")));
            }

            if (context.Page.HasParam(Name))
            {
                Value = context?.Page.GetParamValue(Name);
            }

            var value = string.Empty;
            var options = string.Join(",", Options.Select(x => "{id:'" + x.ID + "',value:'" + x.Value + "'}"));

            if (Value != null)
            {
                value = string.Join(",", Value?.Split(";", System.StringSplitOptions.RemoveEmptyEntries).Select(x => $"'{ x }'"));
            }

            context.Page.AddScript(ID, $"new MoveSelector('#moveselector_{ ID }', '{ Name }', [{ options }], [{ value }]);");
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContextFormular context)
        {
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

            var html = new HtmlElementTextContentDiv()
            {
                ID = $"moveselector_{ID}",
                Class = Css.Concatenate("moveselector", GetClasses()), // form-control
                Style = GetStyles(),
                Role = "moveselector"
            };

            var selectedHeader = new ControlText("selectedHeader") { Text = context.I18N("webexpress", "form.moveselector.selected"), TextColor = new PropertyColorText(TypeColorText.Muted), Format = TypeFormatText.Paragraph };
            var selectedList = new ControlList("selectedOptions") { Layout = TypeLayoutList.Flush };
            var leftAllButton = new ControlButton("") { Text = "<<", BackgroundColor = new PropertyColorButton(TypeColorButton.Primary), Block = TypeBlockButton.Block };
            var leftButton = new ControlButton("") { Text = "<", BackgroundColor = new PropertyColorButton(TypeColorButton.Primary), Block = TypeBlockButton.Block };
            var rightButton = new ControlButton("") { Text = ">", BackgroundColor = new PropertyColorButton(TypeColorButton.Primary), Block = TypeBlockButton.Block };
            var rightAllButton = new ControlButton("") { Text = ">>", BackgroundColor = new PropertyColorButton(TypeColorButton.Primary), Block = TypeBlockButton.Block };
            var availableHeader = new ControlText("availableHeader") { Text = context.I18N("webexpress", "form.moveselector.available"), TextColor = new PropertyColorText(TypeColorText.Muted), Format = TypeFormatText.Paragraph };
            var availableList = new ControlList("availableOptions") { Layout = TypeLayoutList.Flush };
            var sticky = new List<string>(new string[] { "position: sticky;" });
            
            html.Elements.Add(new HtmlElementTextContentDiv
            (
                selectedHeader.Render(context),
                selectedList.Render(context)
            )
            { Class = "moveselector-list" });

            html.Elements.Add(new HtmlElementTextContentDiv
            (
                leftAllButton.Render(context),
                leftButton.Render(context),
                rightButton.Render(context),
                rightAllButton.Render(context)
            )
            { Class = "moveselector-button" });

            html.Elements.Add(new HtmlElementTextContentDiv
            (
                availableHeader.Render(context),
                availableList.Render(context)
            ) 
            { Class = "moveselector-list" });

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

