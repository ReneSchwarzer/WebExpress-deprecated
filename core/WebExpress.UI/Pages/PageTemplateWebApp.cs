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
            Notification = new ControlPanel(this, "notification") { Class = "notification" };
            Head = new ControlPanelHeader(this, "head") { Class = "bg-dark", Role = "" };
            ToolBar = new ControlToolBar(this, "toolbar") { Class = "toolbar" };
            Foot = new ControlFoot(this);
            Main = new ControlPanelMain(this) { Class = "" };
            PathCtrl = new ControlBreadcrumb(this);
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
                Content.Add(ToolBar);
            }

            Content.Add(PathCtrl);
            Content.Add(Main);
            Content.Add(Foot);

            return base.ToHtml();
        }
    }
}
