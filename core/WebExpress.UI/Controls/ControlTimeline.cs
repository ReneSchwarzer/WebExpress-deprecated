using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
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
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlTimeline(IPage page, string id = null)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlTimeline(IPage page, params ControlTimelineItem[] items)
            : base(page, null)
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
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            var classes = new List<string>
            {
                Class,
                "timeline"
            };
            //classes.Add("list-unstyled");

            var ul = new HtmlElementUl(Items.Select(x => new HtmlElementLi(x.ToHtml()) { Class = "item" }))
            {
                ID = ID,
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = Style,
                Role = Role
            };

            return ul;
        }
    }
}
