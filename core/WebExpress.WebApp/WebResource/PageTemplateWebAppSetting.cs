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
            var dict = new Dictionary<string, List<SitemapNode>>();
            var sitemap = ResourceManager.GetSitemap(Context.ModuleID);
            var settings = new ControlNavigation()
            {
                Layout = TypeLayoutTab.Pill,
                Orientation = TypeOrientationTab.Vertical,
                GridColumn = new PropertyGrid(TypeDevice.Medium, 2)
            };

            foreach (var page in sitemap.Where(x => x.Type != null && x.Type.GetInterface(typeof(IPageSetting).Name) != null))
            {
                var hide = page.Type.CustomAttributes.Where(x => x.AttributeType == typeof(SettingHideAttribute)).FirstOrDefault();
                var group = page.Type.CustomAttributes.Where(x => x.AttributeType == typeof(SettingGroupAttribute)).FirstOrDefault();
                var groupValue = group?.ConstructorArguments.FirstOrDefault().Value?.ToString();
                groupValue = string.IsNullOrWhiteSpace(groupValue) ? string.Empty : groupValue.ToString();

                if (hide != null)
                {
                    if (!dict.ContainsKey(groupValue))
                    {
                        dict.Add(groupValue, new List<SitemapNode>());
                    }

                    dict[groupValue].Add(page);
                }
            }

            foreach (var group in dict.OrderBy(x => x.Key))
            {
                if (!string.IsNullOrWhiteSpace(group.Key))
                {
                    settings.Items.Add(new ControlNavigationItemHeader() { Text = this.I18N(group.Key) });
                }

                foreach (var page in group.Value)
                {
                    var uri = new UriResource(Context.ContextPath, page.ExpressionPath);
                    var iconType = page.Type.CustomAttributes.Where(x => x.AttributeType == typeof(SettingIconAttribute)).FirstOrDefault();
                    var iconValue = iconType?.ConstructorArguments.FirstOrDefault().Value;
                    var icon = iconValue != null ? iconValue?.GetType() == typeof(int) ? new PropertyIcon((TypeIcon)Enum.Parse(typeof(TypeIcon), iconValue?.ToString())) : new PropertyIcon(new UriRelative(iconValue?.ToString())) : null; 

                    settings.Items.Add(new ControlNavigationItemLink()
                    {
                        Text = this.I18N(page.Title),
                        Icon = icon,
                        Uri = uri,
                        Active = page.Title == Uri.Display ? TypeActive.Active : TypeActive.None
                    });
                }
            }

            Sidebar.Primary.Add(settings);
            base.Process();
        }
    }
}
