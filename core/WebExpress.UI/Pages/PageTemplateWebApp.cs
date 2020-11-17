using System.Collections.Generic;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.Plugins;
using WebExpress.UI.Controls;

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
            Header.Title = this.I18N(Context.PluginName);

            Breadcrumb.Uri = Uri;
            Breadcrumb.Margin = new PropertySpacingMargin(PropertySpacing.Space.Null);
            Breadcrumb.BackgroundColor = LayoutSchema.BreadcrumbBackground;

            Toast.BackgroundColor = LayoutSchema.ValidationWarningBackground;

            Sidebar.BackgroundColor = LayoutSchema.SidebarBackground;

            Toolbar.BackgroundColor = LayoutSchema.ToolbarBackground;
            Content.BackgroundColor = LayoutSchema.ContentBackground;

            PageTitle.Text = this.I18N(Title);
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

            Header.HamburgerPrimary.AddRange(PluginComponentManager.CreatePluginComponents<IControlDropdownItem>("app.primary", Context.AppArtifactID));
            Header.HamburgerSecondary.AddRange(PluginComponentManager.CreatePluginComponents<IControlDropdownItem>("app.secondary", Context.AppArtifactID));
            Header.NavigationPreferences.AddRange(PluginComponentManager.CreatePluginComponents<IControlNavigationItem>("app.navigation.preferences", Context.AppArtifactID));
            Header.NavigationPrimary.AddRange(PluginComponentManager.CreatePluginComponents<IControlNavigationItem>("app.navigation.primary", Context.AppArtifactID));
            Header.NavigationSecondary.AddRange(PluginComponentManager.CreatePluginComponents<IControlNavigationItem>("app.navigation.secondary", Context.AppArtifactID));
            Header.QuickCreatePrimary.AddRange(PluginComponentManager.CreatePluginComponents<IControlSplitButtonItem>("app.quickcreate.primary", Context.AppArtifactID));
            Header.QuickCreatePrimary.AddRange(PluginComponentManager.CreatePluginComponents<IControlSplitButtonItem>("app.quickcreate.secondary", Context.AppArtifactID));
            Header.SettingsPrimary.AddRange(PluginComponentManager.CreatePluginComponents<IControlDropdownItem>("app.settings.primary", Context.AppArtifactID));
            Header.SettingsPrimary.AddRange(PluginComponentManager.CreatePluginComponents<IControlDropdownItem>("app.settings.secondary", Context.AppArtifactID));
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
