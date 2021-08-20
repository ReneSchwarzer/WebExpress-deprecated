using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebApp.Attribute;
using WebExpress.WebResource;

namespace WebExpress.WebApp.WebResource
{
    /// <summary>
    /// Seite, welche für Einstellungen verwendet werden kann
    /// </summary>
    public abstract class PageTemplateWebAppSetting : PageTemplateWebApp, IPageSetting
    {
        /// <summary>
        /// Das Symbol der Einstellungsseite
        /// </summary>
        public PropertyIcon Icon { get; set; }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        public override void Process()
        {
            var dict = new Dictionary<SettingSection, Dictionary<string, List<SitemapNode>>>();
            var sitemap = ResourceManager.GetSitemap(Context.ModuleID);
            var pageContextAttributes = GetType().CustomAttributes.Where(x => x.AttributeType == typeof(SettingContextAttribute));
            var pageContexts = pageContextAttributes != null ? pageContextAttributes.Select(x => x.ConstructorArguments.FirstOrDefault().Value?.ToString()).ToList() : new List<string>();

            foreach (var page in sitemap.Where(x => x.Type != null && x.Type.GetInterface(typeof(IPageSetting).Name) != null))
            {
                var contextAttributes = page.Type.CustomAttributes.Where(x => x.AttributeType == typeof(SettingContextAttribute));
                var hideAttribute = page.Type.CustomAttributes.Where(x => x.AttributeType == typeof(SettingHideAttribute)).FirstOrDefault();
                var sectionAttribute = page.Type.CustomAttributes.Where(x => x.AttributeType == typeof(SettingSectionAttribute)).FirstOrDefault();
                var groupAttribute = page.Type.CustomAttributes.Where(x => x.AttributeType == typeof(SettingGroupAttribute)).FirstOrDefault();

                var contextsValue = contextAttributes != null ? contextAttributes.Select(x => x.ConstructorArguments.FirstOrDefault().Value?.ToString()).ToList() : new List<string>();
                var sectionValue = sectionAttribute != null ? (SettingSection)Enum.Parse(typeof(SettingSection), sectionAttribute?.ConstructorArguments.FirstOrDefault().Value?.ToString()) : SettingSection.Primary;
                var groupValue = groupAttribute?.ConstructorArguments.FirstOrDefault().Value?.ToString();
                groupValue = string.IsNullOrWhiteSpace(groupValue) ? string.Empty : groupValue.ToString();

                if (hideAttribute == null && ((pageContexts.Count == 0 && contextsValue.Count == 0) || pageContexts.Intersect(contextsValue).Any()))
                {
                    if (!dict.ContainsKey(sectionValue))
                    {
                        dict.Add(sectionValue, new Dictionary<string, List<SitemapNode>>());
                    }

                    var groupSection = dict[sectionValue];

                    if (!groupSection.ContainsKey(groupValue))
                    {
                        groupSection.Add(groupValue, new List<SitemapNode>());
                    }

                    groupSection[groupValue].Add(page);
                }
            }

            var settingMenu = new ControlNavigation("settingmenu")
            {
                Layout = TypeLayoutTab.Pill,
                Orientation = TypeOrientationTab.Vertical,
                GridColumn = new PropertyGrid(TypeDevice.Medium, 2)
            };

            // Preferences
            if (dict.ContainsKey(SettingSection.Preferences))
            {
                AddSettingPageLink(dict[SettingSection.Preferences], settingMenu);
            }

            // Primary
            if (dict.ContainsKey(SettingSection.Primary))
            {
                AddSettingPageLink(dict[SettingSection.Primary], settingMenu);
            }

            // Preferences
            if (dict.ContainsKey(SettingSection.Secondary))
            {
                AddSettingPageLink(dict[SettingSection.Secondary], settingMenu);
            }
            
            Sidebar.Primary.Add(settingMenu);

            base.Process();
        }

        /// <summary>
        /// Fügt die Links der Einstellungsseiten ein
        /// </summary>
        /// <param name="dict">Die Informationen mit den Einstellungsseiten</param>
        /// <param name="control">Die Sektion, welche eingefügt werden soll</param>
        private void AddSettingPageLink(Dictionary<string, List<SitemapNode>> dict, ControlNavigation control)
        {
            foreach (var group in dict.OrderBy(x => x.Key))
            {
                if (!string.IsNullOrWhiteSpace(group.Key))
                {
                    control.Items.Add(new ControlNavigationItemHeader() { Text = this.I18N(group.Key) });
                }

                foreach (var page in group.Value)
                {
                    var uri = new UriResource(Context.ContextPath, page.ExpressionPath);
                    var iconType = page.Type.CustomAttributes.Where(x => x.AttributeType == typeof(SettingIconAttribute)).FirstOrDefault();
                    var iconValue = iconType?.ConstructorArguments.FirstOrDefault().Value;
                    var icon = iconValue != null ? iconValue?.GetType() == typeof(int) ? new PropertyIcon((TypeIcon)Enum.Parse(typeof(TypeIcon), iconValue?.ToString())) : new PropertyIcon(new UriRelative(iconValue?.ToString())) : null;

                    control.Items.Add(new ControlNavigationItemLink()
                    {
                        Text = this.I18N(page.Title),
                        Icon = icon,
                        Uri = uri,
                        Active = page.Type == GetType() ? TypeActive.Active : TypeActive.None,
                        NoWrap = true
                    });
                }
            }
        }
    }
}
