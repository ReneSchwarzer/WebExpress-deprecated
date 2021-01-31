using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebResource;

namespace WebExpress.WebApp.WebControl
{
    /// <summary>
    /// Inhalt einer WebApp-Seite
    /// </summary>
    public class ControlWebAppContent : Control
    {
        /// <summary>
        /// Liefert oder setzt den Seiteneigenschaften
        /// </summary>
        public ControlWebAppProperty Property { get; } = new ControlWebAppProperty("property");

        /// <summary>
        /// Liefert oder setzt den Werkzeugleiste
        /// </summary>
        public ControlToolbar Toolbar { get; } = new ControlToolbar("toolbar");
        
        /// <summary>
        /// Liefert oder setzt das Überschriftssteuerelement
        /// </summary>
        public ControlWebAppHeadline Headline { get; } = new ControlWebAppHeadline("headline");

        /// <summary>
        /// Liefert oder setzt den den Bereich für Präferenzen
        /// </summary>
        public List<IControl> Preferences { get; } = new List<IControl>();

        /// <summary>
        /// Liefert oder setzt den den Primärbereich für die Steuerelemente
        /// </summary>
        public List<IControl> Primary { get; } = new List<IControl>();

        /// <summary>
        /// Liefert oder setzt den den sekundären Bereich für die Steuerelemente
        /// </summary>
        public List<IControl> Secondary { get; } = new List<IControl>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlWebAppContent(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            BackgroundColor = LayoutSchema.ContentBackground;
            Toolbar.BackgroundColor = LayoutSchema.ToolbarBackground;
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var panel = new ControlPanel
            (
                Headline
            )
            {
                BackgroundColor = LayoutSchema.ContentBackground,
                Width = TypeWidth.OneHundred
            };

            panel.Content.AddRange(Preferences);
            panel.Content.AddRange(Primary);
            panel.Content.AddRange(Secondary);

            var flexbox = new ControlPanelFlexbox(panel, Property)
            {
                Layout = TypeLayoutFlexbox.Default,
                Align = TypeAlignFlexbox.Stretch,
                Height = TypeHeight.OneHundred
            };

            var elements = new List<IHtmlNode>
            {
                Toolbar.Render(context),
                flexbox.Render(context)
            };

            return new HtmlElementTextContentDiv(elements)
            {
                ID = ID,
                Class = Css.Concatenate("content", GetClasses()),
                Style = Style.Concatenate("display: block;", GetStyles()),
                Role = Role
            };
        }
    }
}
