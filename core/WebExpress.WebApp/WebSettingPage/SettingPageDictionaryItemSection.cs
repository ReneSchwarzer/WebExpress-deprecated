using System.Collections.Generic;
using WebExpress.WebApp.WebAttribute;

namespace WebExpress.WebApp.SettingPage
{
    public class SettingPageDictionaryItemSection : Dictionary<SettingSection, SettingPageDictionaryItemGroup>
    {
        /// <summary>
        /// Fügt eine Seite hinzu
        /// </summary>
        /// <param name="section">Die Sektion</param>
        /// <param name="group">Die Gruppe</param>
        /// <param name="page">Die einzufügende Seite</param>
        public void AddPage(SettingSection section, string group, SettingPageDictionaryItemMetaPage page)
        {
            // Sektion registrieren
            if (!ContainsKey(section))
            {
                Add(section, new SettingPageDictionaryItemGroup());
            }

            this[section].AddPage(group, page);
        }

        /// <summary>
        /// Sucht eine Seite anhand seiner ID
        /// </summary>
        /// <param name="pageID">Die Seite</param>
        /// <returns>Die gefundene Seite oder null</returns>
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
        /// Ermittelt die erste Seite
        /// </summary>
        /// <returns>Die erste Seite</returns>
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
        /// Liefere alle Gruppen, welche sich in der gegebenen Sektion befinden
        /// </summary>
        /// <param name="section">Die Sektion</param>
        /// <returns>Eine Auflistung aller Gruppen der gleichen Sektion</returns>
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
