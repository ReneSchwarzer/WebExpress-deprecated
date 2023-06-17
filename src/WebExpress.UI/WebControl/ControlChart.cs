using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using WebExpress.Html;
using WebExpress.WebComponent;
using WebExpress.WebPage;
using WebExpress.WebUri;

namespace WebExpress.UI.WebControl
{
    public class ControlChart : Control
    {
        /// <summary>
        /// Returns or sets the type.
        /// </summary>
        public TypeChart Type { get; set; }

        /// <summary>
        /// Returns or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Returns or sets the title of the x-axis.
        /// </summary>
        public string TitleX { get; set; }

        /// <summary>
        /// Returns or sets the title of the y-axis.
        /// </summary>
        public string TitleY { get; set; }

        /// <summary>
        /// Returns or sets the data.
        /// </summary>
        public ICollection<string> Labels { get; set; } = new List<string>();

        /// <summary>
        /// Returns or sets the width.
        /// </summary>
        public new int Width { get; set; }

        /// <summary>
        /// Returns or sets the height.
        /// </summary>
        public new int Height { get; set; }

        /// <summary>
        /// Returns or sets the minimum.
        /// </summary>
        public float Minimum { get; set; } = float.MinValue;

        /// <summary>
        /// Returns or sets the maximum.
        /// </summary>
        public float Maximum { get; set; } = float.MaxValue;

        /// <summary>
        /// Returns or sets the data.
        /// </summary>
        public ICollection<ControlChartDataset> Data { get; set; } = new List<ControlChartDataset>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlChart(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Initializes the form element.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        public void Initialize(RenderContext context)
        {
            var module = ComponentManager.ModuleManager.GetModule(context.ApplicationContext, typeof(Module));
            if (module != null)
            {
                context.VisualTree.HeaderScriptLinks.Add(UriResource.Combine(module.ContextPath, "/assets/js/Chart.min.js"));
                context.VisualTree.CssLinks.Add(UriResource.Combine(module.ContextPath, "/assets/css/Chart.min.css"));
            }

            var builder = new StringBuilder();
            var data = new List<StringBuilder>();
            builder.Append($"var config_{Id} = {{");
            //if (Type != TypeChart.Polar)
            {
                builder.Append($"type:'{Type.ToType()}',");
            }
            builder.Append("data:{");
            builder.Append($"labels:[{string.Join(",", Labels.Select(x => $"'{x}'"))}],");
            builder.Append("datasets:[{");
            foreach (var v in Data)
            {
                var buf = new StringBuilder();

                buf.Append($"label:'{v.Title}',");
                buf.Append($"backgroundColor:{(v.BackgroundColor.Count <= 1 ? v.BackgroundColor.Select(x => $"'{x}'").FirstOrDefault()?.ToString() : $"[ {string.Join(",", v.BackgroundColor.Select(x => $"'{x}'"))} ]")},");
                buf.Append($"borderColor:{(v.BorderColor.Count <= 1 ? v.BorderColor.Select(x => $"'{x}'").FirstOrDefault()?.ToString() : $"[ {string.Join(",", v.BorderColor.Select(x => $"'{x}'"))} ]")},");
                buf.Append($"data:[");
                if (v.Data != null)
                {
                    buf.Append(string.Join(",", v.Data.Select(x => x.ToString(CultureInfo.InvariantCulture))));
                }
                buf.Append($"],");
                if (Type == TypeChart.Line)
                {
                    buf.Append($"fill:'{v.Fill.ToType()}',");
                    buf.Append($"pointStyle:'{v.Point.ToType()}'");
                }
                data.Add(buf);
            }
            builder.Append(string.Join("},{", data));
            builder.Append("}]");
            builder.Append("},");
            builder.Append("options:{");
            builder.Append("responsive:true,");
            builder.Append($"title:{{display:{(string.IsNullOrWhiteSpace(Title) ? "false" : "true")},text:'{Title}'}},");
            builder.Append("tooltips:{mode:'index',intersect:false},");
            builder.Append("hover:{mode:'nearest',intersect:true},");
            if (Type == TypeChart.Line || Type == TypeChart.Bar)
            {
                builder.Append($"scales:{{");
                builder.Append($"xAxes:[{{display: true,scaleLabel:{{display:true,labelString:'{TitleX}'}}}}],");
                builder.Append($"yAxes:[{{display:true,ticks:{{{(Minimum != float.MinValue ? $"min:{Minimum},suggestedMin:{Minimum}," : "")}{(Maximum != float.MaxValue ? $"max:{Maximum},suggestedMax:{Maximum}," : "")}}},scaleLabel:{{display:true,labelString:'{TitleY}'}}}}]");
                builder.Append($"}}");
            }
            builder.Append("}};");

            builder.AppendLine($"var chart_{Id} = new Chart(document.getElementById('{Id}').getContext('2d'), config_{Id});");

            context.VisualTree.AddScript($"chart_{Id}", builder.ToString());
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Initialize(context);

            var html = new HtmlElementScriptingCanvas()
            {
                Id = Id,
                Class = Css.Concatenate("", GetClasses()),
                Style = GetStyles(),
                Role = Role
            };

            if (Width > 0)
            {
                html.Width = Width;
                html.Style = Css.Concatenate($"width: {Width}px;", html.Style);
            }

            if (Height > 0)
            {
                html.Height = Height;
                html.Style = Css.Concatenate($"height: {Height}px;", html.Style);
            }

            return new HtmlElementTextContentDiv(html) { Class = "chart-container" };
        }
    }
}
