using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using WebExpress.Html;

namespace WebExpress.UI.WebControl
{
    public class ControlFormItemInputSelection : ControlFormItemInput
    {
        /// <summary>
        /// Returns the entries.
        /// </summary>
        public ICollection<ControlFormItemInputSelectionItem> Options { get; } = new List<ControlFormItemInputSelectionItem>();

        /// <summary>
        /// Returns or sets the label of the selected options.
        /// </summary>
        public string Placeholder { get; set; }

        /// <summary>
        /// Allows you to select multiple items.
        /// </summary>
        public bool MultiSelect { get; set; }

        /// <summary>
        /// Returns or sets the OnChange attribute.
        /// </summary>
        public PropertyOnChange OnChange { get; set; }

        /// <summary>
        /// Returns or sets the value.
        /// </summary>
        public virtual ICollection<string> Values => base.Value != null ? base.Value.Split(';', System.StringSplitOptions.RemoveEmptyEntries) : new List<string>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
        public ControlFormItemInputSelection(string id = null)
            : base(id ?? Guid.NewGuid().ToString())
        {
            Name = Id;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
        /// <param name="items">The entries.</param>
        public ControlFormItemInputSelection(string id, params ControlFormItemInputSelectionItem[] items)
            : this(id)
        {
            (Options as List<ControlFormItemInputSelectionItem>).AddRange(items);
        }

        /// <summary>
        /// Initializes the form element.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        public override void Initialize(RenderContextFormular context)
        {
            if (context.Request.HasParameter(Name))
            {
                Value = context?.Request.GetParameter(Name)?.Value;
            }
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
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
                Id = Id,
                Style = GetStyles()
            };

            context.AddScript(Id, GetScript(context, Id, string.Join(" ", classes)));

            return html;
        }

        /// <summary>
        /// Checks the input element for correctness of the data.
        /// </summary>
        /// <param name="context">The context in which the inputs are validated.</param>
        public override void Validate(RenderContextFormular context)
        {
            base.Validate(context);
        }

        /// <summary>
        /// Generates the javascript to control the control.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <param name="id">The ID of the control.</param>
        /// <param name="css">The CSS classes that are assigned to the control.</param>
        /// <returns>The javascript code.</returns>
        protected virtual string GetScript(RenderContextFormular context, string id, string css)
        {
            var settings = new
            {
                Id = id,
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

