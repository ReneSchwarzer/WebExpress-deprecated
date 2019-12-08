using WebExpress.UI.Controls;

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
        /// In String konvertieren
        /// </summary>
        /// <returns>Das Objekt als String</returns>
        public override string ToString()
        {
            PathCtrl.Path = Path;

            return base.ToString();
        }
    }
}
