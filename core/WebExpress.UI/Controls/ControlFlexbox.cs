using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlFlexbox : Control
    {
        /// <summary>
        /// Liefert oder setzt die Listeneinträge
        /// </summary>
        public List<Control> Items { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Anordnung
        /// </summary>
        public TypesFlexboxDirection Direction { get; set; }

        /// <summary>
        /// Bestimmt, ob die Items inline angezeigt werden sollen
        /// </summary>
        public bool Inline { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlFlexbox(IPage page, string id = null)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        /// <param name="items">Die Listeneinträge</param>
        public ControlFlexbox(IPage page, string id, List<Control> items)
            : this(page, id)
        {
            Items = items;
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Items = new List<Control>();
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            Classes.Add(Inline ? "d-inline-flex" : "d-flex");
            Classes.Add(Direction.ToClass());

            var html = new HtmlElementTextContentDiv()
            {
                ID = ID,
                Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };

            html.Elements.AddRange(Items.Select(x => x.ToHtml()));

            return html;
        }
    }
}
