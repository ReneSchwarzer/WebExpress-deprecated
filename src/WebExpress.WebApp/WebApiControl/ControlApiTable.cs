using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebApiControl
{
    public class ControlApiTable : ControlPanel, IControlApi
    {
        /// <summary>
        /// Liefert oder setzt die Uri, welche die Daten ermittelt
        /// </summary>
        public string RestUri { get; set; }

        /// <summary>
        /// Setzt oder liefert die Einstellungen für die Bearbeitungsoptionen (z.B. Edit, Delete, ...)
        /// </summary>
        public ControlApiTableOption OptionSettings { get; private set; } = new ControlApiTableOption();

        /// <summary>
        /// Setzt oder liefert die Bearbeitungsoptionen (z.B. Edit, Delete, ...)
        /// </summary>
        public ICollection<ControlApiTableOptionItem> OptionItems { get; private set; } = new List<ControlApiTableOptionItem>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlApiTable(string id = null)
            : base(id ?? Guid.NewGuid().ToString())
        {
        }

        /// <summary>
        /// Generates the javascript to control the control.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <param name="id">The id of the control.</param>
        /// <param name="css">The css classes that are assigned to the control.</param>
        /// <returns>The javascript code.</returns>
        protected virtual string GetScript(RenderContext context, string id, string css)
        {
            var settings = new
            {
                id = id,
                css = css,
                resturi = RestUri?.ToString(),
                optionsettings = OptionSettings,
                optionitems = OptionItems
            };

            var jsonOptions = new JsonSerializerOptions { WriteIndented = false };
            var settingsJson = JsonSerializer.Serialize(settings, jsonOptions);
            var builder = new StringBuilder();
            builder.AppendLine($"$(document).ready(function () {{");
            builder.AppendLine($"let settings = {settingsJson};");
            builder.AppendLine($"let container = $('#{id}');");
            builder.AppendLine($"let obj = new webexpress.webapp.tableCtrl(settings);");
            builder.AppendLine($"obj.on('webexpress.ui.change.columns', function() {{ obj.receiveData(); }});");
            builder.AppendLine($"container.replaceWith(obj.getCtrl);");
            builder.AppendLine($"}});");

            return builder.ToString();
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var classes = Classes.ToList();

            var html = new HtmlElementTextContentDiv()
            {
                Id = Id,
                Style = GetStyles()
            };

            context.VisualTree.AddScript(Id, GetScript(context, Id, string.Join(" ", classes)));

            return html;
        }
    }
}
