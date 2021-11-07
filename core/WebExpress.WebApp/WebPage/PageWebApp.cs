using System.Collections.Generic;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.Module;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebPage;
using WebExpress.Uri;
using WebExpress.WebApp.WebApiControl;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebPage
{
    /// <summary>
    /// Seite, die aus einem vertikal angeordneten Kopf-, Inhalt- und Fuss-Bereich besteht
    /// siehe doc/PageTemplateWebApp.vsdx
    /// </summary>
    public abstract class PageWebApp : PageControl<RenderContextWebApp>
    {
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

            var module = ModuleManager.GetModule(Context?.Application, "webexpress.webapp");
            if (module != null)
            {
                CssLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/css/webexpress.webapp.css")));
                HeaderScriptLinks.Add(module.ContextPath.Append("assets/js/webexpress.webapp.js"));
            }

            // Header
            HeaderHamburgerPrimary.AddRange(ComponentManager.CreateComponent<IControlDropdownItem>(context.Application, Section.AppPrimary, context.Context));
            HeaderHamburgerSecondary.AddRange(ComponentManager.CreateComponent<IControlDropdownItem>(context.Application, Section.AppSecondary, context.Context));
            HeaderNavigationPreferences.AddRange(ComponentManager.CreateComponent<IControlNavigationItem>(context.Application, Section.AppNavigationPreferences, context.Context));
            HeaderNavigationPrimary.AddRange(ComponentManager.CreateComponent<IControlNavigationItem>(context.Application, Section.AppNavigationPrimary, context.Context));
            HeaderNavigationSecondary.AddRange(ComponentManager.CreateComponent<IControlNavigationItem>(context.Application, Section.AppNavigationSecondary, context.Context));
            HeaderQuickCreatePrimary.AddRange(ComponentManager.CreateComponent<IControlSplitButtonItem>(context.Application, Section.AppQuickcreatePrimary, context.Context));
            HeaderQuickCreateSecondary.AddRange(ComponentManager.CreateComponent<IControlSplitButtonItem>(context.Application, Section.AppQuickcreateSecondary, context.Context));
            HeaderHelpPreferences.AddRange(ComponentManager.CreateComponent<IControlDropdownItem>(context.Application, Section.AppHelpPreferences, context.Context));
            HeaderHelpPrimary.AddRange(ComponentManager.CreateComponent<IControlDropdownItem>(context.Application, Section.AppHelpPrimary, context.Context));
            HeaderHelpSecondary.AddRange(ComponentManager.CreateComponent<IControlDropdownItem>(context.Application, Section.AppHelpSecondary, context.Context));
            HeaderSettingsPrimary.AddRange(ComponentManager.CreateComponent<IControlDropdownItem>(context.Application, Section.AppSettingsPrimary, context.Context));
            HeaderSettingsSecondary.AddRange(ComponentManager.CreateComponent<IControlDropdownItem>(context.Application, Section.AppSettingsSecondary, context.Context));

            // Sidebar
            SidebarHeader.AddRange(ComponentManager.CreateComponent<IControl>(context.Application, Section.SidebarHeader, context.Context));
            SidebarPreferences.AddRange(ComponentManager.CreateComponent<IControl>(context.Application, Section.SidebarPreferences, context.Context));
            SidebarPrimary.AddRange(ComponentManager.CreateComponent<IControl>(context.Application, Section.SidebarPrimary, context.Context));
            SidebarSecondary.AddRange(ComponentManager.CreateComponent<IControl>(context.Application, Section.SidebarSecondary, context.Context));

            // Headline
            ContentHeadlinePrologue.AddRange(ComponentManager.CreateComponent<IControl>(context.Application, Section.HeadlinePrologue, context.Context));
            ContentHeadlinePreferences.AddRange(ComponentManager.CreateComponent<IControl>(context.Application, Section.HeadlinePreferences, context.Context));
            ContentHeadlinePrimary.AddRange(ComponentManager.CreateComponent<IControl>(context.Application, Section.HeadlinePrimary, context.Context));
            ContentHeadlineSecondary.AddRange(ComponentManager.CreateComponent<IControl>(context.Application, Section.HeadlineSecondary, context.Context));
            ContentHeadlineMorePreferences.AddRange(ComponentManager.CreateComponent<IControlDropdownItem>(context.Application, Section.MorePreferences, context.Context));
            ContentHeadlineMorePrimary.AddRange(ComponentManager.CreateComponent<IControlDropdownItem>(context.Application, Section.MorePrimary, context.Context));
            ContentHeadlineMoreSecondary.AddRange(ComponentManager.CreateComponent<IControlDropdownItem>(context.Application, Section.MoreSecondary, context.Context));
            ContentHeadlineMetadata.AddRange(ComponentManager.CreateComponent<IControl>(context.Application, Section.Metadata, context.Context));

            // Property
            ContentPropertyPreferences.AddRange(ComponentManager.CreateComponent<IControl>(context.Application, Section.PropertyPreferences, context.Context));
            ContentPropertyPrimary.AddRange(ComponentManager.CreateComponent<IControl>(context.Application, Section.PropertyPrimary, context.Context));
            ContentPropertySecondary.AddRange(ComponentManager.CreateComponent<IControl>(context.Application, Section.PropertySecondary, context.Context));

            // Inhalt
            ContentPreferences.AddRange(ComponentManager.CreateComponent<IControl>(context.Application, Section.ContentPreferences, context.Context));
            ContentPrimary.AddRange(ComponentManager.CreateComponent<IControl>(context.Application, Section.ContentPrimary, context.Context));
            ContentSecondary.AddRange(ComponentManager.CreateComponent<IControl>(context.Application, Section.ContentSecondary, context.Context));

            // Footer
            FooterPreferences.AddRange(ComponentManager.CreateComponent<IControl>(context.Application, Section.FooterPreferences, context.Context));
            FooterPrimary.AddRange(ComponentManager.CreateComponent<IControl>(context.Application, Section.FooterPrimary, context.Context));
            FooterSecondary.AddRange(ComponentManager.CreateComponent<IControl>(context.Application, Section.FooterSecondary, context.Context));
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            context.VisualTree.Favicons.Add(new Favicon(context.Application.Icon));
            context.VisualTree.Header.Logo = context.Application?.Icon;
            context.VisualTree.Header.Title = this.I18N(context.Application, context.Application?.ApplicationName);

            context.VisualTree.Breadcrumb.Uri = context.Uri;

            // Header
            context.VisualTree.Header.HamburgerPrimary.AddRange(HeaderHamburgerPrimary);
            context.VisualTree.Header.HamburgerSecondary.AddRange(HeaderHamburgerSecondary);
            context.VisualTree.Header.NavigationPreferences.AddRange(HeaderNavigationPreferences);
            context.VisualTree.Header.NavigationPrimary.AddRange(HeaderNavigationPrimary);
            context.VisualTree.Header.NavigationSecondary.AddRange(HeaderNavigationSecondary);
            context.VisualTree.Header.QuickCreatePrimary.AddRange(HeaderQuickCreatePrimary);
            context.VisualTree.Header.QuickCreateSecondary.AddRange(HeaderQuickCreateSecondary);
            context.VisualTree.Header.HelpPreferences.AddRange(HeaderHelpPreferences);
            context.VisualTree.Header.HelpPrimary.AddRange(HeaderHelpPrimary);
            context.VisualTree.Header.HelpSecondary.AddRange(HeaderHelpSecondary);
            context.VisualTree.Header.SettingsPrimary.AddRange(HeaderSettingsPrimary);
            context.VisualTree.Header.SettingsSecondary.AddRange(HeaderSettingsSecondary);

            // Sidebar
            context.VisualTree.Sidebar.Header.AddRange(SidebarHeader);
            context.VisualTree.Sidebar.Preferences.AddRange(SidebarPreferences);
            context.VisualTree.Sidebar.Primary.AddRange(SidebarPrimary);
            context.VisualTree.Sidebar.Secondary.AddRange(SidebarSecondary);

            // Headline
            context.VisualTree.Content.Headline.Prologue.AddRange(ContentHeadlinePrologue);
            context.VisualTree.Content.Headline.Preferences.AddRange(ContentHeadlinePreferences);
            context.VisualTree.Content.Headline.Primary.AddRange(ContentHeadlinePrimary);
            context.VisualTree.Content.Headline.Secondary.AddRange(ContentHeadlineSecondary);
            context.VisualTree.Content.Headline.MorePreferences.AddRange(ContentHeadlineMorePreferences);
            context.VisualTree.Content.Headline.MorePrimary.AddRange(ContentHeadlineMorePrimary);
            context.VisualTree.Content.Headline.MoreSecondary.AddRange(ContentHeadlineMoreSecondary);
            context.VisualTree.Content.Headline.Metadata.AddRange(ContentHeadlineMetadata);

            // Property
            context.VisualTree.Content.Property.Preferences.AddRange(ContentPropertyPreferences);
            context.VisualTree.Content.Property.Primary.AddRange(ContentPropertyPrimary);
            context.VisualTree.Content.Property.Secondary.AddRange(ContentPropertySecondary);

            // Inhalt
            context.VisualTree.Content.Preferences.AddRange(ContentPreferences);
            context.VisualTree.Content.Primary.AddRange(ContentPrimary);
            context.VisualTree.Content.Secondary.AddRange(ContentSecondary);

            // Footer
            context.VisualTree.Footer.Preferences.AddRange(FooterPreferences);
            context.VisualTree.Footer.Primary.AddRange(FooterPrimary);
            context.VisualTree.Footer.Secondary.AddRange(FooterSecondary);

            if (context.VisualTree is VisualTreeControl visualTreeControl)
            {
                var split = new ControlPanelSplit
            (
                "split",
                new Control[] { context.VisualTree.Sidebar },
                new Control[] { context.VisualTree.Content }
            )
                {
                    Orientation = TypeOrientationSplit.Horizontal,
                    SplitterColor = LayoutSchema.SplitterColor,
                    Panel1InitialSize = 20,
                    Panel1MinSize = 150,
                    Styles = new List<string>() { "min-height: 85%;" }
                };

                visualTreeControl.Content.Add(context.VisualTree.Header);
                visualTreeControl.Content.Add(context.VisualTree.Toast);
                visualTreeControl.Content.Add(context.VisualTree.Breadcrumb);
                visualTreeControl.Content.Add(context.VisualTree.Prologue);
                visualTreeControl.Content.Add(context.VisualTree.SearchOptions);
                visualTreeControl.Content.Add(context.VisualTree.Sidebar.HasContent ? split : context.VisualTree.Content);
                visualTreeControl.Content.Add(context.VisualTree.Footer);
                visualTreeControl.Content.Add(new ControlApiNotificationPopup("popup_notification"));
            }
        }
    }
}
