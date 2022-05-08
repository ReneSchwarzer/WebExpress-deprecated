using System.Text;
using System.Text.Json;
using WebExpress.UI.WebControl;
using WebExpress.Uri;

namespace WebExpress.WebApp.WebControlRest
{
    public class ControlFormularItemInputSelectionRest : ControlFormularItemInputSelection, IControlRest
    {
        /// <summary>
        /// Liefert oder setzt die Uri, welche die Optionen ermittelt
        /// </summary>
        public IUri RestUri { get; set; }


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularItemInputSelectionRest(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Erzeugt das Javascript zur Ansteuerung des Steuerelements
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <param name="id">Die ID des Steuerelmentes</param>
        /// <param name="css">Die CSS-KLassen, die dem Steuerelement zugewiesen werden</param>
        /// <returns>Der Javascript-Code</returns>
        protected override string GetScript(RenderContextFormular context, string id, string css)
        {
            var settings = new
            {
                ID = id,
                Name,
                CSS = css,
                Placeholder,
                MultiSelect,
                OptionUri = RestUri?.ToString()
            };

            var jsonOptions = new JsonSerializerOptions { WriteIndented = false };
            var settingsJson = JsonSerializer.Serialize(settings, jsonOptions);
            var optionsJson = JsonSerializer.Serialize(Options, jsonOptions);
            var builder = new StringBuilder();
            
            builder.AppendLine($"{{");
            builder.AppendLine($"let settings = { settingsJson };");
            builder.AppendLine($"var options = { optionsJson };");
            builder.AppendLine($"let container = $('#{ id }');");
            builder.AppendLine($"let obj = new restSelectionCtrl(settings);");
            builder.AppendLine($"obj.options = options;");
            builder.AppendLine($"obj.receiveData();");
            builder.AppendLine($"obj.value = ['{ Value }'];");
            builder.AppendLine($"obj.on('webexpress.ui.change.filter', function(key) {{ obj.receiveData(key); }});");
            if (OnChange != null)
            {
                builder.AppendLine($"obj.on('webexpress.ui.change.value', function() {{ { OnChange } }});");
            }
            builder.AppendLine($"container.replaceWith(obj.getCtrl);");
            builder.AppendLine($"}}");

            return builder.ToString();            
        }
    }
}
