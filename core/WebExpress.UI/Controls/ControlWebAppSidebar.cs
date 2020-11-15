using WebExpress.Html;

namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Header für eine WenApp
    /// </summary>
    public class ControlWebAppSidebar : Control
    {
        /// <summary>
        /// Liefert oder setzt den Bereich für die Kopfzeile der Sidebar
        /// </summary>
        public ControlPanel Header { get; protected set; } = new ControlPanel("sidebarheader");

        /// <summary>
        /// Liefert oder setzt den Bereich in der Sidebar für erweiterte Inhalte
        /// </summary>
        public ControlPanel Content { get; protected set; } = new ControlPanel("sidebarcontent");

        /// <summary>
        /// Liefert oder setzt den Navigationsbereich
        /// </summary>
        public ControlNavigation Navigation { get; protected set; } = new ControlNavigation("sidebarnavigation");

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlWebAppSidebar(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            BackgroundColor = LayoutSchema.SidebarBackground;

            Navigation.Layout = TypeLayoutTab.Pill;
            Navigation.Orientation = TypeOrientationTab.Vertical;
            Navigation.GridColumn = new PropertyGrid(TypeDevice.Medium, 2);
            
            Navigation.ActiveColor = LayoutSchema.SidebarNavigationActiveBackground;
            Navigation.ActiveTextColor = LayoutSchema.SidebarNavigationActive;
            Navigation.LinkColor = LayoutSchema.SidebarNavigationLink;
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            if (Header.Content.Count == 0 && Navigation.Items.Count == 0 && Content.Content.Count == 0)
            {
                return null;
            }

            return new HtmlElementTextContentDiv(Header.Render(context), Navigation.Render(context), Content.Render(context))
            {
                ID = ID,
                Class = Css.Concatenate("navbar", GetClasses()),
                Style = Style.Concatenate("display: block;", GetStyles()),
                Role = Role
            };
        }
    }
}
