using System.Collections.Generic;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlTab : Control
    {
        /// <summary>
        /// Liefert oder setzt das Layout
        /// </summary>
        public TypesLayoutTab Layout
        {
            get => (TypesLayoutTab)GetProperty(TypesLayoutTab.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Die horizontale Anordnung
        /// </summary>
        public new TypesTabHorizontalAlignment HorizontalAlignment
        {
            get => (TypesTabHorizontalAlignment)GetProperty(TypesTabHorizontalAlignment.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Liefert oder setzt ob die Tab-Register die gleiche Größe besitzen sollen
        /// </summary>
        public TypesNavJustified Justified
        {
            get => (TypesNavJustified)GetProperty(TypesNavJustified.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Liefert oder setztdie horizontale oder vertikale Ausrichtung
        /// </summary>
        public TypesNavOrientation Orientation
        {
            get => (TypesNavOrientation)GetProperty(TypesNavOrientation.Default);
            set => SetProperty(value, () => value.ToClass());
        }

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
            var items = new List<HtmlElement>();
            foreach (var item in Items)
            {
                var i = item.ToHtml() as HtmlElement;
                i.AddClass("nav-link");

                items.Add(new HtmlElementTextContentLi(i)
                {
                    Class = "nav-item"
                });
            }

            var html = new HtmlElementTextContentUl(items)
            {
                ID = ID,
                Class = Css.Concatenate("nav", GetClasses()),
                Style = GetStyles(),
                Role = Role
            };

            return html;
        }
    }
}
