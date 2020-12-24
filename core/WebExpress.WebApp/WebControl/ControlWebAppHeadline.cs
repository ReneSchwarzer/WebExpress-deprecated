using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.Uri;

namespace WebExpress.WebApp.WebControl
{
    /// <summary>
    /// Header für eine WebApp
    /// </summary>
    public class ControlWebAppHeadline : Control
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
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlWebAppHeadline(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            BackgroundColor = LayoutSchema.HeadlineBackground;
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var navigation = new List<IControlNavigationItem>(Preferences);
            navigation.AddRange(Primary);
            navigation.AddRange(Secondary);

            var content = new ControlPanelFlexbox
            (
                new ControlLink("title", new ControlText()
                {
                    Text = context.I18N(context.Page.Title),
                    TextColor = LayoutSchema.HeadlineTitle,
                    Format = TypeFormatText.H2,
                    Padding = new PropertySpacingPadding(PropertySpacing.Space.One),
                    Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.Null)
                })
                {
                    Uri = context.Page.Uri.Root,
                    Decoration = TypeTextDecoration.None
                },
                new ControlNavigation("functions", navigation)
                {
                    Layout = TypeLayoutTab.Default,
                    ActiveColor = LayoutSchema.HeaderNavigationActiveBackground,
                    ActiveTextColor = LayoutSchema.HeaderNavigationActive,
                    LinkColor = LayoutSchema.HeaderNavigationLink
                }
            )
            {
                Layout = TypeLayoutFlexbox.Default,
                Align = TypeAlignFlexbox.Center
            };

            return new HtmlElementSectionHeader(content.Render(context))
            {
                ID = ID,
                Class = Css.Concatenate("", GetClasses()),
                Style = Style.Concatenate("display: block;", GetStyles()),
                Role = Role
            };
        }
    }
}
