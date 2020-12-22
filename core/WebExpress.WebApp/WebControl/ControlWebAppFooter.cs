using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebControl;

namespace WebExpress.WebApp.WebControl
{
    /// <summary>
    /// Footer für eine WebApp
    /// </summary>
    public class ControlWebAppFooter : Control
    {
        /// <summary>
        /// Liefert oder setzt den Bereich für die Kopfzeile der Sidebar
        /// </summary>
        public ControlPanel Header { get; protected set; } = new ControlPanel("sidebarheader");

        /// <summary>
        /// Liefert oder setzt den den Bereich für Präferenzen
        /// </summary>
        public List<IControl> Preferences { get; protected set; } = new List<IControl>();

        /// <summary>
        /// Liefert oder setzt den den Primärbereich für die Steuerelemente
        /// </summary>
        public List<IControl> Primary { get; protected set; } = new List<IControl>();

        /// <summary>
        /// Liefert oder setzt den den sekundären Bereich für die Steuerelemente
        /// </summary>
        public List<IControl> Secondary { get; protected set; } = new List<IControl>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlWebAppFooter(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            BackgroundColor = LayoutSchema.FooterBackground;

            //Navigation.Layout = TypeLayoutTab.Pill;
            //Navigation.Orientation = TypeOrientationTab.Vertical;
            //Navigation.GridColumn = new PropertyGrid(TypeDevice.Medium, 2);

            //Navigation.ActiveColor = LayoutSchema.SidebarNavigationActiveBackground;
            //Navigation.ActiveTextColor = LayoutSchema.SidebarNavigationActive;
            //Navigation.LinkColor = LayoutSchema.SidebarNavigationLink;
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            if (Header.Content.Count == 0 && Preferences.Count == 0 && Primary.Count == 0 && Secondary.Count == 0)
            {
                return null;
            }

            var elements = new List<IHtmlNode>
            {
                Header.Render(context)
            };
            elements.AddRange(Preferences.Select(x => x.Render(context)));
            elements.AddRange(Primary.Select(x => x.Render(context)));
            elements.AddRange(Secondary.Select(x => x.Render(context)));


            return new HtmlElementTextContentDiv(elements)
            {
                ID = ID,
                Class = Css.Concatenate("navbar", GetClasses()),
                Style = Style.Concatenate("display: block;", GetStyles()),
                Role = Role
            };
        }
    }
}
