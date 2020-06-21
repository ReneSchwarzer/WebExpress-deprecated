using WebExpress.Html;
using WebExpress.UI.Controls;

namespace WebExpress.UI.Pages
{
    /// <summary>
    /// Seite, die aus einem vertikal angeordneten Kopf-, Inhalt- und Fuss-Bereich besteht
    /// </summary>
    public abstract class PageTemplateWebApp : PageTemplate
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageTemplateWebApp()
            : base()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Init()
        {
            base.Init();

            HamburgerMenu = new ControlHamburgerMenu(this, "hamburger") { };
            Notification = new ControlPanel(this, "notification") { };
            Head = new ControlPanelHeader(this, "head") { };
            ToolBar = new ControlToolBar(this, "toolbar") { Orientation = TypeOrientationToolBar.Vertical, Padding = new PropertySpacingPadding(PropertySpacing.Space.Null) };
            SideBar = new ControlToolBar(this, "sidebar") { Orientation = TypeOrientationToolBar.Horizontal, Padding = new PropertySpacingPadding(PropertySpacing.Space.Null) };
            Foot = new ControlFoot(this);
            Main = new ControlPanelMain(this) { };
            PathCtrl = new ControlBreadcrumb(this);

            Notification.Classes.Add("notification");
            Head.Classes.Add("bg-dark");
            ToolBar.Classes.Add("toolbar");
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Die Seite als HTML-Baum</returns>
        public override IHtmlNode ToHtml()
        {
            if (Notification.Content.Count > 0)
            {
                Content.Add(Notification);
            }

            if (Head.Content.Count > 0)
            {
                Content.Add(Head);
            }

            if (ToolBar.Items.Count > 0)
            {
                Head.Content.Add(ToolBar);
            }

            if (SideBar.Items.Count > 0)
            {
                Content.Add(SideBar);
            }

            Content.Add(PathCtrl);
            Content.Add(Main);
            Content.Add(Foot);

            return base.ToHtml();
        }
    }
}
