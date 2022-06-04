using System;
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
        /// Erlaubt die Auswahl mehrerer Elmente
        /// </summary>
        public bool MultiSelect { get; set; }

        /// <summary>
        /// Liefert oder setzt die OnChange-Attribut
        /// </summary>
        public PropertyOnChange OnChange { get; set; }

        /// <summary>
        /// Liefert oder setzt den Wert
        /// </summary>
        public virtual ICollection<string> Values => base.Value != null ? base.Value.Split(';', System.StringSplitOptions.RemoveEmptyEntries) : new List<string>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularItemInputSelection(string id = null)
            : base(id ?? Guid.NewGuid().ToString())
        {
            Name = Id;
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
                case TypesInputValidity.Warning:
                    classes.Add("input-warning");
                    break;
                case TypesInputValidity.Error:
                    classes.Add("input-error");
                    break;
            }

            var html = new HtmlElementTextContentDiv()
            {
                ID = Id,
                Style = GetStyles()
            };

            context.AddScript(Id, GetScript(context, Id, string.Join(" ", classes)));

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
                Name = Id,
                CSS = css,
                Placeholder,
                MultiSelect
            };

            var jsonOptions = new JsonSerializerOptions { WriteIndented = false };
            var settingsJson = JsonSerializer.Serialize(settings, jsonOptions);
            var optionsJson = JsonSerializer.Serialize(Options, jsonOptions);
            var builder = new StringBuilder();

            builder.AppendLine($"let options = {optionsJson};");
            builder.AppendLine($"let settings = {settingsJson};");
            builder.AppendLine($"let container = $('#{id}');");
            builder.AppendLine($"let obj = new webexpress.ui.selectionCtrl(settings);");
            builder.AppendLine($"obj.options = options;");
            builder.AppendLine($"obj.value = [{string.Join(",", Values.Select(x => $"'{x}'"))}];");

            if (OnChange != null)
            {
                builder.AppendLine($"obj.on('webexpress.ui.change.value', {OnChange});");
            }

            builder.AppendLine($"container.replaceWith(obj.getCtrl);");

            return builder.ToString();
        }
    }
}

