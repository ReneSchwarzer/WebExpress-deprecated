using System.Collections.Generic;
using WebExpress.WebApp.Attribute;

namespace WebExpress.WebApp.SettingPage
{
    public class SettingPageDictionaryItemContext : Dictionary<string, SettingPageDictionaryItemSection>
    {
        /// <summary>
        /// Fügt eine Seite hinzu
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <param name="section">Die Sektion</param>
        /// <param name="group">Die Gruppe</param>
        /// <param name="page">Die einzufügende Seite</param>
        public void AddPage(string context, SettingSection section, string group, SettingPageDictionaryItemMetaPage page)
        {
            context ??= "*";

            // Kontext registrieren
            if (!ContainsKey(context))
            {
                Add(context, new SettingPageDictionaryItemSection());
            }

            this[context].AddPage(section, group, page);
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
                    path.Context = v.Key;
                    return path;
                }
            }

            return null;
        }

        /// <summary>
        /// Liefere alle Sektionen, welche den seleben Settingkontext besitzen
        /// </summary>
        /// <param name="context">Der Kontext</param>
        /// <returns>Eine Auflistung aller Sektionen des gleichen Kontextes</returns>
        public SettingPageDictionaryItemSection GetSections(string context)
        {
            if (ContainsKey(context))
            {
                return this[context];
            }

            return null;
        }
    }
}
