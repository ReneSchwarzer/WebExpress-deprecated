using System.Collections.Generic;
using WebExpress.Application;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.Components;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebResource;
using WebExpress.Uri;
using WebExpress.WebApp.Components;
using WebExpress.WebApp.WebControl;

namespace WebExpress.WebApp.WebResource
{
    /// <summary>
    /// Seite, die aus einem vertikal angeordneten Kopf-, Inhalt- und Fuss-Bereich besteht
    /// siehe doc/PageTemplateWebApp.vsdx
    /// </summary>
    public abstract class PageTemplateWebApp : ResourcePageTemplate
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
        public ControlWebAppFooter Footer { get; protected set; } = new ControlWebAppFooter("footer");

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
        public ControlWebAppProperty Property { get; protected set; } = new ControlWebAppProperty("property");

        /// <summary>
        /// Liefert oder setzt den Werkzeugleiste
        /// </summary>
        public ControlToolbar Toolbar { get; protected set; } = new ControlToolbar("toolbar");

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageTemplateWebApp()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Initialization()
        {
            base.Initialization();
            var application = ApplicationManager.GetApplcation(Context.ApplicationID);

            Header.Fixed = TypeFixed.Top;
            Header.Styles = new List<string>(new[] { "position: sticky; top: 0; z-index: 99;" });

            Header.Logo = application.Icon;
            Header.Title = this.I18N(application.ApplicationName);

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

            // Header
            Header.HamburgerPrimary.AddRange(ComponentManager.CreateComponent<IControlDropdownItem>(Context.ApplicationID, Section.AppPrimary, ResourceContext));
            Header.HamburgerSecondary.AddRange(ComponentManager.CreateComponent<IControlDropdownItem>(Context.ApplicationID, Section.AppSecondary, ResourceContext));
            Header.NavigationPreferences.AddRange(ComponentManager.CreateComponent<IControlNavigationItem>(Context.ApplicationID, Section.AppNavigationPreferences, ResourceContext));
            Header.NavigationPrimary.AddRange(ComponentManager.CreateComponent<IControlNavigationItem>(Context.ApplicationID, Section.AppNavigationPrimary, ResourceContext));
            Header.NavigationSecondary.AddRange(ComponentManager.CreateComponent<IControlNavigationItem>(Context.ApplicationID, Section.AppNavigationSecondary, ResourceContext));
            Header.QuickCreatePrimary.AddRange(ComponentManager.CreateComponent<IControlSplitButtonItem>(Context.ApplicationID, Section.AppQuickcreatePrimary, ResourceContext));
            Header.QuickCreatePrimary.AddRange(ComponentManager.CreateComponent<IControlSplitButtonItem>(Context.ApplicationID, Section.AppQuickcreateSecondary, ResourceContext));
            Header.SettingsPrimary.AddRange(ComponentManager.CreateComponent<IControlDropdownItem>(Context.ApplicationID, Section.AppSettingsPrimary, ResourceContext));
            Header.SettingsPrimary.AddRange(ComponentManager.CreateComponent<IControlDropdownItem>(Context.ApplicationID, Section.AppSettingsSecondary, ResourceContext));

            // Sidebar
            Sidebar.Preferences.AddRange(ComponentManager.CreateComponent<IControl>(Context.ApplicationID, Section.SidebarPreferences, ResourceContext));
            Sidebar.Primary.AddRange(ComponentManager.CreateComponent<IControl>(Context.ApplicationID, Section.SidebarPrimary, ResourceContext));
            Sidebar.Secondary.AddRange(ComponentManager.CreateComponent<IControl>(Context.ApplicationID, Section.SidebarSecondary, ResourceContext));

            // Property
            Property.Preferences.AddRange(ComponentManager.CreateComponent<IControl>(Context.ApplicationID, Section.PropertyPreferences, ResourceContext));
            Property.Primary.AddRange(ComponentManager.CreateComponent<IControl>(Context.ApplicationID, Section.PropertyPrimary, ResourceContext));
            Property.Secondary.AddRange(ComponentManager.CreateComponent<IControl>(Context.ApplicationID, Section.PropertySecondary, ResourceContext));

            // Footer
            Footer.Preferences.AddRange(ComponentManager.CreateComponent<IControl>(Context.ApplicationID, Section.FooterPreferences, ResourceContext));
            Footer.Primary.AddRange(ComponentManager.CreateComponent<IControl>(Context.ApplicationID, Section.FooterPrimary, ResourceContext));
            Footer.Secondary.AddRange(ComponentManager.CreateComponent<IControl>(Context.ApplicationID, Section.FooterSecondary, ResourceContext));
        }

        /// <summary>
        /// In HTML konvertieren+
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
                        Property
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
