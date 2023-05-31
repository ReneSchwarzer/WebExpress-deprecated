using System.Linq;
using System.Text;
using System.Text.Json;
using WebExpress.UI.WebControl;

namespace WebExpress.WebApp.WebApiControl;

public class ControlApiFormularItemInputSelection : ControlFormItemInputSelection, IControlApi
{
    /// <summary>
    /// Liefert oder setzt die Uri, welche die Optionen ermittelt
    /// </summary>
    public string RestUri { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id">Returns or sets the id.</param>
    public ControlApiFormularItemInputSelection(string id = null)
        : base(id)
    {
    }

    /// <summary>
    /// Generates the javascript to control the control.
    /// </summary>
    /// <param name="context">The context in which the control is rendered.</param>
    /// <param name="id">The ID of the control.</param>
    /// <param name="css">The CSS classes that are assigned to the control.</param>
    /// <returns>The javascript code.</returns>
    protected override string GetScript(RenderContextFormular context, string id, string css)
    {
        var settings = new
        {
            id = id,
            name = Name,
            css = css,
            placeholder = Placeholder,
            multiSelect = MultiSelect,
            optionuri = RestUri?.ToString()
        };

        var jsonOptions = new JsonSerializerOptions { WriteIndented = false };
        var settingsJson = JsonSerializer.Serialize(settings, jsonOptions);
        var optionsJson = JsonSerializer.Serialize(Options, jsonOptions);
        var builder = new StringBuilder();

        builder.AppendLine($"$(document).ready(function () {{");
        builder.AppendLine($"let settings = {settingsJson};");
        builder.AppendLine($"var options = {optionsJson};");
        builder.AppendLine($"let container = $('#{id}');");
        builder.AppendLine($"let obj = new webexpress.webapp.selectionCtrl(settings);");
        builder.AppendLine($"obj.options = options;");
        builder.AppendLine($"obj.receiveData();");
        builder.AppendLine($"obj.value = [{string.Join(",", Values.Select(x => $"'{x}'"))}];");
        builder.AppendLine($"obj.on('webexpress.ui.change.filter', function(key) {{ obj.receiveData(key); }});");
        if (OnChange != null)
        {
            builder.AppendLine($"obj.on('webexpress.ui.change.value', function() {{ {OnChange} }});");
        }
        builder.AppendLine($"container.replaceWith(obj.getCtrl);");
        builder.AppendLine($"}});");

        return builder.ToString();
    }
}
