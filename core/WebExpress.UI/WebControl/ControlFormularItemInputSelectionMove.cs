using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using WebExpress.Html;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.UI.WebControl
{
    public class ControlFormularItemInputSelectionMove : ControlFormularItemInput
    {
        /// <summary>
        /// Liefert die Einträge
        /// </summary>
        public ICollection<ControlFormularItemInputSelectionItem> Options { get; } = new List<ControlFormularItemInputSelectionItem>();

        /// <summary>
        /// Liefert oder setzt die Beschriftung der ausgewählten Optionen
        /// </summary>
        public string SelectedHeader { get; set; } = "webexpress.ui:form.selectionmove.selected";

        /// <summary>
        /// Liefert oder setzt die Beschriftung der vorhandenen Optionen
        /// </summary>
        public string AvailableHeader { get; set; } = "webexpress.ui:form.selectionmove.available";

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularItemInputSelectionMove(string id = null)
            : base(id)
        {
            Name = ID;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="items">Die Einträge</param>
        public ControlFormularItemInputSelectionMove(string id, params ControlFormularItemInputSelectionItem[] items)
            : this(id)
        {
            (Options as List<ControlFormularItemInputSelectionItem>).AddRange(items);
        }

        /// <summary>
        /// Initialisiert das Formularelement
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public override void Initialize(RenderContextFormular context)
        {
            if (context.Request.HasParameter(Name))
            {
                Value = context?.Request.GetParameter(Name)?.Value;
            }
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
                case TypesInputValidity.Warning:
                    classes.Add("input-warning");
                    break;
                case TypesInputValidity.Error:
                    classes.Add("input-error");
                    break;
            }

            var html = new HtmlElementTextContentDiv()
            {
                ID = $"selection-move-{ID}",
                Style = GetStyles()
            };

            context.VisualTree.AddScript(ID, GetScript(context, $"selection-move-{ID}", string.Join(" ", classes)));

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

        /// <summary>
        /// Erzeugt das Javascript zur Ansteuerung des Steuerelements
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <param name="id">Die ID des Steuerelmentes</param>
        /// <param name="css">Die CSS-KLassen, die dem Steuerelement zugewiesen werden</param>
        /// <returns>Der Javascript-Code</returns>
        protected virtual string GetScript(RenderContextFormular context, string id, string css)
        {
            var settings = new 
            { 
                ID = id,
                Name = ID,
                CSS = css,
                Header = new 
                {
                    Selected = I18N(context, SelectedHeader),
                    Available = I18N(context, AvailableHeader)
                },
                Buttons = new 
                {
                    ToSelectedAll = I18N(context, "˂˂"),
                    ToSelected = I18N(context, "˂"),
                    ToAvailableAll = I18N(context, "˃˃"),
                    ToAvailable = I18N(context, "˃")
                }
            };

            var jsonOptions = new JsonSerializerOptions { WriteIndented = false };
            var settingsJson = JsonSerializer.Serialize(settings, jsonOptions);
            var optionsJson = JsonSerializer.Serialize(Options, jsonOptions);
            var valuesJson = JsonSerializer.Serialize(Value?.Split(";", System.StringSplitOptions.RemoveEmptyEntries), jsonOptions);
            var builder = new StringBuilder();

            builder.Append($"var options = { optionsJson };");
            builder.Append($"var settings = { settingsJson };");
            builder.Append($"var container = $('#{ id }');");
            builder.Append($"var obj = new selectionMoveCtrl(settings);");
            builder.Append($"obj.options = options;");
            builder.Append($"obj.value = { (!string.IsNullOrWhiteSpace(valuesJson) ? valuesJson : "[]")  };");
            builder.Append($"container.replaceWith(obj.getCtrl);");

            return builder.ToString();
        }
    }
}

