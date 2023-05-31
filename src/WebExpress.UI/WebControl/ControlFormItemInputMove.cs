using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using WebExpress.Html;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.UI.WebControl
{
    public class ControlFormItemInputMove : ControlFormItemInput
    {
        /// <summary>
        /// Returns the entries.
        /// </summary>
        public ICollection<ControlFormItemInputSelectionItem> Options { get; } = new List<ControlFormItemInputSelectionItem>();

        /// <summary>
        /// Returns or sets the label of the selected options.
        /// </summary>
        public string SelectedHeader { get; set; } = "webexpress.ui:form.selectionmove.selected";

        /// <summary>
        /// Returns or sets the label.
        /// </summary>
        public string AvailableHeader { get; set; } = "webexpress.ui:form.selectionmove.available";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
        public ControlFormItemInputMove(string id = null)
            : base(string.IsNullOrEmpty(id) ? typeof(ControlFormItemInputSelection).GUID.ToString() : id)
        {
            Name = Id;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
        /// <param name="items">The entries.</param>
        public ControlFormItemInputMove(string id, params ControlFormItemInputSelectionItem[] items)
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
                Id = $"selection-move-{Id}",
                Style = GetStyles()
            };

            context.AddScript(Id, GetScript(context, $"selection-move-{Id}", string.Join(" ", classes)));

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
        /// <param name="id">The id of the control.</param>
        /// <param name="css">The css classes that are assigned to the control.</param>
        /// <returns>The javascript code.</returns>
        protected virtual string GetScript(RenderContextFormular context, string id, string css)
        {
            var settings = new
            {
                Id = id,
                Name = Id,
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

            builder.Append($"var options = {optionsJson};");
            builder.Append($"var settings = {settingsJson};");
            builder.Append($"var container = $('#{id}');");
            builder.Append($"var obj = new webexpress.ui.moveCtrl(settings);");
            builder.Append($"obj.options = options;");
            builder.Append($"obj.value = {(!string.IsNullOrWhiteSpace(valuesJson) ? valuesJson : "[]")};");
            builder.Append($"container.replaceWith(obj.getCtrl);");

            return builder.ToString();
        }
    }
}

