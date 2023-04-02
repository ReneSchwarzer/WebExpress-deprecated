using WebExpress.UI.WebControl;
using WebExpress.WebApp.SettingPage;
using WebExpress.WebApp.WebAttribute;
using WebExpress.WebApp.WebPage;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebSettingPage
{
    /// <summary>
    /// Page that can be used for settings.
    /// </summary>
    public abstract class PageWebAppSetting : PageWebApp, IPageSetting
    {
        /// <summary>
        /// The settings page icon.
        /// </summary>
        public PropertyIcon Icon { get; set; }

        /// <summary>
        /// Returns the menu by listing settings pages.
        /// </summary>
        public ControlNavigation SettingMenu { get; } = new ControlNavigation("settingmenu")
        {
            Layout = TypeLayoutTab.Pill,
            Orientation = TypeOrientationTab.Vertical,
            GridColumn = new PropertyGrid(TypeDevice.Medium, 2)
        };

        /// <summary>
        /// Returns the tab control by listing settings pages.
        /// </summary>
        public ControlNavigation SettingTab = new ControlNavigation("settingtab")
        {
            Layout = TypeLayoutTab.Tab,
            Orientation = TypeOrientationTab.Horizontal
        };

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="context">The context of the resource.</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Processing of the setting page.
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        public override void Process(RenderContextWebApp context)
        {
            //SettingMenu.Items.Clear();
            //SettingTab.Items.Clear();

            //var searchResult = SettingPageManager.FindPage(ApplicationContext, ModuleContext, ID);
            //if (searchResult != null)
            //{
            //    var contexts = SettingPageManager.GetContexts(ApplicationContext.ApplicationID);
            //    var section = SettingPageManager.GetSections(ApplicationContext.ApplicationID, searchResult?.Context);

            //    // setting menu
            //    AddSettingMenu(section, SettingSection.Preferences, SettingMenu, context);
            //    AddSettingMenu(section, SettingSection.Primary, SettingMenu, context);
            //    AddSettingMenu(section, SettingSection.Secondary, SettingMenu, context);

            //    // setting tab
            //    foreach (var settingContext in contexts.Select(x => new { Name = x.Key, ContextName = x.Key, Sections = x.Value }).Where(x => x.Name != "*").OrderBy(x => x.Name))
            //    {
            //        var firstPage = settingContext.Sections.FindFirstPage();

            //        if (firstPage != null)
            //        {
            //            SettingTab.Items.Add(new ControlNavigationItemLink()
            //            {
            //                Uri = new UriResource(firstPage?.Item?.Resource.Context.GetContextPath(ApplicationContext), firstPage?.Item.Resource?.PathSegment.ToString()),
            //                Text = settingContext.Name,
            //                Active = searchResult.Context == settingContext.ContextName ? TypeActive.Active : TypeActive.None,
            //            });
            //        }
            //    }
            //}

            //if (SettingMenu.Items.Count > 1)
            //{
            //    context.VisualTree.Sidebar.Primary.Add(SettingMenu);
            //}

            //if (SettingTab.Items.Count > 1)
            //{
            //    context.VisualTree.Prologue.Content.Add(SettingTab);
            //}

            //context.VisualTree.Breadcrumb.Prefix = "webexpress.webapp:setting.label";
            //context.VisualTree.Breadcrumb.TakeLast = 1;

            base.Process(context);
        }

        /// <summary>
        /// Inserts the links of the settings pages.
        /// </summary>
        /// <param name="sections">The sections.</param>
        /// <param name="section">The selected section.</param>
        /// <param name="control">The section to be inserted.</param>
        /// <param name="context">The context for rendering the page.</param>
        private void AddSettingMenu(SettingPageDictionaryItemSection sections, SettingSection section, ControlNavigation control, RenderContextWebApp context)
        {
            //var groups = sections?.GetGroups(section);

            //if (groups == null)
            //{
            //    return;
            //}

            //foreach (var group in groups.Select(x => new { Name = x.Key, Pages = x.Value }).OrderBy(x => x.Name))
            //{
            //    control.Items.Add(new ControlNavigationItemHeader() { Text = group.Name });

            //    foreach (var page in group.Pages)
            //    {
            //        if (!page.Hide && (!page.Resource.Context.Conditions.Any() || page.Resource.Context.Conditions.All(x => x.Fulfillment(context.Request))))
            //        {
            //            control.Items.Add(new ControlNavigationItemLink()
            //            {
            //                Text = page.Resource?.Title,
            //                Icon = page.Icon,
            //                Uri = new UriResource(page?.Resource.Context.GetContextPath(ApplicationContext), page?.Resource?.PathSegment.ToString()),
            //                Active = page.ID.Equals(ID, System.StringComparison.OrdinalIgnoreCase) ? TypeActive.Active : TypeActive.None,
            //                NoWrap = true
            //            });
            //        }
            //    }
            //}
        }
    }
}
