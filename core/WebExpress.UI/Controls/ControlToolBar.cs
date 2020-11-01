using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlToolBar : Control
    {
        /// <summary>
        /// Die horizontale Anordnung
        /// </summary>
        public virtual TypeOrientationToolBar Orientation
        {
            get => (TypeOrientationToolBar)GetProperty(TypeOrientationToolBar.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Die fixierte Anordnung
        /// </summary>
        public virtual TypeFixed Fixed
        {
            get => (TypeFixed)GetProperty(TypeFixed.None);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Fixiert die Anordnung, wenn sich die Toolbar am oberen Rand befindet
        /// </summary>
        public virtual TypeSticky Sticky
        {
            get => (TypeSticky)GetProperty(TypeSticky.None);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        public List<IControlToolBarItem> Items { get; private set; } = new List<IControlToolBarItem>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlToolBar(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="items">Die Toolitems</param>
        public ControlToolBar(string id = null, params IControlToolBarItem[] items)
            : this(id)
        {
            Add(items);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="items">Die Toolitems</param>
        public ControlToolBar(params IControlToolBarItem[] items)
            : this((string)null)
        {
            Add(items);
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Classes.Add("navbar");
        }

        /// <summary>
        /// Fügt Einträge hinzu
        /// </summary>
        /// <param name="item">Die Einträge welcher hinzugefügt werden sollen</param>
        public void Add(params IControlToolBarItem[] item)
        {
            Items.AddRange(item);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var html = new HtmlElementSectionNav()
            {
                ID = ID,
                Class = GetClasses(),
                Style = GetStyles(),
                Role = Role
            };

            html.Elements.Add
            (
                new HtmlElementTextContentUl
                (
                    Items.Select
                    (
                        x =>
                        x == null || x is ControlDropdownMenuDivider || x is ControlLine ?
                        new HtmlElementTextContentLi() { Class = "divider", Inline = true } :
                        x is ControlDropdownMenuHeader ?
                        x.Render(context) :
                        new HtmlElementTextContentLi(x.Render(context)) { Class = "nav-item" }
                    )
                )
                {
                    Class = HorizontalAlignment == TypeHorizontalAlignment.Right ? "" : "navbar-nav"
                }
            );

            return html;
        }
    }
}
