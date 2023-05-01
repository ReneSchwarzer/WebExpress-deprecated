using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.UI.WebFragment;
using WebExpress.UI.WebPage;
using WebExpress.WebApp.WebApiControl;
using WebExpress.WebApp.WebFragment;
using WebExpress.WebComponent;
using WebExpress.WebResource;
using WebExpress.WebUri;

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

            var fm = ComponentManager.GetComponent<FragmentManager>();
            var module = ComponentManager.ModuleManager.GetModule(ApplicationContext, "webexpress.webapp");
            if (module != null)
            {
                CssLinks.Add(UriResource.Combine(module.ContextPath, "/assets/css/webexpress.webapp.css"));
                CssLinks.Add(UriResource.Combine(module.ContextPath, "/assets/css/webexpress.webapp.popupnotification.css"));
                CssLinks.Add(UriResource.Combine(module.ContextPath, "/assets/css/webexpress.webapp.taskprogressbar.css"));
                HeaderScriptLinks.Add(UriResource.Combine(module.ContextPath, "assets/js/webexpress.webapp.js"));
                HeaderScriptLinks.Add(UriResource.Combine(module.ContextPath, "assets/js/webexpress.webapp.popupnotification.js"));
                HeaderScriptLinks.Add(UriResource.Combine(module.ContextPath, "assets/js/webexpress.webapp.selection.js"));
                HeaderScriptLinks.Add(UriResource.Combine(module.ContextPath, "assets/js/webexpress.webapp.table.js"));
                HeaderScriptLinks.Add(UriResource.Combine(module.ContextPath, "assets/js/webexpress.webapp.taskprogressbar.js"));
            }

            // Header
            HeaderAppNavigatorPreferences.AddRange(fm.GetCacheableFragments<IControlDropdownItem>(Section.AppPreferences, this, context.Context));
            HeaderAppNavigatorPrimary.AddRange(fm.GetCacheableFragments<IControlDropdownItem>(Section.AppPrimary, this, context.Context));
            HeaderAppNavigatorSecondary.AddRange(fm.GetCacheableFragments<IControlDropdownItem>(Section.AppSecondary, this, context.Context));
            HeaderNavigationPreferences.AddRange(fm.GetCacheableFragments<IControlNavigationItem>(Section.AppNavigationPreferences, this, context.Context));
            HeaderNavigationPrimary.AddRange(fm.GetCacheableFragments<IControlNavigationItem>(Section.AppNavigationPrimary, this, context.Context));
            HeaderNavigationSecondary.AddRange(fm.GetCacheableFragments<IControlNavigationItem>(Section.AppNavigationSecondary, this, context.Context));
            HeaderQuickCreatePreferences.AddRange(fm.GetCacheableFragments<IControlSplitButtonItem>(Section.AppQuickcreatePreferences, this, context.Context));
            HeaderQuickCreatePrimary.AddRange(fm.GetCacheableFragments<IControlSplitButtonItem>(Section.AppQuickcreatePrimary, this, context.Context));
            HeaderQuickCreateSecondary.AddRange(fm.GetCacheableFragments<IControlSplitButtonItem>(Section.AppQuickcreateSecondary, this, context.Context));
            HeaderHelpPreferences.AddRange(fm.GetCacheableFragments<IControlDropdownItem>(Section.AppHelpPreferences, this, context.Context));
            HeaderHelpPrimary.AddRange(fm.GetCacheableFragments<IControlDropdownItem>(Section.AppHelpPrimary, this, context.Context));
            HeaderHelpSecondary.AddRange(fm.GetCacheableFragments<IControlDropdownItem>(Section.AppHelpSecondary, this, context.Context));
            HeaderSettingsPrimary.AddRange(fm.GetCacheableFragments<IControlDropdownItem>(Section.AppSettingsPrimary, this, context.Context));
            HeaderSettingsSecondary.AddRange(fm.GetCacheableFragments<IControlDropdownItem>(Section.AppSettingsSecondary, this, context.Context));

            // Sidebar
            SidebarHeader.AddRange(fm.GetCacheableFragments<IControl>(Section.SidebarHeader, this, context.Context));
            SidebarPreferences.AddRange(fm.GetCacheableFragments<IControl>(Section.SidebarPreferences, this, context.Context));
            SidebarPrimary.AddRange(fm.GetCacheableFragments<IControl>(Section.SidebarPrimary, this, context.Context));
            SidebarSecondary.AddRange(fm.GetCacheableFragments<IControl>(Section.SidebarSecondary, this, context.Context));

            // Headline
            ContentHeadlinePrologue.AddRange(fm.GetCacheableFragments<IControl>(Section.HeadlinePrologue, this, context.Context));
            ContentHeadlinePreferences.AddRange(fm.GetCacheableFragments<IControl>(Section.HeadlinePreferences, this, context.Context));
            ContentHeadlinePrimary.AddRange(fm.GetCacheableFragments<IControl>(Section.HeadlinePrimary, this, context.Context));
            ContentHeadlineSecondary.AddRange(fm.GetCacheableFragments<IControl>(Section.HeadlineSecondary, this, context.Context));
            ContentHeadlineMorePreferences.AddRange(fm.GetCacheableFragments<IControlDropdownItem>(Section.MorePreferences, this, context.Context));
            ContentHeadlineMorePrimary.AddRange(fm.GetCacheableFragments<IControlDropdownItem>(Section.MorePrimary, this, context.Context));
            ContentHeadlineMoreSecondary.AddRange(fm.GetCacheableFragments<IControlDropdownItem>(Section.MoreSecondary, this, context.Context));
            ContentHeadlineMetadata.AddRange(fm.GetCacheableFragments<IControl>(Section.Metadata, this, context.Context));

            // Property
            ContentPropertyPreferences.AddRange(fm.GetCacheableFragments<IControl>(Section.PropertyPreferences, this, context.Context));
            ContentPropertyPrimary.AddRange(fm.GetCacheableFragments<IControl>(Section.PropertyPrimary, this, context.Context));
            ContentPropertySecondary.AddRange(fm.GetCacheableFragments<IControl>(Section.PropertySecondary, this, context.Context));

            // Inhalt
            ContentPreferences.AddRange(fm.GetCacheableFragments<IControl>(Section.ContentPreferences, this, context.Context));
            ContentPrimary.AddRange(fm.GetCacheableFragments<IControl>(Section.ContentPrimary, this, context.Context));
            ContentSecondary.AddRange(fm.GetCacheableFragments<IControl>(Section.ContentSecondary, this, context.Context));

            // Footer
            FooterPreferences.AddRange(fm.GetCacheableFragments<IControl>(Section.FooterPreferences, this, context.Context));
            FooterPrimary.AddRange(fm.GetCacheableFragments<IControl>(Section.FooterPrimary, this, context.Context));
            FooterSecondary.AddRange(fm.GetCacheableFragments<IControl>(Section.FooterSecondary, this, context.Context));
        }

        /// <summary>
        /// Processing of the resource.
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            context.VisualTree.Favicons.Add(new Favicon(context.ApplicationContext?.Icon));
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
