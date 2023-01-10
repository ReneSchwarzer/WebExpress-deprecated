using System.Collections.Generic;
using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebPage;
using WebExpress.WebPage;

namespace WebExpress.WebApp.WebControl
{
    /// <summary>
    /// Header für eine WebApp
    /// </summary>
    public class ControlWebAppHeaderAppNavigation : ControlPanelFlexbox
    {
        /// <summary>
        /// Liefert oder setzt den den Bereich für die App-Navigation
        /// </summary>
        public List<IControlNavigationItem> Preferences { get; protected set; } = new List<IControlNavigationItem>();

        /// <summary>
        /// Liefert oder setzt den den Bereich für die App-Navigation
        /// </summary>
        public List<IControlNavigationItem> Primary { get; protected set; } = new List<IControlNavigationItem>();

        /// <summary>
        /// Liefert oder setzt den den Bereich für die App-Navigation
        /// </summary>
        public List<IControlNavigationItem> Secondary { get; protected set; } = new List<IControlNavigationItem>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlWebAppHeaderAppNavigation(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private void Init()
        {
            Padding = new PropertySpacingPadding(PropertySpacing.Space.Null);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {

            var preferences = new ControlNavigation("webexpress.webapp.header.appnavigation.preferences", Preferences)
            {
                Layout = TypeLayoutTab.Default,
                ActiveColor = LayoutSchema.HeaderNavigationActiveBackground,
                ActiveTextColor = LayoutSchema.HeaderNavigationActive,
                LinkColor = LayoutSchema.HeaderNavigationLink
            };

            var primary = new ControlNavigation("webexpress.webapp.header.appnavigation.primary", Primary)
            {
                Layout = TypeLayoutTab.Default,
                ActiveColor = LayoutSchema.HeaderNavigationActiveBackground,
                ActiveTextColor = LayoutSchema.HeaderNavigationActive,
                LinkColor = LayoutSchema.HeaderNavigationLink
            };

            var secondary = new ControlNavigation("webexpress.webapp.header.appnavigation.secondary", Secondary)
            {
                Layout = TypeLayoutTab.Default,
                ActiveColor = LayoutSchema.HeaderNavigationActiveBackground,
                ActiveTextColor = LayoutSchema.HeaderNavigationActive,
                LinkColor = LayoutSchema.HeaderNavigationLink
            };

            return new HtmlElementTextContentDiv(preferences.Render(context), primary.Render(context), secondary.Render(context))
            {
                ID = Id,
                Class = Css.Concatenate("", GetClasses()),
                Style = Style.Concatenate("", GetStyles()),
                Role = Role
            };
        }
    }
}
