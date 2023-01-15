using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebFragment;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebPage;
using WebExpress.Uri;
using WebExpress.WebApp.WebApiControl;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebModule;
using WebExpress.WebResource;
using WebExpress.WebComponent;

namespace WebExpress.WebApp.WebPage
{
    /// <summary>
    /// Page consisting of a vertically arranged header, content and footer area.
    /// </summary>
    public abstract class PageWebApp : PageControl<RenderContextWebApp>
    {
        // Header
        private List<FragmentCacheItem> HeaderAppNavigatorPreferences { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> HeaderAppNavigatorPrimary { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> HeaderAppNavigatorSecondary { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> HeaderNavigationPreferences { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> HeaderNavigationPrimary { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> HeaderNavigationSecondary { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> HeaderQuickCreatePreferences { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> HeaderQuickCreatePrimary { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> HeaderQuickCreateSecondary { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> HeaderHelpPreferences { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> HeaderHelpPrimary { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> HeaderHelpSecondary { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> HeaderSettingsPreferences { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> HeaderSettingsPrimary { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> HeaderSettingsSecondary { get; } = new List<FragmentCacheItem>();

        // Sidebar
        private List<FragmentCacheItem> SidebarHeader { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> SidebarPreferences { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> SidebarPrimary { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> SidebarSecondary { get; } = new List<FragmentCacheItem>();

        // Headline
        private List<FragmentCacheItem> ContentHeadlinePrologue { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> ContentHeadlinePreferences { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> ContentHeadlinePrimary { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> ContentHeadlineSecondary { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> ContentHeadlineMorePreferences { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> ContentHeadlineMorePrimary { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> ContentHeadlineMoreSecondary { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> ContentHeadlineMetadata { get; } = new List<FragmentCacheItem>();

        // Property
        private List<FragmentCacheItem> ContentPropertyPreferences { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> ContentPropertyPrimary { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> ContentPropertySecondary { get; } = new List<FragmentCacheItem>();

        // Inhalt
        private List<FragmentCacheItem> ContentPreferences { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> ContentPrimary { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> ContentSecondary { get; } = new List<FragmentCacheItem>();

        // Footer
        private List<FragmentCacheItem> FooterPreferences { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> FooterPrimary { get; } = new List<FragmentCacheItem>();
        private List<FragmentCacheItem> FooterSecondary { get; } = new List<FragmentCacheItem>();

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

            var fm = ComponentManager.GetManager<FragmentManager>();
            var module = ComponentManager.ModuleManager.GetModule(ApplicationContext, "webexpress.webapp");
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
            HeaderAppNavigatorPreferences.AddRange(fm.CacheComponent<IControlDropdownItem>(Section.AppPreferences, this, context.ContentContext));
            HeaderAppNavigatorPrimary.AddRange(fm.CacheComponent<IControlDropdownItem>(Section.AppPrimary, this, context.ContentContext));
            HeaderAppNavigatorSecondary.AddRange(fm.CacheComponent<IControlDropdownItem>(Section.AppSecondary, this, context.ContentContext));
            HeaderNavigationPreferences.AddRange(fm.CacheComponent<IControlNavigationItem>(Section.AppNavigationPreferences, this, context.ContentContext));
            HeaderNavigationPrimary.AddRange(fm.CacheComponent<IControlNavigationItem>(Section.AppNavigationPrimary, this, context.ContentContext));
            HeaderNavigationSecondary.AddRange(fm.CacheComponent<IControlNavigationItem>(Section.AppNavigationSecondary, this, context.ContentContext));
            HeaderQuickCreatePreferences.AddRange(fm.CacheComponent<IControlSplitButtonItem>(Section.AppQuickcreatePreferences, this, context.ContentContext));
            HeaderQuickCreatePrimary.AddRange(fm.CacheComponent<IControlSplitButtonItem>(Section.AppQuickcreatePrimary, this, context.ContentContext));
            HeaderQuickCreateSecondary.AddRange(fm.CacheComponent<IControlSplitButtonItem>(Section.AppQuickcreateSecondary, this, context.ContentContext));
            HeaderHelpPreferences.AddRange(fm.CacheComponent<IControlDropdownItem>(Section.AppHelpPreferences, this, context.ContentContext));
            HeaderHelpPrimary.AddRange(fm.CacheComponent<IControlDropdownItem>(Section.AppHelpPrimary, this, context.ContentContext));
            HeaderHelpSecondary.AddRange(fm.CacheComponent<IControlDropdownItem>(Section.AppHelpSecondary, this, context.ContentContext));
            HeaderSettingsPrimary.AddRange(fm.CacheComponent<IControlDropdownItem>(Section.AppSettingsPrimary, this, context.ContentContext));
            HeaderSettingsSecondary.AddRange(fm.CacheComponent<IControlDropdownItem>(Section.AppSettingsSecondary, this, context.ContentContext));

            // Sidebar
            SidebarHeader.AddRange(fm.CacheComponent<IControl>(Section.SidebarHeader, this, context.ContentContext));
            SidebarPreferences.AddRange(fm.CacheComponent<IControl>(Section.SidebarPreferences, this, context.ContentContext));
            SidebarPrimary.AddRange(fm.CacheComponent<IControl>(Section.SidebarPrimary, this, context.ContentContext));
            SidebarSecondary.AddRange(fm.CacheComponent<IControl>(Section.SidebarSecondary, this, context.ContentContext));

            // Headline
            ContentHeadlinePrologue.AddRange(fm.CacheComponent<IControl>(Section.HeadlinePrologue, this, context.ContentContext));
            ContentHeadlinePreferences.AddRange(fm.CacheComponent<IControl>(Section.HeadlinePreferences, this, context.ContentContext));
            ContentHeadlinePrimary.AddRange(fm.CacheComponent<IControl>(Section.HeadlinePrimary, this, context.ContentContext));
            ContentHeadlineSecondary.AddRange(fm.CacheComponent<IControl>(Section.HeadlineSecondary, this, context.ContentContext));
            ContentHeadlineMorePreferences.AddRange(fm.CacheComponent<IControlDropdownItem>(Section.MorePreferences, this, context.ContentContext));
            ContentHeadlineMorePrimary.AddRange(fm.CacheComponent<IControlDropdownItem>(Section.MorePrimary, this, context.ContentContext));
            ContentHeadlineMoreSecondary.AddRange(fm.CacheComponent<IControlDropdownItem>(Section.MoreSecondary, this, context.ContentContext));
            ContentHeadlineMetadata.AddRange(fm.CacheComponent<IControl>(Section.Metadata, this, context.ContentContext));

            // Property
            ContentPropertyPreferences.AddRange(fm.CacheComponent<IControl>(Section.PropertyPreferences, this, context.ContentContext));
            ContentPropertyPrimary.AddRange(fm.CacheComponent<IControl>(Section.PropertyPrimary, this, context.ContentContext));
            ContentPropertySecondary.AddRange(fm.CacheComponent<IControl>(Section.PropertySecondary, this, context.ContentContext));

            // Inhalt
            ContentPreferences.AddRange(fm.CacheComponent<IControl>(Section.ContentPreferences, this, context.ContentContext));
            ContentPrimary.AddRange(fm.CacheComponent<IControl>(Section.ContentPrimary, this, context.ContentContext));
            ContentSecondary.AddRange(fm.CacheComponent<IControl>(Section.ContentSecondary, this, context.ContentContext));

            // Footer
            FooterPreferences.AddRange(fm.CacheComponent<IControl>(Section.FooterPreferences, this, context.ContentContext));
            FooterPrimary.AddRange(fm.CacheComponent<IControl>(Section.FooterPrimary, this, context.ContentContext));
            FooterSecondary.AddRange(fm.CacheComponent<IControl>(Section.FooterSecondary, this, context.ContentContext));
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
