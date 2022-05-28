using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebApiControl
{
    public class ControlApiTable : ControlPanel, IControlApi
    {
        /// <summary>
        /// Liefert oder setzt die Uri, welche die Daten ermittelt
        /// </summary>
        public IUri RestUri { get; set; }

        /// <summary>
        /// Setzt oder liefert die Einstellungen für die Bearbeitungsoptionen (z.B. Edit, Delete, ...)
        /// </summary>
        public ControlApiTableOption OptionSettings { get; private set; } = new ControlApiTableOption();

        /// <summary>
        /// Setzt oder liefert die Bearbeitungsoptionen (z.B. Edit, Delete, ...)
        /// </summary>
        public ICollection<ControlApiTableOptionItem> OptionItems { get; private set; } = new List<ControlApiTableOptionItem>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlApiTable(string id = null)
            : base(id ?? Guid.NewGuid().ToString())
        {
        }

        /// <summary>
        /// Erzeugt das Javascript zur Ansteuerung des Steuerelements
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <param name="id">Die ID des Steuerelmentes</param>
        /// <param name="css">Die CSS-KLassen, die dem Steuerelement zugewiesen werden</param>
        /// <returns>Der Javascript-Code</returns>
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
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var classes = Classes.ToList();

            var html = new HtmlElementTextContentDiv()
            {
                ID = ID,
                Style = GetStyles()
            };

            context.VisualTree.AddScript(ID, GetScript(context, ID, string.Join(" ", classes)));

            return html;
        }
    }
}
