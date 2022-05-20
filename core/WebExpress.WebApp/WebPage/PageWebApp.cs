using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.UI.WebComponent;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebPage;
using WebExpress.Uri;
using WebExpress.WebApp.WebApiControl;
using WebExpress.WebApp.WebComponent;
using WebExpress.WebModule;
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
        private List<ComponentCacheItem> HeaderHamburgerPrimary { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> HeaderHamburgerSecondary { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> HeaderNavigationPreferences { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> HeaderNavigationPrimary { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> HeaderNavigationSecondary { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> HeaderQuickCreatePrimary { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> HeaderQuickCreateSecondary { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> HeaderHelpPreferences { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> HeaderHelpPrimary { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> HeaderHelpSecondary { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> HeaderSettingsPrimary { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> HeaderSettingsSecondary { get; } = new List<ComponentCacheItem>();

        // Sidebar
        private List<ComponentCacheItem> SidebarHeader { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> SidebarPreferences { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> SidebarPrimary { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> SidebarSecondary { get; } = new List<ComponentCacheItem>();

        // Headline
        private List<ComponentCacheItem> ContentHeadlinePrologue { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> ContentHeadlinePreferences { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> ContentHeadlinePrimary { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> ContentHeadlineSecondary { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> ContentHeadlineMorePreferences { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> ContentHeadlineMorePrimary { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> ContentHeadlineMoreSecondary { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> ContentHeadlineMetadata { get; } = new List<ComponentCacheItem>();

        // Property
        private List<ComponentCacheItem> ContentPropertyPreferences { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> ContentPropertyPrimary { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> ContentPropertySecondary { get; } = new List<ComponentCacheItem>();

        // Inhalt
        private List<ComponentCacheItem> ContentPreferences { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> ContentPrimary { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> ContentSecondary { get; } = new List<ComponentCacheItem>();

        // Footer
        private List<ComponentCacheItem> FooterPreferences { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> FooterPrimary { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> FooterSecondary { get; } = new List<ComponentCacheItem>();

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
                CssLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/css/webexpress.webapp.popupnotification.css")));
                CssLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/css/webexpress.webapp.taskprogressbar.css")));
                HeaderScriptLinks.Add(module.ContextPath.Append("assets/js/webexpress.webapp.js"));
                HeaderScriptLinks.Add(module.ContextPath.Append("assets/js/webexpress.webapp.popupnotification.js"));
                HeaderScriptLinks.Add(module.ContextPath.Append("assets/js/webexpress.webapp.selection.js"));
                HeaderScriptLinks.Add(module.ContextPath.Append("assets/js/webexpress.webapp.table.js"));
                HeaderScriptLinks.Add(module.ContextPath.Append("assets/js/webexpress.webapp.taskprogressbar.js"));
            }

            // Header
            HeaderHamburgerPrimary.AddRange(ComponentManager.CacheComponent<IControlDropdownItem>(context.Application, Section.AppPrimary, this, context.Context));
            HeaderHamburgerSecondary.AddRange(ComponentManager.CacheComponent<IControlDropdownItem>(context.Application, Section.AppSecondary, this, context.Context));
            HeaderNavigationPreferences.AddRange(ComponentManager.CacheComponent<IControlNavigationItem>(context.Application, Section.AppNavigationPreferences, this, context.Context));
            HeaderNavigationPrimary.AddRange(ComponentManager.CacheComponent<IControlNavigationItem>(context.Application, Section.AppNavigationPrimary, this, context.Context));
            HeaderNavigationSecondary.AddRange(ComponentManager.CacheComponent<IControlNavigationItem>(context.Application, Section.AppNavigationSecondary, this, context.Context));
            HeaderQuickCreatePrimary.AddRange(ComponentManager.CacheComponent<IControlSplitButtonItem>(context.Application, Section.AppQuickcreatePrimary, this, context.Context));
            HeaderQuickCreateSecondary.AddRange(ComponentManager.CacheComponent<IControlSplitButtonItem>(context.Application, Section.AppQuickcreateSecondary, this, context.Context));
            HeaderHelpPreferences.AddRange(ComponentManager.CacheComponent<IControlDropdownItem>(context.Application, Section.AppHelpPreferences, this, context.Context));
            HeaderHelpPrimary.AddRange(ComponentManager.CacheComponent<IControlDropdownItem>(context.Application, Section.AppHelpPrimary, this, context.Context));
            HeaderHelpSecondary.AddRange(ComponentManager.CacheComponent<IControlDropdownItem>(context.Application, Section.AppHelpSecondary, this, context.Context));
            HeaderSettingsPrimary.AddRange(ComponentManager.CacheComponent<IControlDropdownItem>(context.Application, Section.AppSettingsPrimary, this, context.Context));
            HeaderSettingsSecondary.AddRange(ComponentManager.CacheComponent<IControlDropdownItem>(context.Application, Section.AppSettingsSecondary, this, context.Context));

            // Sidebar
            SidebarHeader.AddRange(ComponentManager.CacheComponent<IControl>(context.Application, Section.SidebarHeader, this, context.Context));
            SidebarPreferences.AddRange(ComponentManager.CacheComponent<IControl>(context.Application, Section.SidebarPreferences, this, context.Context));
            SidebarPrimary.AddRange(ComponentManager.CacheComponent<IControl>(context.Application, Section.SidebarPrimary, this, context.Context));
            SidebarSecondary.AddRange(ComponentManager.CacheComponent<IControl>(context.Application, Section.SidebarSecondary, this, context.Context));

            // Headline
            ContentHeadlinePrologue.AddRange(ComponentManager.CacheComponent<IControl>(context.Application, Section.HeadlinePrologue, this, context.Context));
            ContentHeadlinePreferences.AddRange(ComponentManager.CacheComponent<IControl>(context.Application, Section.HeadlinePreferences, this, context.Context));
            ContentHeadlinePrimary.AddRange(ComponentManager.CacheComponent<IControl>(context.Application, Section.HeadlinePrimary, this, context.Context));
            ContentHeadlineSecondary.AddRange(ComponentManager.CacheComponent<IControl>(context.Application, Section.HeadlineSecondary, this, context.Context));
            ContentHeadlineMorePreferences.AddRange(ComponentManager.CacheComponent<IControlDropdownItem>(context.Application, Section.MorePreferences, this, context.Context));
            ContentHeadlineMorePrimary.AddRange(ComponentManager.CacheComponent<IControlDropdownItem>(context.Application, Section.MorePrimary, this, context.Context));
            ContentHeadlineMoreSecondary.AddRange(ComponentManager.CacheComponent<IControlDropdownItem>(context.Application, Section.MoreSecondary, this, context.Context));
            ContentHeadlineMetadata.AddRange(ComponentManager.CacheComponent<IControl>(context.Application, Section.Metadata, this, context.Context));

            // Property
            ContentPropertyPreferences.AddRange(ComponentManager.CacheComponent<IControl>(context.Application, Section.PropertyPreferences, this, context.Context));
            ContentPropertyPrimary.AddRange(ComponentManager.CacheComponent<IControl>(context.Application, Section.PropertyPrimary, this, context.Context));
            ContentPropertySecondary.AddRange(ComponentManager.CacheComponent<IControl>(context.Application, Section.PropertySecondary, this, context.Context));

            // Inhalt
            ContentPreferences.AddRange(ComponentManager.CacheComponent<IControl>(context.Application, Section.ContentPreferences, this, context.Context));
            ContentPrimary.AddRange(ComponentManager.CacheComponent<IControl>(context.Application, Section.ContentPrimary, this, context.Context));
            ContentSecondary.AddRange(ComponentManager.CacheComponent<IControl>(context.Application, Section.ContentSecondary, this, context.Context));

            // Footer
            FooterPreferences.AddRange(ComponentManager.CacheComponent<IControl>(context.Application, Section.FooterPreferences, this, context.Context));
            FooterPrimary.AddRange(ComponentManager.CacheComponent<IControl>(context.Application, Section.FooterPrimary, this, context.Context));
            FooterSecondary.AddRange(ComponentManager.CacheComponent<IControl>(context.Application, Section.FooterSecondary, this, context.Context));
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            context.VisualTree.Favicons.Add(new Favicon(context.Application?.Icon));
            context.VisualTree.Header.Logo = context.Application?.Icon;
            context.VisualTree.Header.Title = this.I18N(context.Application, context.Application?.ApplicationName);

            context.VisualTree.Breadcrumb.Uri = context.Uri;

            // Header
            context.VisualTree.Header.HamburgerPrimary.AddRange(HeaderHamburgerPrimary.Select(x => x.CreateInstance(this, context.Request) as IControlDropdownItem));
            context.VisualTree.Header.HamburgerSecondary.AddRange(HeaderHamburgerSecondary.Select(x => x.CreateInstance(this, context.Request) as IControlDropdownItem));
            context.VisualTree.Header.NavigationPreferences.AddRange(HeaderNavigationPreferences.Select(x => x.CreateInstance(this, context.Request) as IControlNavigationItem));
            context.VisualTree.Header.NavigationPrimary.AddRange(HeaderNavigationPrimary.Select(x => x.CreateInstance(this, context.Request) as IControlNavigationItem));
            context.VisualTree.Header.NavigationSecondary.AddRange(HeaderNavigationSecondary.Select(x => x.CreateInstance(this, context.Request) as IControlNavigationItem));
            context.VisualTree.Header.QuickCreatePrimary.AddRange(HeaderQuickCreatePrimary.Select(x => x.CreateInstance(this, context.Request) as IControlSplitButtonItem));
            context.VisualTree.Header.QuickCreateSecondary.AddRange(HeaderQuickCreateSecondary.Select(x => x.CreateInstance(this, context.Request) as IControlSplitButtonItem));
            context.VisualTree.Header.HelpPreferences.AddRange(HeaderHelpPreferences.Select(x => x.CreateInstance(this, context.Request) as IControlDropdownItem));
            context.VisualTree.Header.HelpPrimary.AddRange(HeaderHelpPrimary.Select(x => x.CreateInstance(this, context.Request) as IControlDropdownItem));
            context.VisualTree.Header.HelpSecondary.AddRange(HeaderHelpSecondary.Select(x => x.CreateInstance(this, context.Request) as IControlDropdownItem));
            context.VisualTree.Header.SettingsPrimary.AddRange(HeaderSettingsPrimary.Select(x => x.CreateInstance(this, context.Request) as IControlDropdownItem));
            context.VisualTree.Header.SettingsSecondary.AddRange(HeaderSettingsSecondary.Select(x => x.CreateInstance(this, context.Request) as IControlDropdownItem));

            // Sidebar
            context.VisualTree.Sidebar.Header.AddRange(SidebarHeader.Select(x => x.CreateInstance(this, context.Request) as IControl));
            context.VisualTree.Sidebar.Preferences.AddRange(SidebarPreferences.Select(x => x.CreateInstance(this, context.Request) as IControl));
            context.VisualTree.Sidebar.Primary.AddRange(SidebarPrimary.Select(x => x.CreateInstance(this, context.Request) as IControl));
            context.VisualTree.Sidebar.Secondary.AddRange(SidebarSecondary.Select(x => x.CreateInstance(this, context.Request) as IControl));

            // Headline
            context.VisualTree.Content.Headline.Prologue.AddRange(ContentHeadlinePrologue.Select(x => x.CreateInstance(this, context.Request) as IControl));
            context.VisualTree.Content.Headline.Preferences.AddRange(ContentHeadlinePreferences.Select(x => x.CreateInstance(this, context.Request) as IControl));
            context.VisualTree.Content.Headline.Primary.AddRange(ContentHeadlinePrimary.Select(x => x.CreateInstance(this, context.Request) as IControl));
            context.VisualTree.Content.Headline.Secondary.AddRange(ContentHeadlineSecondary.Select(x => x.CreateInstance(this, context.Request) as IControl));
            context.VisualTree.Content.Headline.MorePreferences.AddRange(ContentHeadlineMorePreferences.Select(x => x.CreateInstance(this, context.Request) as IControlDropdownItem));
            context.VisualTree.Content.Headline.MorePrimary.AddRange(ContentHeadlineMorePrimary.Select(x => x.CreateInstance(this, context.Request) as IControlDropdownItem));
            context.VisualTree.Content.Headline.MoreSecondary.AddRange(ContentHeadlineMoreSecondary.Select(x => x.CreateInstance(this, context.Request) as IControlDropdownItem));
            context.VisualTree.Content.Headline.Metadata.AddRange(ContentHeadlineMetadata.Select(x => x.CreateInstance(this, context.Request) as IControl));

            // Property
            context.VisualTree.Content.Property.Preferences.AddRange(ContentPropertyPreferences.Select(x => x.CreateInstance(this, context.Request) as IControl));
            context.VisualTree.Content.Property.Primary.AddRange(ContentPropertyPrimary.Select(x => x.CreateInstance(this, context.Request) as IControl));
            context.VisualTree.Content.Property.Secondary.AddRange(ContentPropertySecondary.Select(x => x.CreateInstance(this, context.Request) as IControl));

            // Inhalt
            context.VisualTree.Content.Preferences.AddRange(ContentPreferences.Select(x => x.CreateInstance(this, context.Request) as IControl));
            context.VisualTree.Content.Primary.AddRange(ContentPrimary.Select(x => x.CreateInstance(this, context.Request) as IControl));
            context.VisualTree.Content.Secondary.AddRange(ContentSecondary.Select(x => x.CreateInstance(this, context.Request) as IControl));

            // Footer
            context.VisualTree.Footer.Preferences.AddRange(FooterPreferences.Select(x => x.CreateInstance(this, context.Request) as IControl));
            context.VisualTree.Footer.Primary.AddRange(FooterPrimary.Select(x => x.CreateInstance(this, context.Request) as IControl));
            context.VisualTree.Footer.Secondary.AddRange(FooterSecondary.Select(x => x.CreateInstance(this, context.Request) as IControl));

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
