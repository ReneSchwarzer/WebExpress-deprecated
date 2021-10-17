using System.Collections.Generic;
using WebExpress.Application;
using WebExpress.Internationalization;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebPage;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebPage;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebPage
{
    /// <summary>
    /// Seite, die aus einem vertikal angeordneten Kopf-, Inhalt- und Fuss-Bereich besteht
    /// siehe doc/PageTemplateWebApp.vsdx
    /// </summary>
    public abstract class PageWebApp : PageControl<RenderContextWebApp>
    {
        // Anwendung
        private IApplicationContext Application { get; set; }

        // Header
        private List<IControlDropdownItem> HeaderHamburgerPrimary { get; } = new List<IControlDropdownItem>();
        private List<IControlDropdownItem> HeaderHamburgerSecondary { get; } = new List<IControlDropdownItem>();
        private List<IControlNavigationItem> HeaderNavigationPreferences { get; } = new List<IControlNavigationItem>();
        private List<IControlNavigationItem> HeaderNavigationPrimary { get; } = new List<IControlNavigationItem>();
        private List<IControlNavigationItem> HeaderNavigationSecondary { get; } = new List<IControlNavigationItem>();
        private List<IControlSplitButtonItem> HeaderQuickCreatePrimary { get; } = new List<IControlSplitButtonItem>();
        private List<IControlSplitButtonItem> HeaderQuickCreateSecondary { get; } = new List<IControlSplitButtonItem>();
        private List<IControlDropdownItem> HeaderHelpPreferences { get; } = new List<IControlDropdownItem>();
        private List<IControlDropdownItem> HeaderHelpPrimary { get; } = new List<IControlDropdownItem>();
        private List<IControlDropdownItem> HeaderHelpSecondary { get; } = new List<IControlDropdownItem>();
        private List<IControlDropdownItem> HeaderSettingsPrimary { get; } = new List<IControlDropdownItem>();
        private List<IControlDropdownItem> HeaderSettingsSecondary { get; } = new List<IControlDropdownItem>();

        // Sidebar
        private List<IControl> SidebarHeader { get; } = new List<IControl>();
        private List<IControl> SidebarPreferences { get; } = new List<IControl>();
        private List<IControl> SidebarPrimary { get; } = new List<IControl>();
        private List<IControl> SidebarSecondary { get; } = new List<IControl>();

        // Headline
        private List<IControl> ContentHeadlinePrologue { get; } = new List<IControl>();
        private List<IControl> ContentHeadlinePreferences { get; } = new List<IControl>();
        private List<IControl> ContentHeadlinePrimary { get; } = new List<IControl>();
        private List<IControl> ContentHeadlineSecondary { get; } = new List<IControl>();
        private List<IControlDropdownItem> ContentHeadlineMorePreferences { get; } = new List<IControlDropdownItem>();
        private List<IControlDropdownItem> ContentHeadlineMorePrimary { get; } = new List<IControlDropdownItem>();
        private List<IControlDropdownItem> ContentHeadlineMoreSecondary { get; } = new List<IControlDropdownItem>();
        private List<IControl> ContentHeadlineMetadata { get; } = new List<IControl>();

        // Property
        private List<IControl> ContentPropertyPreferences { get; } = new List<IControl>();
        private List<IControl> ContentPropertyPrimary { get; } = new List<IControl>();
        private List<IControl> ContentPropertySecondary { get; } = new List<IControl>();

        // Inhalt
        private List<IControl> ContentPreferences { get; } = new List<IControl>();
        private List<IControl> ContentPrimary { get; } = new List<IControl>();
        private List<IControl> ContentSecondary { get; } = new List<IControl>();

        // Footer
        private List<IControl> FooterPreferences { get; } = new List<IControl>();
        private List<IControl> FooterPrimary { get; } = new List<IControl>();
        private List<IControl> FooterSecondary { get; } = new List<IControl>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageWebApp()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            Application = ApplicationManager.GetApplcation(context.ApplicationID);

            // Header
            HeaderHamburgerPrimary.AddRange(ComponentManager.CreateComponent<IControlDropdownItem>(context.ApplicationID, Section.AppPrimary, context.Context));
            HeaderHamburgerSecondary.AddRange(ComponentManager.CreateComponent<IControlDropdownItem>(context.ApplicationID, Section.AppSecondary, context.Context));
            HeaderNavigationPreferences.AddRange(ComponentManager.CreateComponent<IControlNavigationItem>(context.ApplicationID, Section.AppNavigationPreferences, context.Context));
            HeaderNavigationPrimary.AddRange(ComponentManager.CreateComponent<IControlNavigationItem>(context.ApplicationID, Section.AppNavigationPrimary, context.Context));
            HeaderNavigationSecondary.AddRange(ComponentManager.CreateComponent<IControlNavigationItem>(context.ApplicationID, Section.AppNavigationSecondary, context.Context));
            HeaderQuickCreatePrimary.AddRange(ComponentManager.CreateComponent<IControlSplitButtonItem>(context.ApplicationID, Section.AppQuickcreatePrimary, context.Context));
            HeaderQuickCreateSecondary.AddRange(ComponentManager.CreateComponent<IControlSplitButtonItem>(context.ApplicationID, Section.AppQuickcreateSecondary, context.Context));
            HeaderHelpPreferences.AddRange(ComponentManager.CreateComponent<IControlDropdownItem>(context.ApplicationID, Section.AppHelpPreferences, context.Context));
            HeaderHelpPrimary.AddRange(ComponentManager.CreateComponent<IControlDropdownItem>(context.ApplicationID, Section.AppHelpPrimary, context.Context));
            HeaderHelpSecondary.AddRange(ComponentManager.CreateComponent<IControlDropdownItem>(context.ApplicationID, Section.AppHelpSecondary, context.Context));
            HeaderSettingsPrimary.AddRange(ComponentManager.CreateComponent<IControlDropdownItem>(context.ApplicationID, Section.AppSettingsPrimary, context.Context));
            HeaderSettingsSecondary.AddRange(ComponentManager.CreateComponent<IControlDropdownItem>(context.ApplicationID, Section.AppSettingsSecondary, context.Context));

            // Sidebar
            SidebarHeader.AddRange(ComponentManager.CreateComponent<IControl>(context.ApplicationID, Section.SidebarHeader, context.Context));
            SidebarPreferences.AddRange(ComponentManager.CreateComponent<IControl>(context.ApplicationID, Section.SidebarPreferences, context.Context));
            SidebarPrimary.AddRange(ComponentManager.CreateComponent<IControl>(context.ApplicationID, Section.SidebarPrimary, context.Context));
            SidebarSecondary.AddRange(ComponentManager.CreateComponent<IControl>(context.ApplicationID, Section.SidebarSecondary, context.Context));

            // Headline
            ContentHeadlinePrologue.AddRange(ComponentManager.CreateComponent<IControl>(context.ApplicationID, Section.HeadlinePrologue, context.Context));
            ContentHeadlinePreferences.AddRange(ComponentManager.CreateComponent<IControl>(context.ApplicationID, Section.HeadlinePreferences, context.Context));
            ContentHeadlinePrimary.AddRange(ComponentManager.CreateComponent<IControl>(context.ApplicationID, Section.HeadlinePrimary, context.Context));
            ContentHeadlineSecondary.AddRange(ComponentManager.CreateComponent<IControl>(context.ApplicationID, Section.HeadlineSecondary, context.Context));
            ContentHeadlineMorePreferences.AddRange(ComponentManager.CreateComponent<IControlDropdownItem>(context.ApplicationID, Section.MorePreferences, context.Context));
            ContentHeadlineMorePrimary.AddRange(ComponentManager.CreateComponent<IControlDropdownItem>(context.ApplicationID, Section.MorePrimary, context.Context));
            ContentHeadlineMoreSecondary.AddRange(ComponentManager.CreateComponent<IControlDropdownItem>(context.ApplicationID, Section.MoreSecondary, context.Context));
            ContentHeadlineMetadata.AddRange(ComponentManager.CreateComponent<IControl>(context.ApplicationID, Section.Metadata, context.Context));

            // Property
            ContentPropertyPreferences.AddRange(ComponentManager.CreateComponent<IControl>(context.ApplicationID, Section.PropertyPreferences, context.Context));
            ContentPropertyPrimary.AddRange(ComponentManager.CreateComponent<IControl>(context.ApplicationID, Section.PropertyPrimary, context.Context));
            ContentPropertySecondary.AddRange(ComponentManager.CreateComponent<IControl>(context.ApplicationID, Section.PropertySecondary, context.Context));

            // Inhalt
            ContentPreferences.AddRange(ComponentManager.CreateComponent<IControl>(context.ApplicationID, Section.ContentPreferences, context.Context));
            ContentPrimary.AddRange(ComponentManager.CreateComponent<IControl>(context.ApplicationID, Section.ContentPrimary, context.Context));
            ContentSecondary.AddRange(ComponentManager.CreateComponent<IControl>(context.ApplicationID, Section.ContentSecondary, context.Context));

            // Footer
            FooterPreferences.AddRange(ComponentManager.CreateComponent<IControl>(context.ApplicationID, Section.FooterPreferences, context.Context));
            FooterPrimary.AddRange(ComponentManager.CreateComponent<IControl>(context.ApplicationID, Section.FooterPrimary, context.Context));
            FooterSecondary.AddRange(ComponentManager.CreateComponent<IControl>(context.ApplicationID, Section.FooterSecondary, context.Context));
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var visualTree = context.VisualTree;

            visualTree.Header.Logo = Application?.Icon;
            visualTree.Header.Title = this.I18N(Application?.ApplicationName);

            visualTree.Breadcrumb.Uri = context.Uri;

            // Header
            visualTree.Header.HamburgerPrimary.AddRange(HeaderHamburgerPrimary);
            visualTree.Header.HamburgerSecondary.AddRange(HeaderHamburgerSecondary);
            visualTree.Header.NavigationPreferences.AddRange(HeaderNavigationPreferences);
            visualTree.Header.NavigationPrimary.AddRange(HeaderNavigationPrimary);
            visualTree.Header.NavigationSecondary.AddRange(HeaderNavigationSecondary);
            visualTree.Header.QuickCreatePrimary.AddRange(HeaderQuickCreatePrimary);
            visualTree.Header.QuickCreateSecondary.AddRange(HeaderQuickCreateSecondary);
            visualTree.Header.HelpPreferences.AddRange(HeaderHelpPreferences);
            visualTree.Header.HelpPrimary.AddRange(HeaderHelpPrimary);
            visualTree.Header.HelpSecondary.AddRange(HeaderHelpSecondary);
            visualTree.Header.SettingsPrimary.AddRange(HeaderSettingsPrimary);
            visualTree.Header.SettingsSecondary.AddRange(HeaderSettingsSecondary);

            // Sidebar
            visualTree.Sidebar.Header.AddRange(SidebarHeader);
            visualTree.Sidebar.Preferences.AddRange(SidebarPreferences);
            visualTree.Sidebar.Primary.AddRange(SidebarPrimary);
            visualTree.Sidebar.Secondary.AddRange(SidebarSecondary);

            // Headline
            visualTree.Content.Headline.Prologue.AddRange(ContentHeadlinePrologue);
            visualTree.Content.Headline.Preferences.AddRange(ContentHeadlinePreferences);
            visualTree.Content.Headline.Primary.AddRange(ContentHeadlinePrimary);
            visualTree.Content.Headline.Secondary.AddRange(ContentHeadlineSecondary);
            visualTree.Content.Headline.MorePreferences.AddRange(ContentHeadlineMorePreferences);
            visualTree.Content.Headline.MorePrimary.AddRange(ContentHeadlineMorePrimary);
            visualTree.Content.Headline.MoreSecondary.AddRange(ContentHeadlineMoreSecondary);
            visualTree.Content.Headline.Metadata.AddRange(ContentHeadlineMetadata);

            // Property
            visualTree.Content.Property.Preferences.AddRange(ContentPropertyPreferences);
            visualTree.Content.Property.Primary.AddRange(ContentPropertyPrimary);
            visualTree.Content.Property.Secondary.AddRange(ContentPropertySecondary);

            // Inhalt
            visualTree.Content.Preferences.AddRange(ContentPreferences);
            visualTree.Content.Primary.AddRange(ContentPrimary);
            visualTree.Content.Secondary.AddRange(ContentSecondary);

            // Footer
            visualTree.Footer.Preferences.AddRange(FooterPreferences);
            visualTree.Footer.Primary.AddRange(FooterPrimary);
            visualTree.Footer.Secondary.AddRange(FooterSecondary);

            if (visualTree is VisualTreeControl visualTreeControl)
            {
                var split = new ControlPanelSplit
            (
                "split",
                new Control[] { visualTree.Sidebar },
                new Control[] { visualTree.Content }
            )
                {
                    Orientation = TypeOrientationSplit.Horizontal,
                    SplitterColor = LayoutSchema.SplitterColor,
                    Panel1InitialSize = 20,
                    Panel1MinSize = 150,
                    Styles = new List<string>() { "min-height: 85%;" }
                };

                visualTreeControl.Content.Add(visualTree.Header);
                visualTreeControl.Content.Add(visualTree.Toast);
                visualTreeControl.Content.Add(visualTree.Breadcrumb);
                visualTreeControl.Content.Add(visualTree.Prologue);
                visualTreeControl.Content.Add(visualTree.SearchOptions);
                visualTreeControl.Content.Add(visualTree.Sidebar.HasContent ? split : visualTree.Content);
                visualTreeControl.Content.Add(visualTree.Footer);
            }
        }
    }
}
