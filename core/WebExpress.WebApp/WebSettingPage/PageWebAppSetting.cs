using System.Linq;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.Attribute;
using WebExpress.WebApp.SettingPage;
using WebExpress.WebApp.WebPage;
using WebExpress.WebPage;

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
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public override void Process(RenderContextWebApp context)
        {
            base.Process(context);

            var visualTree = context.VisualTree;

            var path = SettingPageManager.FindPage(Context.ApplicationID, ID);
            if (path != null)
            {
                var contexts = SettingPageManager.GetContexts(Context.ApplicationID);
                var section = SettingPageManager.GetSections(Context.ApplicationID, path?.Context);

                // SettingMenü
                var settingMenu = new ControlNavigation("settingmenu")
                {
                    Layout = TypeLayoutTab.Pill,
                    Orientation = TypeOrientationTab.Vertical,
                    GridColumn = new PropertyGrid(TypeDevice.Medium, 2)
                };

                AddSettingMenu(section, SettingSection.Preferences, settingMenu);
                AddSettingMenu(section, SettingSection.Primary, settingMenu);
                AddSettingMenu(section, SettingSection.Secondary, settingMenu);

                visualTree.Sidebar.Primary.Add(settingMenu);

                // SettingTab
                var settingTab = new ControlNavigation("settingtab")
                {
                    Layout = TypeLayoutTab.Tab,
                    Orientation = TypeOrientationTab.Horizontal
                };

                foreach (var settingContext in contexts.Select(x => new { Name = this.I18N(x.Key), ContextName = x.Key, Sections = x.Value }).Where(x => x.Name != "*").OrderBy(x => x.Name))
                {
                    var firstPage = settingContext.Sections.FindFirstPage();

                    if (firstPage != null)
                    {
                        settingTab.Items.Add(new ControlNavigationItemLink()
                        {
                            Uri = new UriResource(Context.ContextPath, firstPage.Page.Node?.ExpressionPath),
                            Text = settingContext.Name,
                            Active = path.Context == settingContext.ContextName ? TypeActive.Active : TypeActive.None,
                        });
                    }
                }

                if (settingTab.Items.Count > 1)
                {
                    visualTree.Prologue.Content.Add(settingTab);
                }
            }
        }

        /// <summary>
        /// Fügt die Links der Einstellungsseiten ein
        /// </summary>
        /// <param name="sections">Die Sektionen</param>
        /// <param name="section">Die ausgewählte Sektion</param>
        /// <param name="control">Die Sektion, welche eingefügt werden soll</param>
        private void AddSettingMenu(SettingPageDictionaryItemSection sections, SettingSection section, ControlNavigation control)
        {
            var groups = sections.GetGroups(section);

            if (groups == null)
            {
                return;
            }

            foreach (var group in groups.Select(x => new { Name = this.I18N(x.Key), Pages = x.Value }).OrderBy(x => x.Name))
            {
                control.Items.Add(new ControlNavigationItemHeader() { Text = group.Name });

                foreach (var page in group.Pages)
                {
                    if (!page.Hide)
                    {
                        control.Items.Add(new ControlNavigationItemLink()
                        {
                            Text = this.I18N(page.ModuleContext.PluginID, page.Node?.Title),
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
