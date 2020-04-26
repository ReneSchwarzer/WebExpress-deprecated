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
            Classes.Add("timeline");

            var ul = new HtmlElementTextContentUl(Items.Select(x => new HtmlElementTextContentLi(x.ToHtml()) { Class = "item" }))
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
