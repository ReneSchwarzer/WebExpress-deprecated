using WebExpress.UI.Controls;
using WebServer.Html;

namespace WebExpress.UI.Pages
{
    /// <summary>
    /// Seite, die aus einem vertikal angeordneten Kopf-, Inhalt- und Fuss-Bereich besteht
    /// </summary>
    public abstract class PageTemplate : PageBlank
    {
        /// <summary>
        /// Liefert oder setzt den Kopf
        /// </summary>
        public ControlPanel Notification { get; protected set; }

        /// <summary>
        /// Liefert oder setzt den Kopf
        /// </summary>
        public ControlPanel Head { get; protected set; }

        /// <summary>
        /// Liefert oder setzt die ToolBar
        /// </summary>
        public ControlToolBar ToolBar { get; protected set; }

        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        public ControlPanelMain Main { get; protected set; }

        /// <summary>
        /// Liefert oder setzt den Pfad
        /// </summary>
        public ControlBreadcrumb PathCtrl { get; protected set; }

        /// <summary>
        /// Liefert oder setzt den Fuß
        /// </summary>
        public ControlFoot Foot { get; protected set; }

        /// <summary>
        /// Liefert oder setzt das Hamburgermenü
        /// </summary>
        public ControlHamburgerMenu HamburgerMenu { get; protected set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageTemplate()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Init()
        {
            base.Init();
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            Content.Clear();
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Die Seite als HTML-Baum</returns>
        public override IHtmlNode ToHtml()
        {
            PathCtrl.Path = Path;

            return base.ToHtml();
        }
    }
}
