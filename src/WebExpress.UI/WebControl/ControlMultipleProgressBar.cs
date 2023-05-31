using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlMultipleProgressBar : Control
    {
        /// <summary>
        /// Liefert oder setzt das Format des Fortschrittbalkens
        /// </summary>
        public TypeFormatProgress Format { get; set; }

        /// <summary>
        /// Returns or sets the value.
        /// </summary>
        public List<ControlMultipleProgressBarItem> Items { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
        public ControlMultipleProgressBar(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
        /// <param name="value">The value.</param>
        public ControlMultipleProgressBar(string id, params ControlMultipleProgressBarItem[] items)
            : this(id)
        {
            Items.AddRange(items);
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private void Init()
        {
            Items = new List<ControlMultipleProgressBarItem>();
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var barClass = new List<string>();

            switch (Format)
            {
                case TypeFormatProgress.Colored:
                    barClass.Add("progress-bar");
                    break;

                case TypeFormatProgress.Striped:
                    barClass.Add("progress-bar");
                    barClass.Add("progress-bar-striped");
                    break;

                case TypeFormatProgress.Animated:
                    barClass.Add("progress-bar");
                    barClass.Add("progress-bar-striped");
                    barClass.Add("progress-bar-animated");
                    break;

                default:
                    return new HtmlElementFormProgress(Items.Select(x => x.Value).Sum() + "%")
                    {
                        Id = Id,
                        Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                        Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                        Role = Role,
                        Min = "0",
                        Max = "100",
                        Value = Items.Select(x => x.Value).Sum().ToString()
                    };
            }

            Classes.Add("progress");

            var html = new HtmlElementTextContentDiv()
            {
                Id = Id,
                Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };

            foreach (var v in Items)
            {
                var styles = new List<string>
                {
                    "width: " + v.Value + "%;"
                };

                var c = new List<string>(barClass)
                {
                    v.BackgroundColor.ToClass()
                };

                barClass.Add(v.Color.ToClass());

                var bar = new HtmlElementTextContentDiv(new HtmlText(v.Text))
                {
                    Id = Id,
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
