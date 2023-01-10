using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
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
        private List<ComponentCacheItem> HeaderAppNavigatorPreferences { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> HeaderAppNavigatorPrimary { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> HeaderAppNavigatorSecondary { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> HeaderNavigationPreferences { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> HeaderNavigationPrimary { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> HeaderNavigationSecondary { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> HeaderQuickCreatePreferences { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> HeaderQuickCreatePrimary { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> HeaderQuickCreateSecondary { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> HeaderHelpPreferences { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> HeaderHelpPrimary { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> HeaderHelpSecondary { get; } = new List<ComponentCacheItem>();
        private List<ComponentCacheItem> HeaderSettingsPreferences { get; } = new List<ComponentCacheItem>();
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
        /// Constructor
        /// </summary>
        public PageWebApp()
        {
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);

            var module = ModuleManager.GetModule(ApplicationContext, "webexpress.webapp");
            if (module != null)
            {
                CssLinks.Add(new UriResource(module.GetContextPath(ApplicationContext), new UriRelative("/assets/css/webexpress.webapp.css")));
                CssLinks.Add(new UriResource(module.GetContextPath(ApplicationContext), new UriRelative("/assets/css/webexpress.webapp.popupnotification.css")));
                CssLinks.Add(new UriResource(module.GetContextPath(ApplicationContext), new UriRelative("/assets/css/webexpress.webapp.taskprogressbar.css")));
                HeaderScriptLinks.Add(module.GetContextPath(ApplicationContext).Append("assets/js/webexpress.webapp.js"));
                HeaderScriptLinks.Add(module.GetContextPath(ApplicationContext).Append("assets/js/webexpress.webapp.popupnotification.js"));
                HeaderScriptLinks.Add(module.GetContextPath(ApplicationContext).Append("assets/js/webexpress.webapp.selection.js"));
                HeaderScriptLinks.Add(module.GetContextPath(ApplicationContext).Append("assets/js/webexpress.webapp.table.js"));
                HeaderScriptLinks.Add(module.GetContextPath(ApplicationContext).Append("assets/js/webexpress.webapp.taskprogressbar.js"));
            }

            // Header
            HeaderAppNavigatorPreferences.AddRange(ComponentManager.CacheComponent<IControlDropdownItem>(Section.AppPreferences, this, context.ResourceContextFilter));
            HeaderAppNavigatorPrimary.AddRange(ComponentManager.CacheComponent<IControlDropdownItem>(Section.AppPrimary, this, context.ResourceContextFilter));
            HeaderAppNavigatorSecondary.AddRange(ComponentManager.CacheComponent<IControlDropdownItem>(Section.AppSecondary, this, context.ResourceContextFilter));
            HeaderNavigationPreferences.AddRange(ComponentManager.CacheComponent<IControlNavigationItem>(Section.AppNavigationPreferences, this, context.ResourceContextFilter));
            HeaderNavigationPrimary.AddRange(ComponentManager.CacheComponent<IControlNavigationItem>(Section.AppNavigationPrimary, this, context.ResourceContextFilter));
            HeaderNavigationSecondary.AddRange(ComponentManager.CacheComponent<IControlNavigationItem>(Section.AppNavigationSecondary, this, context.ResourceContextFilter));
            HeaderQuickCreatePreferences.AddRange(ComponentManager.CacheComponent<IControlSplitButtonItem>(Section.AppQuickcreatePreferences, this, context.ResourceContextFilter));
            HeaderQuickCreatePrimary.AddRange(ComponentManager.CacheComponent<IControlSplitButtonItem>(Section.AppQuickcreatePrimary, this, context.ResourceContextFilter));
            HeaderQuickCreateSecondary.AddRange(ComponentManager.CacheComponent<IControlSplitButtonItem>(Section.AppQuickcreateSecondary, this, context.ResourceContextFilter));
            HeaderHelpPreferences.AddRange(ComponentManager.CacheComponent<IControlDropdownItem>(Section.AppHelpPreferences, this, context.ResourceContextFilter));
            HeaderHelpPrimary.AddRange(ComponentManager.CacheComponent<IControlDropdownItem>(Section.AppHelpPrimary, this, context.ResourceContextFilter));
            HeaderHelpSecondary.AddRange(ComponentManager.CacheComponent<IControlDropdownItem>(Section.AppHelpSecondary, this, context.ResourceContextFilter));
            HeaderSettingsPrimary.AddRange(ComponentManager.CacheComponent<IControlDropdownItem>(Section.AppSettingsPrimary, this, context.ResourceContextFilter));
            HeaderSettingsSecondary.AddRange(ComponentManager.CacheComponent<IControlDropdownItem>(Section.AppSettingsSecondary, this, context.ResourceContextFilter));

            // Sidebar
            SidebarHeader.AddRange(ComponentManager.CacheComponent<IControl>(Section.SidebarHeader, this, context.ResourceContextFilter));
            SidebarPreferences.AddRange(ComponentManager.CacheComponent<IControl>(Section.SidebarPreferences, this, context.ResourceContextFilter));
            SidebarPrimary.AddRange(ComponentManager.CacheComponent<IControl>(Section.SidebarPrimary, this, context.ResourceContextFilter));
            SidebarSecondary.AddRange(ComponentManager.CacheComponent<IControl>(Section.SidebarSecondary, this, context.ResourceContextFilter));

            // Headline
            ContentHeadlinePrologue.AddRange(ComponentManager.CacheComponent<IControl>(Section.HeadlinePrologue, this, context.ResourceContextFilter));
            ContentHeadlinePreferences.AddRange(ComponentManager.CacheComponent<IControl>(Section.HeadlinePreferences, this, context.ResourceContextFilter));
            ContentHeadlinePrimary.AddRange(ComponentManager.CacheComponent<IControl>(Section.HeadlinePrimary, this, context.ResourceContextFilter));
            ContentHeadlineSecondary.AddRange(ComponentManager.CacheComponent<IControl>(Section.HeadlineSecondary, this, context.ResourceContextFilter));
            ContentHeadlineMorePreferences.AddRange(ComponentManager.CacheComponent<IControlDropdownItem>(Section.MorePreferences, this, context.ResourceContextFilter));
            ContentHeadlineMorePrimary.AddRange(ComponentManager.CacheComponent<IControlDropdownItem>(Section.MorePrimary, this, context.ResourceContextFilter));
            ContentHeadlineMoreSecondary.AddRange(ComponentManager.CacheComponent<IControlDropdownItem>(Section.MoreSecondary, this, context.ResourceContextFilter));
            ContentHeadlineMetadata.AddRange(ComponentManager.CacheComponent<IControl>(Section.Metadata, this, context.ResourceContextFilter));

            // Property
            ContentPropertyPreferences.AddRange(ComponentManager.CacheComponent<IControl>(Section.PropertyPreferences, this, context.ResourceContextFilter));
            ContentPropertyPrimary.AddRange(ComponentManager.CacheComponent<IControl>(Section.PropertyPrimary, this, context.ResourceContextFilter));
            ContentPropertySecondary.AddRange(ComponentManager.CacheComponent<IControl>(Section.PropertySecondary, this, context.ResourceContextFilter));

            // Inhalt
            ContentPreferences.AddRange(ComponentManager.CacheComponent<IControl>(Section.ContentPreferences, this, context.ResourceContextFilter));
            ContentPrimary.AddRange(ComponentManager.CacheComponent<IControl>(Section.ContentPrimary, this, context.ResourceContextFilter));
            ContentSecondary.AddRange(ComponentManager.CacheComponent<IControl>(Section.ContentSecondary, this, context.ResourceContextFilter));

            // Footer
            FooterPreferences.AddRange(ComponentManager.CacheComponent<IControl>(Section.FooterPreferences, this, context.ResourceContextFilter));
            FooterPrimary.AddRange(ComponentManager.CacheComponent<IControl>(Section.FooterPrimary, this, context.ResourceContextFilter));
            FooterSecondary.AddRange(ComponentManager.CacheComponent<IControl>(Section.FooterSecondary, this, context.ResourceContextFilter));
        }

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            context.VisualTree.Favicons.Add(new Favicon(context.Application?.Icon));
            //context.VisualTree.Header.Logo = context.Application?.Icon;
            //context.VisualTree.Header.AppTitle.Title = this.I18N(context.Application, context.Application?.ApplicationName);

            context.VisualTree.Breadcrumb.Uri = context.Uri;

            // Header
            context.VisualTree.Header.AppNavigator.Preferences.AddRange(HeaderAppNavigatorPreferences.SelectMany(x => x.CreateInstance<IControlDropdownItem>(this, context.Request)));
            context.VisualTree.Header.AppNavigator.Primary.AddRange(HeaderAppNavigatorPrimary.SelectMany(x => x.CreateInstance<IControlDropdownItem>(this, context.Request)));
            context.VisualTree.Header.AppNavigator.Secondary.AddRange(HeaderAppNavigatorSecondary.SelectMany(x => x.CreateInstance<IControlDropdownItem>(this, context.Request)));
            context.VisualTree.Header.AppNavigation.Preferences.AddRange(HeaderNavigationPreferences.SelectMany(x => x.CreateInstance<IControlNavigationItem>(this, context.Request)));
            context.VisualTree.Header.AppNavigation.Primary.AddRange(HeaderNavigationPrimary.SelectMany(x => x.CreateInstance<IControlNavigationItem>(this, context.Request)));
            context.VisualTree.Header.AppNavigation.Secondary.AddRange(HeaderNavigationSecondary.SelectMany(x => x.CreateInstance<IControlNavigationItem>(this, context.Request)));
            context.VisualTree.Header.QuickCreate.Preferences.AddRange(HeaderQuickCreatePreferences.SelectMany(x => x.CreateInstance<IControlSplitButtonItem>(this, context.Request)));
            context.VisualTree.Header.QuickCreate.Primary.AddRange(HeaderQuickCreatePrimary.SelectMany(x => x.CreateInstance<IControlSplitButtonItem>(this, context.Request)));
            context.VisualTree.Header.QuickCreate.Secondary.AddRange(HeaderQuickCreateSecondary.SelectMany(x => x.CreateInstance<IControlSplitButtonItem>(this, context.Request)));
            context.VisualTree.Header.Help.Preferences.AddRange(HeaderHelpPreferences.SelectMany(x => x.CreateInstance<IControlDropdownItem>(this, context.Request)));
            context.VisualTree.Header.Help.Primary.AddRange(HeaderHelpPrimary.SelectMany(x => x.CreateInstance<IControlDropdownItem>(this, context.Request)));
            context.VisualTree.Header.Help.Secondary.AddRange(HeaderHelpSecondary.SelectMany(x => x.CreateInstance<IControlDropdownItem>(this, context.Request)));
            context.VisualTree.Header.Settings.Preferences.AddRange(HeaderSettingsPreferences.SelectMany(x => x.CreateInstance<IControlDropdownItem>(this, context.Request)));
            context.VisualTree.Header.Settings.Primary.AddRange(HeaderSettingsPrimary.SelectMany(x => x.CreateInstance<IControlDropdownItem>(this, context.Request)));
            context.VisualTree.Header.Settings.Secondary.AddRange(HeaderSettingsSecondary.SelectMany(x => x.CreateInstance<IControlDropdownItem>(this, context.Request)));

            // Sidebar
            context.VisualTree.Sidebar.Header.AddRange(SidebarHeader.SelectMany(x => x.CreateInstance<IControl>(this, context.Request)));
            context.VisualTree.Sidebar.Preferences.AddRange(SidebarPreferences.SelectMany(x => x.CreateInstance<IControl>(this, context.Request)));
            context.VisualTree.Sidebar.Primary.AddRange(SidebarPrimary.SelectMany(x => x.CreateInstance<IControl>(this, context.Request)));
            context.VisualTree.Sidebar.Secondary.AddRange(SidebarSecondary.SelectMany(x => x.CreateInstance<IControl>(this, context.Request)));

            // Headline
            context.VisualTree.Content.Headline.Prologue.AddRange(ContentHeadlinePrologue.SelectMany(x => x.CreateInstance<IControl>(this, context.Request)));
            context.VisualTree.Content.Headline.Preferences.AddRange(ContentHeadlinePreferences.SelectMany(x => x.CreateInstance<IControl>(this, context.Request)));
            context.VisualTree.Content.Headline.Primary.AddRange(ContentHeadlinePrimary.SelectMany(x => x.CreateInstance<IControl>(this, context.Request)));
            context.VisualTree.Content.Headline.Secondary.AddRange(ContentHeadlineSecondary.SelectMany(x => x.CreateInstance<IControl>(this, context.Request)));
            context.VisualTree.Content.Headline.MorePreferences.AddRange(ContentHeadlineMorePreferences.SelectMany(x => x.CreateInstance<IControlDropdownItem>(this, context.Request)));
            context.VisualTree.Content.Headline.MorePrimary.AddRange(ContentHeadlineMorePrimary.SelectMany(x => x.CreateInstance<IControlDropdownItem>(this, context.Request)));
            context.VisualTree.Content.Headline.MoreSecondary.AddRange(ContentHeadlineMoreSecondary.SelectMany(x => x.CreateInstance<IControlDropdownItem>(this, context.Request)));
            context.VisualTree.Content.Headline.Metadata.AddRange(ContentHeadlineMetadata.SelectMany(x => x.CreateInstance<IControl>(this, context.Request)));

            // Property
            context.VisualTree.Content.Property.Preferences.AddRange(ContentPropertyPreferences.SelectMany(x => x.CreateInstance<IControl>(this, context.Request)));
            context.VisualTree.Content.Property.Primary.AddRange(ContentPropertyPrimary.SelectMany(x => x.CreateInstance<IControl>(this, context.Request)));
            context.VisualTree.Content.Property.Secondary.AddRange(ContentPropertySecondary.SelectMany(x => x.CreateInstance<IControl>(this, context.Request)));

            // Inhalt
            context.VisualTree.Content.Preferences.AddRange(ContentPreferences.SelectMany(x => x.CreateInstance<IControl>(this, context.Request)));
            context.VisualTree.Content.Primary.AddRange(ContentPrimary.SelectMany(x => x.CreateInstance<IControl>(this, context.Request)));
            context.VisualTree.Content.Secondary.AddRange(ContentSecondary.SelectMany(x => x.CreateInstance<IControl>(this, context.Request)));

            // Footer
            context.VisualTree.Footer.Preferences.AddRange(FooterPreferences.SelectMany(x => x.CreateInstance<IControl>(this, context.Request)));
            context.VisualTree.Footer.Primary.AddRange(FooterPrimary.SelectMany(x => x.CreateInstance<IControl>(this, context.Request)));
            context.VisualTree.Footer.Secondary.AddRange(FooterSecondary.SelectMany(x => x.CreateInstance<IControl>(this, context.Request)));

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
