using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.Controls;
using WebExpress.UI.Plugin;

namespace WebExpress.UI.Pages
{
    /// <summary>
    /// Seite, die aus einem vertikal angeordneten Kopf-, Inhalt- und Fuss-Bereich besteht
    /// siehe doc/PageTemplateWebApp.vsdx
    /// </summary>
    public abstract class PageTemplateWebApp : PageTemplate
    {
        /// <summary>
        /// Liefert oder setzt den Kopf
        /// </summary>
        public ControlWebAppHeader Header { get; protected set; } = new ControlWebAppHeader("header");

        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        public new ControlPanelMain Content { get; protected set; } = new ControlPanelMain("content");

        /// <summary>
        /// Liefert oder setzt den Fuß
        /// </summary>
        public ControlPanelFooter Footer { get; protected set; } = new ControlPanelFooter("footer");

        /// <summary>
        /// Liefert oder setzt den Bereich für die Meldungen
        /// </summary>
        public ControlPanelToast Toast { get; protected set; } = new ControlPanelToast("toast");

        /// <summary>
        /// Liefert oder setzt den Bereich für die Pfadangabe
        /// </summary>
        public ControlBreadcrumb Breadcrumb { get; protected set; } = new ControlBreadcrumb("breadcrumb");

        /// <summary>
        /// Liefert oder setzt den den Bereich für die Suchoptionen
        /// </summary>
        public ControlPanel SearchOptions { get; protected set; } = new ControlPanel("searchoptions");

        /// <summary>
        /// Liefert oder setzt die Sidebar
        /// </summary>
        public ControlWebAppSidebar Sidebar { get; protected set; } = new ControlWebAppSidebar("sidebar");

        /// <summary>
        /// Liefert oder setzt den Navigationsbereich
        /// </summary>
        public ControlText PageTitle { get; protected set; } = new ControlText("pagetitle");

        /// <summary>
        /// Liefert oder setzt die Seitenfunktionen
        /// </summary>
        public ControlPanel PageFunctions { get; protected set; } = new ControlPanel("pagefunctions");

        /// <summary>
        /// Liefert oder setzt den Seiteneigenschaften
        /// </summary>
        public ControlPanel PageProperty { get; protected set; } = new ControlPanel("pageproperty");

        /// <summary>
        /// Liefert oder setzt den Werkzeugleiste
        /// </summary>
        public ControlToolbar Toolbar { get; protected set; } = new ControlToolbar("toolbar");

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

            
            Header.Fixed = TypeFixed.Top;
            Header.Styles = new List<string>(new[] { "position: sticky; top: 0; z-index: 99;" });

            Header.Logo = Uri.Root.Append(Context.IconUrl);
            Header.Title = Context.Name;

            Breadcrumb.Uri = Uri;
            Breadcrumb.Margin = new PropertySpacingMargin(PropertySpacing.Space.Null);
            Breadcrumb.BackgroundColor = LayoutSchema.BreadcrumbBackground;

            Toast.BackgroundColor = LayoutSchema.ValidationWarningBackground;

            Sidebar.BackgroundColor = LayoutSchema.SidebarBackground;

            Toolbar.BackgroundColor = LayoutSchema.ToolbarBackground;
            Content.BackgroundColor = LayoutSchema.ContentBackground;

            PageTitle.Text = Title;
            PageTitle.Format = TypeFormatText.H2;
            PageTitle.TextColor = new PropertyColorText(TypeColorText.Dark);
            PageTitle.Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.None);

            PageFunctions.BackgroundColor = new PropertyColorBackground(TypeColorBackground.Danger);

            Content.Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.None);
            Content.Width = TypeWidth.OneHundred;

            Footer.BackgroundColor = LayoutSchema.FooterBackground;
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            base.Process();

            var hamburgerPrimary = Context.Host.CreatePluginComponet<IPluginComponentHamburgerPrimary>();
            Header.HamburgerPrimary.AddRange(hamburgerPrimary.Where(x => x != null));

            var hamburgerSecondary = Context.Host.CreatePluginComponet<IPluginComponentHamburgerSecondary>();
            Header.HamburgerSecondary.AddRange(hamburgerSecondary.Where(x => x != null));

            var appNavigationPreferences = Context.Host.CreatePluginComponet<IPluginComponentAppNavigationPreferences>();
            Header.NavigationPreferences.AddRange(appNavigationPreferences.Where(x => x != null));

            var appNavigationPrimary = Context.Host.CreatePluginComponet<IPluginComponentAppNavigationPrimary>();
            Header.NavigationPrimary.AddRange(appNavigationPrimary.Where(x => x != null));

            var appNavigationSecondary = Context.Host.CreatePluginComponet<IPluginComponentAppNavigationSecondary>();
            Header.NavigationSecondary.AddRange(appNavigationSecondary.Where(x => x != null));

            var quickCreatePrimary = Context.Host.CreatePluginComponet<IPluginComponentQuickCreatePrimary>();
            Header.QuickCreatePrimary.AddRange(quickCreatePrimary.Where(x => x != null));

            var quickCreateSecondary = Context.Host.CreatePluginComponet<IPluginComponentQuickCreateSecondary>();
            Header.QuickCreateSecondary.AddRange(quickCreateSecondary.Where(x => x != null));

            var settingsPrimary = Context.Host.CreatePluginComponet<IPluginComponentSettingsPrimary>();
            Header.SettingsPrimary.AddRange(settingsPrimary.Where(x => x != null));

            var settingsSecondary = Context.Host.CreatePluginComponet<IPluginComponentSettingsSecondary>();
            Header.SettingsSecondary.AddRange(settingsSecondary.Where(x => x != null));

        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Die Seite als HTML-Baum</returns>
        public override IHtmlNode Render()
        {
            var flexbox = new ControlPanelFlexbox
            (
                Sidebar,
                new ControlPanel
                (
                    Toolbar,
                    new ControlPanelFlexbox
                    (
                        PageTitle,
                        PageFunctions
                    )
                    {
                        Layout = TypeLayoutFlexbox.Default,
                        Align = TypeAlignFlexbox.Start
                    },
                    new ControlPanelFlexbox
                    (
                        Content,
                        PageProperty
                    )
                    {
                        Layout = TypeLayoutFlexbox.Default,
                        Align = TypeAlignFlexbox.Stretch
                    }
                )
                {
                    BackgroundColor = LayoutSchema.MainBackground,
                    Width = TypeWidth.OneHundred
                }
            )
            {
                Layout = TypeLayoutFlexbox.Default,
                Align = TypeAlignFlexbox.Stretch
            };


            base.Content.Add(Header);
            base.Content.Add(Toast);
            base.Content.Add(Breadcrumb);
            base.Content.Add(SearchOptions);
            base.Content.Add(flexbox);
            base.Content.Add(Footer);

            return base.Render();
        }
    }
}
