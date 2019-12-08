using System.Collections.Generic;
using System.Linq;
using WebExpress.Pages;
using WebServer.Html;

namespace WebExpress.UI.Controls
{
    public class ControlProgressBar : Control
    {
        /// <summary>
        /// Liefert oder setzt das Format des Fortschrittbalkens
        /// </summary>
        public TypesProgressBarFormat Format { get; set; }

        /// <summary>
        /// Liefert oder setzt die Größe
        /// </summary>
        public TypesSize Size { get; set; }

        /// <summary>
        /// Liefert oder setzt die Textfarbe
        /// </summary>
        public TypesTextColor Color { get; set; }

        /// <summary>
        /// Liefert oder setzt das Layout
        /// </summary>
        public TypesLayoutProgressBar Layout { get; set; }

        /// <summary>
        /// Liefert oder setzt die Hintergrundfarbe
        /// </summary>
        public string BackgroundColor { get; set; }

        /// <summary>
        /// Liefert oder setzt den Wert
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Liefert oder setzt den Minimumwert
        /// </summary>
        public int Min { get; set; }

        /// <summary>
        /// Liefert oder setzt dem Maximumwert
        /// </summary>
        public int Max { get; set; }

        /// <summary>
        /// Liefert oder setzt die Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlProgressBar(IPage page, string id = null)
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
        public ControlProgressBar(IPage page, string id, int value)
            : this(page, id)
        {
            Value = value;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="value">Der Wert</param>
        public ControlProgressBar(IPage page, string id, int value, int min = 0, int max = 100)
            : this(page, id)
        {
            Value = value;
            Min = min;
            Max = max;
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Min = 0;
            Max = 100;
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
                    return new HtmlElementProgress(Value + "%")
                    {
                        ID = ID,
                        Class = Class,
                        Style = Style,
                        Role = Role,
                        Min = Min.ToString(),
                        Max = Max.ToString(),
                        Value = Value.ToString()
                    };
            }

            classes.Add("progress");

            var styles = new List<string>
            {
                Style
            };

            var barStyles = new List<string>
            {
                "width: " + Value + "%;"
            };

            switch (Layout)
            {
                case TypesLayoutProgressBar.Primary:
                    barClass.Add("bg-primary");
                    break;
                case TypesLayoutProgressBar.Success:
                    barClass.Add("bg-success");
                    break;
                case TypesLayoutProgressBar.Info:
                    barClass.Add("bg-info");
                    break;
                case TypesLayoutProgressBar.Warning:
                    barClass.Add("bg-warning");
                    break;
                case TypesLayoutProgressBar.Danger:
                    barClass.Add("bg-danger");
                    break;
                case TypesLayoutProgressBar.Light:
                    barClass.Add("bg-light");
                    break;
                case TypesLayoutProgressBar.Dark:
                    barClass.Add("bg-dark");
                    break;
                case TypesLayoutProgressBar.Color:
                    barStyles.Add("background-color: " + BackgroundColor + ";");
                    break;
            }

            switch (Color)
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

            switch (Size)
            {
                case TypesSize.Large:
                    styles.Add("height: 30px;");
                    break;
                case TypesSize.Small:
                    styles.Add("height: 10px;");
                    break;
            }

            var bar = new HtmlElementDiv(new HtmlText(Text))
            {
                ID = ID,
                Class = string.Join(" ", barClass.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join(" ", barStyles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };

            return new HtmlElementDiv(bar)
            {
                ID = ID,
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join(" ", styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };
        }
    }
}
