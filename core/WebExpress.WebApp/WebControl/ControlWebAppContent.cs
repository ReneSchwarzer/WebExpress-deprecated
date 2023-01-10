using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebControl
{
    /// <summary>
    /// Inhalt einer WebApp-Seite
    /// </summary>
    public class ControlWebAppContent : ControlPanel
    {
        /// <summary>
        /// Liefert das Mainpanel
        /// </summary>
        private ControlPanelMain MainPanel { get; } = new ControlPanelMain("webexpress.webapp.content.main")
        {
            Padding = new PropertySpacingPadding(PropertySpacing.Space.Two, PropertySpacing.Space.Null),
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Null, PropertySpacing.Space.Two, PropertySpacing.Space.Null, PropertySpacing.Space.Null),
            BackgroundColor = LayoutSchema.ContentBackground,
            Classes = new() { "flex-grow-1" }
        };

        /// <summary>
        /// Liefert die Flexbox
        /// </summary>
        private ControlPanelFlexbox Flexbox { get; } = new ControlPanelFlexbox()
        {
            Layout = TypeLayoutFlexbox.Default,
            Align = TypeAlignFlexbox.Stretch
        };

        /// <summary>
        /// Liefert oder setzt den Seiteneigenschaften
        /// </summary>
        public ControlWebAppProperty Property { get; } = new ControlWebAppProperty("webexpress.webapp.content.property");

        /// <summary>
        /// Liefert oder setzt den Werkzeugleiste
        /// </summary>
        public ControlToolbar Toolbar { get; } = new ControlToolbar("webexpress.webapp.content.toolbar");

        /// <summary>
        /// Liefert oder setzt das Überschriftssteuerelement
        /// </summary>
        public ControlWebAppHeadline Headline { get; } = new ControlWebAppHeadline("webexpress.webapp.content.main.headline");

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
        /// Constructor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlWebAppContent(string id = null)
            : base(id)
        {
            Init();

            Classes.Add("content");
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private void Init()
        {
            BackgroundColor = LayoutSchema.ContentBackground;
            Toolbar.BackgroundColor = LayoutSchema.ToolbarBackground;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.Two);

            Flexbox.Content.Add(MainPanel);
            Flexbox.Content.Add(Property);

            Content.Add(Toolbar);
            Content.Add(Flexbox);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            MainPanel.Content.Clear();

            MainPanel.Content.Add(Headline);
            MainPanel.Content.Add(new ControlPanel("webexpress.webapp.content.main.preferences", Preferences));
            MainPanel.Content.Add(new ControlPanel("webexpress.webapp.content.main.primary", Primary));
            MainPanel.Content.Add(new ControlPanel("webexpress.webapp.content.main.secondary", Secondary));

            Content.Clear();

            if (Toolbar.Items.Any())
            {
                Content.Add(Toolbar);
            }

            if (Property.Preferences.Any() || Property.Primary.Any() || Property.Secondary.Any())
            {
                Content.Add(Flexbox);
            }
            else
            {
                Content.Add(MainPanel);
            }

            return base.Render(context);
        }
    }
}
