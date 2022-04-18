using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using WebExpress.Html;

namespace WebExpress.UI.WebControl
{
    public class ControlFormularItemInputSelection : ControlFormularItemInput
    {
        /// <summary>
        /// Liefert die Einträge
        /// </summary>
        public ICollection<ControlFormularItemInputSelectionItem> Options { get; } = new List<ControlFormularItemInputSelectionItem>();

        /// <summary>
        /// Liefert oder setzt die Beschriftung der ausgewählten Optionen
        /// </summary>
        public string Placeholder { get; set; }

        /// <summary>
        /// Bestimmt, ob ein leerer Wert (null) gültig ist
        /// </summary>
        public bool HasEmptyValue { get; set; }

        /// <summary>
        /// Liefert oder setzt die OnChange-Attribut
        /// </summary>
        public PropertyOnChange OnChange { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularItemInputSelection(string id = null)
            : base(string.IsNullOrEmpty(id) ? "selection" : $"selection-{id}")
        {
            Name = ID;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="items">Die Einträge</param>
        public ControlFormularItemInputSelection(string id, params ControlFormularItemInputSelectionItem[] items)
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
                ID = ID,
                Style = GetStyles()
            };

            context.VisualTree.AddScript(ID, GetScript(context, ID, string.Join(" ", classes)));

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
                Placeholder,
                HasEmptyValue
            };

            var jsonOptions = new JsonSerializerOptions { WriteIndented = false };
            var settingsJson = JsonSerializer.Serialize(settings, jsonOptions);
            var optionsJson = JsonSerializer.Serialize(Options, jsonOptions);
            var builder = new StringBuilder();

            builder.Append($"var options = { optionsJson };");
            builder.Append($"var settings = { settingsJson };");
            builder.Append($"var container = $('#{ id }');");
            builder.Append($"var obj = new selectionCtrl(settings);");
            builder.Append($"obj.options = options;");
            builder.Append($"obj.value = '{ Value }';");

            if (OnChange != null)
            {
                builder.Append($"obj.on('webexpress.ui.change.value', { OnChange });");
            }

            builder.Append($"container.replaceWith(obj.getCtrl);");

            return builder.ToString();
        }
    }
}

