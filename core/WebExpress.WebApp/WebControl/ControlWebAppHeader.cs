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
    public class ControlWebAppHeader : Control
    {
        /// <summary>
        /// Liefert oder setzt die Farbe des Textes
        /// </summary>
        public new virtual PropertyColorNavbar TextColor
        {
            get => (PropertyColorNavbar)GetPropertyObject();
            set => SetProperty(value, () => value?.ToClass(), () => value?.ToStyle());
        }

        /// <summary>
        /// Die fixierte Anordnung
        /// </summary>
        public virtual TypeFixed Fixed
        {
            get => (TypeFixed)GetProperty(TypeFixed.None);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Fixiert die Anordnung, wenn sich die Toolbar am oberen Rand befindet
        /// </summary>
        public virtual TypeSticky Sticky
        {
            get => (TypeSticky)GetProperty(TypeSticky.None);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Der Anwendungsnavigator
        /// </summary>
        public ControlWebAppHeaderAppNavigator AppNavigator { get; } = new ControlWebAppHeaderAppNavigator("webexpress.webapp.header.appnavigator")
        {
        };

        /// <summary>
        /// Der Name der Anwendung
        /// </summary>
        public ControlWebAppHeaderAppTitle AppTitle { get; } = new ControlWebAppHeaderAppTitle("webexpress.webapp.header.apptitle")
        {
        };

        /// <summary>
        /// Die Navigation der Anwendung
        /// </summary>
        public ControlWebAppHeaderAppNavigation AppNavigation { get; } = new ControlWebAppHeaderAppNavigation("webexpress.webapp.header.appnavigation")
        {
            Layout = TypeLayoutFlexbox.Inline,
            Justify = TypeJustifiedFlexbox.Start
        };

        /// <summary>
        /// Die Navigation der Anwendung
        /// </summary>
        public ControlWebAppHeaderQuickCreate QuickCreate { get; } = new ControlWebAppHeaderQuickCreate("webexpress.webapp.header.quickcreate")
        {
        };

        /// <summary>
        /// Die Navigation der Anwendungshilfen
        /// </summary>
        public ControlWebAppHeaderHelp Help { get; } = new ControlWebAppHeaderHelp("webexpress.webapp.header.help")
        {
        };

        /// <summary>
        /// Die Navigation der Anwendungseinstellungen
        /// </summary>
        public ControlWebAppHeaderSettings Settings { get; } = new ControlWebAppHeaderSettings("webexpress.webapp.header.settings")
        {
        };

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlWebAppHeader(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private void Init()
        {
            Fixed = TypeFixed.Top;
            Styles = new List<string>(new[] { "position: sticky; top: 0; z-index: 99;" });
            Padding = new PropertySpacingPadding(PropertySpacing.Space.Null);
            BackgroundColor = LayoutSchema.HeaderBackground;
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var content = new ControlPanelFlexbox
            (
                AppNavigator,
                AppTitle,
                AppNavigation,
                QuickCreate,
                new ControlPanel() { Margin = new PropertySpacingMargin(PropertySpacing.Space.Auto, PropertySpacing.Space.None) },
                Help,
                Settings
            )
            {
                Layout = TypeLayoutFlexbox.Default,
                Align = TypeAlignFlexbox.Center
            };

            return new HtmlElementSectionHeader(content.Render(context))
            {
                ID = Id,
                Class = Css.Concatenate("navbar", GetClasses()),
                Style = Style.Concatenate("display: block;", GetStyles()),
                Role = Role
            };
        }
    }
}
