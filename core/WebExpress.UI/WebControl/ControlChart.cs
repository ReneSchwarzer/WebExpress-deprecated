using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebExpress.Html;
using WebExpress.Module;
using WebExpress.Uri;

namespace WebExpress.UI.WebControl
{
    public class ControlChart : Control
    {
        /// <summary>
        /// Liefert oder setzt den Typ
        /// </summary>
        public TypeChart Type { get; set; }

        /// <summary>
        /// Liefert oder setzt den Titel
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Liefert oder setzt den Titel der X-Achse
        /// </summary>
        public string TitleX { get; set; }

        /// <summary>
        /// Liefert oder setzt den Titel der Y-Achse
        /// </summary>
        public string TitleY { get; set; }

        /// <summary>
        /// Liefert oder setzt die Daten
        /// </summary>
        public ICollection<string> Labels { get; set; } = new List<string>();

        /// <summary>
        /// Liefert oder setzt die Weite
        /// </summary>
        public new int Width { get; set; }

        /// <summary>
        /// Liefert oder setzt die Höhe
        /// </summary>
        public new int Height { get; set; }

        /// <summary>
        /// Liefert oder setzt die Daten
        /// </summary>
        public ICollection<ControlChartDataset> Data { get; set; } = new List<ControlChartDataset>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlChart(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Initialisiert das Formularelement
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public void Initialize(RenderContext context)
        {
            var module = ModuleManager.GetModule("webexpress");
            if (module != null)
            {
                context.Page.HeaderScriptLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/js/Chart.min.js")));
                context.Page.CssLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/css/Chart.min.css")));
            }

            var builder = new StringBuilder();
            var data = new List<StringBuilder>();
            builder.Append($"var config_{ ID } = {{");
            //if (Type != TypeChart.Polar)
            {
                builder.Append($"type:'{ Type.ToType() }',");
            }
            builder.Append("data:{");
            builder.Append($"labels:[{ string.Join(",", Labels.Select(x => $"'{ x }'"))}],");
            builder.Append("datasets:[{");
            foreach (var v in Data)
            {
                var buf = new StringBuilder();

                buf.Append($"label:'{ v.Title }',");
                buf.Append($"backgroundColor:{ (v.BackgroundColor.Count <= 1 ? v.BackgroundColor.Select(x => $"'{ x }'").FirstOrDefault()?.ToString() : $"[ { string.Join(",", v.BackgroundColor.Select(x => $"'{ x }'"))} ]")},");
                buf.Append($"borderColor:{ (v.BorderColor.Count <= 1 ? v.BorderColor.Select(x => $"'{ x }'").FirstOrDefault()?.ToString() : $"[ { string.Join(",", v.BorderColor.Select(x => $"'{ x }'"))} ]")},");
                buf.Append($"data:[");
                buf.Append(string.Join(",", v.Data));
                buf.Append($"],");
                if (Type == TypeChart.Line)
                {
                    buf.Append($"fill:'{ v.Fill.ToType() }',");
                    buf.Append($"pointStyle:'{ v.Point.ToType() }'");
                }
                data.Add(buf);
            }
            builder.Append(string.Join("},{", data));
            builder.Append("}]");
            builder.Append("},");
            builder.Append("options:{");
            builder.Append("responsive:true,");
            builder.Append($"title:{{display:true,text:'{ Title }'}},");
            builder.Append("tooltips:{mode:'index',intersect:false},");
            builder.Append("hover:{mode:'nearest',intersect:true},");
            if (Type == TypeChart.Line || Type == TypeChart.Bar)
            {
                builder.Append($"scales:{{xAxes:[{{display: true,scaleLabel:{{display:true,labelString:'{ TitleX }'}}}}],yAxes:[{{display:true,scaleLabel:{{display:true,labelString:'{ TitleY }'}}}}]}}");
            }
            builder.Append("}};");

            builder.AppendLine($"var chart_{ ID } = new Chart(document.getElementById('{ ID }').getContext('2d'), config_{ ID });");

            context.Page.AddScript($"chart_{ ID }", builder.ToString());
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Initialize(context);

            var html = new HtmlElementScriptingCanvas()
            {
                ID = ID,
                Class = Css.Concatenate("", GetClasses()),
                Style = GetStyles(),
                Role = Role
            };

            if (Width > 0)
            {
                html.Width = Width;
                html.Style = Css.Concatenate($"width: { Width }px;", html.Style);
            }

            if (Height > 0)
            {
                html.Height = Height;
                html.Style = Css.Concatenate($"height: { Height }px;", html.Style);
            }

            return new HtmlElementTextContentDiv(html) { Class = "chart-container" };
        }
    }
}
