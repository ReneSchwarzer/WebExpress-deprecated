using System.Collections.Generic;
using System.Linq;
using WebExpress.WebHtml;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlToolbar : Control
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
        /// Returns or sets the content.
        /// </summary>
        public List<IControlToolBarItem> Items { get; private set; } = new List<IControlToolBarItem>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlToolbar(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="items">Die Toolitems</param>
        public ControlToolbar(string id = null, params IControlToolBarItem[] items)
            : this(id)
        {
            Add(items);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="items">Die Toolitems</param>
        public ControlToolbar(params IControlToolBarItem[] items)
            : this((string)null)
        {
            Add(items);
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private void Init()
        {
            BackgroundColor = new PropertyColorBackground(TypeColorBackground.Warning);
            Orientation = TypeOrientationToolBar.Default;
            Padding = new PropertySpacingPadding(PropertySpacing.Space.Two, PropertySpacing.Space.None);
        }

        /// <summary>
        /// Fügt Einträge hinzu
        /// </summary>
        /// <param name="item">The entries. welcher hinzugefügt werden sollen</param>
        public void Add(params IControlToolBarItem[] item)
        {
            Items.AddRange(item);
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var html = new HtmlElementSectionNav()
            {
                Id = Id,
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
                        x == null || x is ControlDropdownItemDivider || x is ControlLine ?
                        new HtmlElementTextContentLi() { Class = "divider", Inline = true } :
                        x is ControlDropdownItemHeader ?
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
