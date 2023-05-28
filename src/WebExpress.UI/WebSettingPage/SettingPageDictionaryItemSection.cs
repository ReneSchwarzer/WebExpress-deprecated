using System.Collections.Generic;
using WebExpress.UI.WebAttribute;

namespace WebExpress.UI.SettingPage
{
    public class SettingPageDictionaryItemSection : Dictionary<WebExSettingSection, SettingPageDictionaryItemGroup>
    {
        /// <summary>
        /// Adds an item.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="group">The group.</param>
        /// <param name="item">The item to insert.</param>
        public void AddPage(WebExSettingSection section, string group, SettingPageDictionaryItem item)
        {
            // register Section
            if (!ContainsKey(section))
            {
                Add(section, new SettingPageDictionaryItemGroup());
            }

            this[section].AddPage(group, item);
        }

        /// <summary>
        /// Searches for an item based on its id.
        /// </summary>
        /// <param name="pageId">The setting site id.</param>
        /// <returns>The setting page found or null.</returns>
        public SettingPageSearchResult FindPage(string pageId)
        {
            foreach (var v in this)
            {
                var path = v.Value.FindPage(pageId);
                if (path != null)
                {
                    path.Section = v.Key;
                    return path;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the first setting page.
        /// </summary>
        /// <returns> The first setting page.</returns>
        public SettingPageSearchResult FindFirstPage()
        {
            var firstPage = null as SettingPageSearchResult;

            if (ContainsKey(WebExSettingSection.Preferences))
            {
                firstPage = this[WebExSettingSection.Preferences].FindFirstPage();
                if (firstPage != null)
                {
                    firstPage.Section = WebExSettingSection.Preferences;

                    return firstPage;
                }
            }
            else if (ContainsKey(WebExSettingSection.Primary))
            {
                firstPage = this[WebExSettingSection.Primary].FindFirstPage();
                if (firstPage != null)
                {
                    firstPage.Section = WebExSettingSection.Primary;

                    return firstPage;
                }
            }
            else if (ContainsKey(WebExSettingSection.Secondary))
            {
                firstPage = this[WebExSettingSection.Secondary].FindFirstPage();
                if (firstPage != null)
                {
                    firstPage.Section = WebExSettingSection.Secondary;

                    return firstPage;
                }
            }

            return firstPage;
        }

        /// <summary>
        /// Returns a groups that are in the given section.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <returns>A group in the same section.</returns>
        public SettingPageDictionaryItemGroup GetGroup(WebExSettingSection section)
        {
            if (ContainsKey(section))
            {
                return this[section];
            }

            return null;
        }
    }
}
