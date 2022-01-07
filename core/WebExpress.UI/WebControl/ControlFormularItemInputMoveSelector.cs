using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Uri;
using WebExpress.WebModule;

namespace WebExpress.UI.WebControl
{
    public class ControlFormularItemInputMoveSelector : ControlFormularItemInput
    {
        /// <summary>
        /// Liefert die Einträge
        /// </summary>
        public ICollection<ControlFormularItemInputMoveSelectorItem> Options { get; } = new List<ControlFormularItemInputMoveSelectorItem>();

        /// <summary>
        /// Liefert oder setzt die Beschriftung der ausgewählten Optionen
        /// </summary>
        public string SelectedHeader { get; set; } = "webexpress.ui:form.moveselector.selected";

        /// <summary>
        /// Liefert oder setzt die Beschriftung der vorhandenen Optionen
        /// </summary>
        public string AvailableHeader { get; set; } = "webexpress.ui:form.moveselector.available";

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
            var module = ModuleManager.GetModule(context.Application, "webexpress.ui");
            if (module != null)
            {
                context.VisualTree.CssLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/css/moveselector.css")));
                context.VisualTree.HeaderScriptLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/js/moveselector.js")));
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
            var classes = Classes.ToList();

            if (Disabled)
            {
                classes.Add("disabled");
            }

            switch (ValidationResult)
            {
                case TypesInputValidity.Success:
                    classes.Add("input-success");
                    break;
                case TypesInputValidity.Warning:
                    classes.Add("input-warning");
                    break;
                case TypesInputValidity.Error:
                    classes.Add("input-error");
                    break;
            }

            var html = new HtmlElementTextContentDiv()
            {
                ID = $"moveselector_{ID}",
                Class = Css.Concatenate("moveselector", string.Join(" ", classes)), // form-control
                Style = GetStyles(),
                Role = "moveselector"
            };

            var selectedHeader = new ControlText("selectedHeader") { Text = SelectedHeader, TextColor = new PropertyColorText(TypeColorText.Muted), Format = TypeFormatText.Paragraph };
            var selectedList = new ControlList("selectedOptions") { Layout = TypeLayoutList.Flush };
            var leftAllButton = new ControlButton("") { Text = "<<", BackgroundColor = new PropertyColorButton(TypeColorButton.Primary), Block = TypeBlockButton.Block };
            var leftButton = new ControlButton("") { Text = "<", BackgroundColor = new PropertyColorButton(TypeColorButton.Primary), Block = TypeBlockButton.Block };
            var rightButton = new ControlButton("") { Text = ">", BackgroundColor = new PropertyColorButton(TypeColorButton.Primary), Block = TypeBlockButton.Block };
            var rightAllButton = new ControlButton("") { Text = ">>", BackgroundColor = new PropertyColorButton(TypeColorButton.Primary), Block = TypeBlockButton.Block };
            var availableHeader = new ControlText("availableHeader") { Text = AvailableHeader, TextColor = new PropertyColorText(TypeColorText.Muted), Format = TypeFormatText.Paragraph };
            var availableList = new ControlList("availableOptions") { Layout = TypeLayoutList.Flush };

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

            var value = string.Empty;
            var options = string.Join(",", Options.Select(x => "{id:'" + x.ID + "',value:'" + x.Value + "'}"));

            if (Value != null)
            {
                value = string.Join(",", Value?.Split(";", System.StringSplitOptions.RemoveEmptyEntries).Select(x => $"'{ x }'"));
            }

            context.VisualTree.AddScript(ID, $"new MoveSelector('#moveselector_{ ID }', '{ Name }', [{ options }], [{ value }]);");

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

