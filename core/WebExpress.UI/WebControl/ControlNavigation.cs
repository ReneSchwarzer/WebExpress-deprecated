using System.Collections.Generic;
using WebExpress.Html;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlNavigation : Control
    {
        /// <summary>
        /// Liefert oder setzt das Layout
        /// </summary>
        public TypeLayoutTab Layout
        {
            get => (TypeLayoutTab)GetProperty(TypeLayoutTab.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Die horizontale Anordnung
        /// </summary>
        public new TypeHorizontalAlignmentTab HorizontalAlignment
        {
            get => (TypeHorizontalAlignmentTab)GetProperty(TypeHorizontalAlignmentTab.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Liefert oder setzt ob die Tab-Register die gleiche Größe besitzen sollen
        /// </summary>
        public TypeJustifiedTab Justified
        {
            get => (TypeJustifiedTab)GetProperty(TypeJustifiedTab.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Liefert oder setztdie horizontale oder vertikale Ausrichtung
        /// </summary>
        public TypeOrientationTab Orientation
        {
            get => (TypeOrientationTab)GetProperty(TypeOrientationTab.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Liefert oder setzt die aktive Farbe
        /// </summary>
        public PropertyColorBackground ActiveColor { get; set; } = new PropertyColorBackground();

        /// <summary>
        /// Liefert oder setzt die aktive Farbe
        /// </summary>
        public PropertyColorText ActiveTextColor { get; set; } = new PropertyColorText();

        /// <summary>
        /// Liefert oder setzt die normale Farbe
        /// </summary>
        public PropertyColorText LinkColor { get; set; } = new PropertyColorText();

        /// <summary>
        /// Liefert oder setzt die Listeneinträge
        /// </summary>
        public List<IControlNavigationItem> Items { get; private set; } = new List<IControlNavigationItem>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlNavigation(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="items">Die Linkelemente</param>
        public ControlNavigation(params IControlNavigationItem[] items)
            : base(null)
        {
            Items.AddRange(items);

            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="content">Der Inhalt</param>
        public ControlNavigation(string id, IEnumerable<IControlNavigationItem> content)
            : base(id)
        {
            Items.AddRange(content);

            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="content">Der Inhalt</param>
        public ControlNavigation(IEnumerable<IControlNavigationItem> content)
            : base(null)
        {
            Items.AddRange(content);

            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            //ActiveColor = LayoutSchema.NavigationActiveBackground;
            //ActiveTextColor = LayoutSchema.NavigationActive;
            //LinkColor = LayoutSchema.NavigationLink;
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var items = new List<HtmlElement>();
            foreach (var item in Items)
            {
                var i = item.Render(context) as HtmlElement;

                if (item is ControlNavigationItemLink link)
                {
                    i.RemoveClass(link.TextColor?.ToClass());
                    i.RemoveStyle(link.TextColor?.ToStyle());

                    i.AddClass
                    (
                        Css.Concatenate
                        (
                            "nav-link",
                            link.Active == TypeActive.Active ? ActiveColor?.ToClass() : "",
                            link.Active == TypeActive.Active ? ActiveTextColor?.ToClass() : LinkColor?.ToClass()
                        )
                    );

                    i.AddStyle
                    (
                        Style.Concatenate
                        (
                            link.Active == TypeActive.Active ? ActiveColor?.ToStyle() : "",
                            link.Active == TypeActive.Active ? ActiveTextColor?.ToStyle() : LinkColor?.ToStyle()
                        )
                    );


                }
                else if (item is ControlNavigationItemDropdown dropdown)
                {
                    i.RemoveClass(dropdown.TextColor?.ToClass());
                    i.RemoveStyle(dropdown.TextColor?.ToStyle());

                    i.AddClass
                    (
                        Css.Concatenate
                        (
                            "nav-link",
                            dropdown.Active == TypeActive.Active ? ActiveColor?.ToClass() : "",
                            dropdown.Active == TypeActive.Active ? ActiveTextColor?.ToClass() : ""
                        )
                    );
                    i.AddStyle
                    (
                        Style.Concatenate
                        (
                            dropdown.Active == TypeActive.Active ? ActiveColor?.ToStyle() : "",
                            dropdown.Active == TypeActive.Active ? ActiveTextColor?.ToStyle() : ""
                        )
                    );
                }
                else
                {
                    i.AddClass(Css.Concatenate("nav-link"));
                }

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
