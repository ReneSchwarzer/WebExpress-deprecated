using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlTab : Control
    {
        /// <summary>
        /// Liefert oder setzt das Layout
        /// </summary>
        public TypesLayoutTab Layout { get; set; }

        /// <summary>
        /// Die horizontale Anordnung
        /// </summary>
        public new TypesTabHorizontalAlignment HorizontalAlignment { get; set; }

        /// <summary>
        /// Liefert oder setzt ob die Tab-Register die gleiche Größe besitzen sollen
        /// </summary>
        public bool Justified { get; set; }

        /// <summary>
        /// Liefert oder setzt die Listeneinträge
        /// </summary>
        public List<ControlLink> Items { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlTab(IPage page, string id = null)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Items = new List<ControlLink>();
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
                "nav"
            };

            switch (HorizontalAlignment)
            {
                case TypesTabHorizontalAlignment.Center:
                    classes.Add("justify-content-center");
                    break;
                case TypesTabHorizontalAlignment.Right:
                    classes.Add("justify-content-end");
                    break;
            }

            switch (Layout)
            {
                case TypesLayoutTab.Tab:
                    classes.Add("nav-tabs");
                    break;
                case TypesLayoutTab.Pill:
                    classes.Add("nav-pills");
                    break;
            }

            if (Justified)
            {
                classes.Add("nav-justified");
            }

            var items = new List<HtmlElement>();
            foreach (var item in Items)
            {
                var i = item.ToHtml() as HtmlElement;
                i.AddClass("nav-link");

                items.Add(new HtmlElementLi(i) { Class = "nav-item" });
            }

            var html = new HtmlElementUl(items)
            {
                ID = ID,
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = Style,
                Role = Role
            };

            return html;
        }
    }
}
