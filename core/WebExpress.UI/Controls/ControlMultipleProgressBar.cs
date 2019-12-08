using System.Collections.Generic;
using System.Linq;
using WebExpress.Pages;
using WebServer.Html;

namespace WebExpress.UI.Controls
{
    public class ControlMultipleProgressBar : Control
    {
        /// <summary>
        /// Liefert oder setzt das Format des Fortschrittbalkens
        /// </summary>
        public TypesProgressBarFormat Format { get; set; }

        /// <summary>
        /// Liefert oder setzt den Wert
        /// </summary>
        public List<ControlMultipleProgressBarItem> Items { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlMultipleProgressBar(IPage page, string id = null)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="value">Der Wert</param>
        public ControlMultipleProgressBar(IPage page, string id, params ControlMultipleProgressBarItem[] items)
            : this(page, id)
        {
            Items.AddRange(items);
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Items = new List<ControlMultipleProgressBarItem>();
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            var classes = new List<string>
            {
                Class
            };

            var barClass = new List<string>();

            switch (Format)
            {
                case TypesProgressBarFormat.Colored:
                    barClass.Add("progress-bar");
                    break;

                case TypesProgressBarFormat.Striped:
                    barClass.Add("progress-bar");
                    barClass.Add("progress-bar-striped");
                    break;

                case TypesProgressBarFormat.Animated:
                    barClass.Add("progress-bar");
                    barClass.Add("progress-bar-striped");
                    barClass.Add("progress-bar-animated");
                    break;

                default:
                    return new HtmlElementProgress(Items.Select(x => x.Value).Sum() + "%")
                    {
                        ID = ID,
                        Class = Class,
                        Style = Style,
                        Role = Role,
                        Min = "0",
                        Max = "100",
                        Value = Items.Select(x => x.Value).Sum().ToString()
                    };
            }

            classes.Add("progress");

            var html = new HtmlElementDiv()
            {
                ID = ID,
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = Style,
                Role = Role
            };

            foreach (var v in Items)
            {
                var styles = new List<string>
                {
                    "width: " + v.Value + "%;"
                };

                var c = new List<string>(barClass);

                switch (v.Layout)
                {
                    case TypesLayoutProgressBar.Primary:
                        c.Add("bg-primary");
                        break;
                    case TypesLayoutProgressBar.Success:
                        c.Add("bg-success");
                        break;
                    case TypesLayoutProgressBar.Info:
                        c.Add("bg-info");
                        break;
                    case TypesLayoutProgressBar.Warning:
                        c.Add("bg-warning");
                        break;
                    case TypesLayoutProgressBar.Danger:
                        c.Add("bg-danger");
                        break;
                    case TypesLayoutProgressBar.Light:
                        c.Add("bg-light");
                        break;
                    case TypesLayoutProgressBar.Dark:
                        c.Add("bg-dark");
                        break;
                    case TypesLayoutProgressBar.Color:
                        c.Add("background-color: " + v.BackgroundColor + ";");
                        break;
                }

                switch (v.Color)
                {
                    case TypesTextColor.Muted:
                        barClass.Add("text-muted");
                        break;
                    case TypesTextColor.Primary:
                        barClass.Add("text-primary");
                        break;
                    case TypesTextColor.Success:
                        barClass.Add("text-success");
                        break;
                    case TypesTextColor.Info:
                        barClass.Add("text-info");
                        break;
                    case TypesTextColor.Warning:
                        barClass.Add("text-warning");
                        break;
                    case TypesTextColor.Danger:
                        barClass.Add("text-danger");
                        break;
                    case TypesTextColor.Light:
                        barClass.Add("text-light");
                        break;
                    case TypesTextColor.Dark:
                        barClass.Add("text-dark");
                        break;
                    case TypesTextColor.White:
                        barClass.Add("text-white");
                        break;
                }

                var bar = new HtmlElementDiv(new HtmlText(v.Text))
                {
                    ID = ID,
                    Class = string.Join(" ", c.Where(x => !string.IsNullOrWhiteSpace(x))),
                    Style = string.Join(" ", styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                    Role = Role
                };

                html.Elements.Add(bar);
            }

            return html;
        }
    }
}
