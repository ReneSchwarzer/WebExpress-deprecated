using System.Collections.Generic;
using System.Linq;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.WebApp.SettingPage
{
    public class SettingPageDictionaryItemGroup : Dictionary<string, List<SettingPageDictionaryItemMetaPage>>
    {
        /// <summary>
        /// Fügt eine Seite hinzu
        /// </summary>
        /// <param name="group">Die Gruppe</param>
        /// <param name="page">Die einzufügende Seite</param>
        public void AddPage(string group, SettingPageDictionaryItemMetaPage page)
        {
            group ??= string.Empty;

            // Sektion registrieren
            if (!ContainsKey(group))
            {
                Add(group, new List<SettingPageDictionaryItemMetaPage>());
            }

            this[group].Add(page);
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
                var page = v.Value.Where(x => x.ID == pageID).FirstOrDefault();
                if (page != null)
                {
                    return new SettingPageSearchResult() { Group = v.Key, Page = page };
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
            var firstPage = null as SettingPageDictionaryItemMetaPage;

            foreach (var group in this.OrderBy(x => x.Key))
            {
                firstPage = group.Value.FirstOrDefault();

                if (firstPage != null)
                {
                    return new SettingPageSearchResult() { Group = group.Key, Page = firstPage };
                }
            }

            return null;
        }

        /// <summary>
        /// Liefere alle Seiten, welche sich in der gegebenen Gruppe befinden
        /// </summary>
        /// <param name="group">Die Gruppe</param>
        /// <returns>Eine Auflistung aller Seiten der gleichen Gruppe</returns>
        public List<SettingPageDictionaryItemMetaPage> GetPages(string group)
        {
            if (ContainsKey(group))
            {
                return this[group];
            }

            return null;
        }
    }
}
