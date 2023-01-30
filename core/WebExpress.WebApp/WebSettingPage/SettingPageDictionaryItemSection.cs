using System.Collections.Generic;
using WebExpress.WebApp.WebAttribute;

namespace WebExpress.WebApp.SettingPage
{
    public class SettingPageDictionaryItemSection : Dictionary<SettingSection, SettingPageDictionaryItemGroup>
    {
        /// <summary>
        /// Adds an item.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="group">The group.</param>
        /// <param name="item">The item to insert.</param>
        public void AddPage(SettingSection section, string group, SettingPageDictionaryItem item)
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
        /// <param name="pageID">The setting site id.</param>
        /// <returns>The setting page found or null.</returns>
        public SettingPageSearchResult FindPage(string pageID)
        {
            foreach (var v in this)
            {
                var path = v.Value.FindPage(pageID);
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

            if (ContainsKey(SettingSection.Preferences))
            {
                firstPage = this[SettingSection.Preferences].FindFirstPage();
                if (firstPage != null)
                {
                    firstPage.Section = SettingSection.Preferences;

                    return firstPage;
                }
            }
            else if (ContainsKey(SettingSection.Primary))
            {
                firstPage = this[SettingSection.Primary].FindFirstPage();
                if (firstPage != null)
                {
                    firstPage.Section = SettingSection.Primary;

                    return firstPage;
                }
            }
            else if (ContainsKey(SettingSection.Secondary))
            {
                firstPage = this[SettingSection.Secondary].FindFirstPage();
                if (firstPage != null)
                {
                    firstPage.Section = SettingSection.Secondary;

                    return firstPage;
                }
            }

            return firstPage;
        }

        /// <summary>
        /// Returns all groups that are in the given section.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <returns>A listing of all groups in the same section.</returns>
        public SettingPageDictionaryItemGroup GetGroups(SettingSection section)
        {
            if (ContainsKey(section))
            {
                return this[section];
            }

            return null;
        }
    }
}
