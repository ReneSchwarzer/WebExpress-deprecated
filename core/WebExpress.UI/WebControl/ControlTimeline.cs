using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Stellt eine Timline (analog Facebook) bereit
    /// </summary>
    public class ControlTimeline : Control
    {
        /// <summary>
        /// Liefert oder setzt die Timeline-Einträge
        /// </summary>
        public List<ControlTimelineItem> Items { get; protected set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlTimeline(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlTimeline(params ControlTimelineItem[] items)
            : base(null)
        {
            Init();

            Items.AddRange(items);
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Items = new List<ControlTimelineItem>();
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Classes.Add("timeline");

            var ul = new HtmlElementTextContentUl(Items.Select(x => new HtmlElementTextContentLi(x.Render(context)) { Class = "item" }))
            {
                ID = ID,
                Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };

            return ul;
        }
    }
}
