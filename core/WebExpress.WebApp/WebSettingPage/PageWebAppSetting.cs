using System.Linq;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.SettingPage;
using WebExpress.WebApp.WebAttribute;
using WebExpress.WebApp.WebPage;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebSettingPage
{
    /// <summary>
    /// Seite, welche für Einstellungen verwendet werden kann
    /// </summary>
    public abstract class PageWebAppSetting : PageWebApp, IPageSetting
    {
        /// <summary>
        /// Das Symbol der Einstellungsseite
        /// </summary>
        public PropertyIcon Icon { get; set; }

        /// <summary>
        /// Liefert das Menü, indem Einstellungsseiten aufgelistet sind
        /// </summary>
        public ControlNavigation SettingMenu { get; } = new ControlNavigation("settingmenu")
        {
            Layout = TypeLayoutTab.Pill,
            Orientation = TypeOrientationTab.Vertical,
            GridColumn = new PropertyGrid(TypeDevice.Medium, 2)
        };

        /// <summary>
        /// Liefert das Tab-Control, indem Einstellungsseiten aufgelistet sind
        /// </summary>
        public ControlNavigation SettingTab = new ControlNavigation("settingtab")
        {
            Layout = TypeLayoutTab.Tab,
            Orientation = TypeOrientationTab.Horizontal
        };

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            SettingMenu.Items.Clear();
            SettingTab.Items.Clear();

            var path = SettingPageManager.FindPage(Context.Application.ApplicationID, ID);
            if (path != null)
            {
                var contexts = SettingPageManager.GetContexts(Context.Application.ApplicationID);
                var section = SettingPageManager.GetSections(Context.Application.ApplicationID, path?.Context);

                // SettingMenü
                AddSettingMenu(section, SettingSection.Preferences, SettingMenu, context);
                AddSettingMenu(section, SettingSection.Primary, SettingMenu, context);
                AddSettingMenu(section, SettingSection.Secondary, SettingMenu, context);

                // SettingTab
                foreach (var settingContext in contexts.Select(x => new { Name = x.Key, ContextName = x.Key, Sections = x.Value }).Where(x => x.Name != "*").OrderBy(x => x.Name))
                {
                    var firstPage = settingContext.Sections.FindFirstPage();

                    if (firstPage != null)
                    {
                        SettingTab.Items.Add(new ControlNavigationItemLink()
                        {
                            Uri = new UriResource(firstPage.Page.ModuleContext.ContextPath, firstPage.Page.Node?.ExpressionPath),
                            Text = settingContext.Name,
                            Active = path.Context == settingContext.ContextName ? TypeActive.Active : TypeActive.None,
                        });
                    }
                }
            }

            if (SettingMenu.Items.Count > 1)
            {
                context.VisualTree.Sidebar.Primary.Add(SettingMenu);
            }

            if (SettingTab.Items.Count > 1)
            {
                context.VisualTree.Prologue.Content.Add(SettingTab);
            }

            context.VisualTree.Breadcrumb.Prefix = "webexpress.webapp:setting.label";
            context.VisualTree.Breadcrumb.TakeLast = 1;

            base.Process(context);
        }

        /// <summary>
        /// Fügt die Links der Einstellungsseiten ein
        /// </summary>
        /// <param name="sections">Die Sektionen</param>
        /// <param name="section">Die ausgewählte Sektion</param>
        /// <param name="control">Die Sektion, welche eingefügt werden soll</param>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        private void AddSettingMenu(SettingPageDictionaryItemSection sections, SettingSection section, ControlNavigation control, RenderContextWebApp context)
        {
            var groups = sections.GetGroups(section);

            if (groups == null)
            {
                return;
            }

            foreach (var group in groups.Select(x => new { Name = x.Key, Pages = x.Value }).OrderBy(x => x.Name))
            {
                control.Items.Add(new ControlNavigationItemHeader() { Text = group.Name });

                foreach (var page in group.Pages)
                {
                    if (!page.Hide && (!page.Node.Context.Conditions.Any() || page.Node.Context.Conditions.All(x => x.Fulfillment(context.Request))))
                    {
                        control.Items.Add(new ControlNavigationItemLink()
                        {
                            Text = page.Node?.Title,
                            Icon = page.Icon,
                            Uri = new UriResource(page.ModuleContext.ContextPath, page.Node?.ExpressionPath),
                            Active = page.ID.Equals(ID, System.StringComparison.OrdinalIgnoreCase) ? TypeActive.Active : TypeActive.None,
                            NoWrap = true
                        });
                    }
                }

            }
        }
    }
}
