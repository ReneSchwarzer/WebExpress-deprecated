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
        public new ControlWebAppContent Content { get; protected set; } = new ControlWebAppContent("content");

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
            Breadcrumb.Size = LayoutSchema.BreadcrumbSize;

            Toast.BackgroundColor = LayoutSchema.ValidationWarningBackground;

            Sidebar.BackgroundColor = LayoutSchema.SidebarBackground;

            Content.BackgroundColor = LayoutSchema.ContentBackground;
            Content.Margin = new PropertySpacingMargin(PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.None, PropertySpacing.Space.None);
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
            Header.HelpPreferences.AddRange(ComponentManager.CreateComponent<IControlDropdownItem>(Context.ApplicationID, Section.AppHelpPreferences, ResourceContext));
            Header.HelpPrimary.AddRange(ComponentManager.CreateComponent<IControlDropdownItem>(Context.ApplicationID, Section.AppHelpPrimary, ResourceContext));
            Header.HelpSecondary.AddRange(ComponentManager.CreateComponent<IControlDropdownItem>(Context.ApplicationID, Section.AppHelpSecondary, ResourceContext));
            Header.SettingsPrimary.AddRange(ComponentManager.CreateComponent<IControlDropdownItem>(Context.ApplicationID, Section.AppSettingsPrimary, ResourceContext));
            Header.SettingsSecondary.AddRange(ComponentManager.CreateComponent<IControlDropdownItem>(Context.ApplicationID, Section.AppSettingsSecondary, ResourceContext));

            // Sidebar
            Sidebar.Preferences.AddRange(ComponentManager.CreateComponent<IControl>(Context.ApplicationID, Section.SidebarPreferences, ResourceContext));
            Sidebar.Primary.AddRange(ComponentManager.CreateComponent<IControl>(Context.ApplicationID, Section.SidebarPrimary, ResourceContext));
            Sidebar.Secondary.AddRange(ComponentManager.CreateComponent<IControl>(Context.ApplicationID, Section.SidebarSecondary, ResourceContext));

            // Headline
            Content.Headline.Prologue.AddRange(ComponentManager.CreateComponent<IControl>(Context.ApplicationID, Section.HeadlinePrologue, ResourceContext));
            Content.Headline.Preferences.AddRange(ComponentManager.CreateComponent<IControl>(Context.ApplicationID, Section.HeadlinePreferences, ResourceContext));
            Content.Headline.Primary.AddRange(ComponentManager.CreateComponent<IControl>(Context.ApplicationID, Section.HeadlinePrimary, ResourceContext));
            Content.Headline.Secondary.AddRange(ComponentManager.CreateComponent<IControl>(Context.ApplicationID, Section.HeadlineSecondary, ResourceContext));
            Content.Headline.MorePreferences.AddRange(ComponentManager.CreateComponent<IControl>(Context.ApplicationID, Section.MorePreferences, ResourceContext));
            Content.Headline.MorePrimary.AddRange(ComponentManager.CreateComponent<IControl>(Context.ApplicationID, Section.MorePrimary, ResourceContext));
            Content.Headline.MoreSecondary.AddRange(ComponentManager.CreateComponent<IControl>(Context.ApplicationID, Section.MoreSecondary, ResourceContext));

            // Property
            Content.Property.Preferences.AddRange(ComponentManager.CreateComponent<IControl>(Context.ApplicationID, Section.PropertyPreferences, ResourceContext));
            Content.Property.Primary.AddRange(ComponentManager.CreateComponent<IControl>(Context.ApplicationID, Section.PropertyPrimary, ResourceContext));
            Content.Property.Secondary.AddRange(ComponentManager.CreateComponent<IControl>(Context.ApplicationID, Section.PropertySecondary, ResourceContext));

            // Inhalt
            Content.Preferences.AddRange(ComponentManager.CreateComponent<IControl>(Context.ApplicationID, Section.ContentPreferences, ResourceContext));
            Content.Primary.AddRange(ComponentManager.CreateComponent<IControl>(Context.ApplicationID, Section.ContentPrimary, ResourceContext));
            Content.Secondary.AddRange(ComponentManager.CreateComponent<IControl>(Context.ApplicationID, Section.ContentSecondary, ResourceContext));

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
            var split = new ControlPanelSplit
            (
                "split",
                new Control[] { Sidebar },
                new Control[] { Content }
            )
            {
                Orientation = TypeOrientationSplit.Horizontal,
                SplitterColor = LayoutSchema.SplitterColor,
                Panel1InitialSize = 20,
                Panel1MinSize = 150,
                Styles = new List<string>() { "min-height: 85%;" }
            };

            base.Content.Add(Header);
            base.Content.Add(Toast);
            base.Content.Add(Breadcrumb);
            base.Content.Add(SearchOptions);
            base.Content.Add(Sidebar.HasContent ? split : Content);
            base.Content.Add(Footer);

            return base.Render();
        }
    }
}
