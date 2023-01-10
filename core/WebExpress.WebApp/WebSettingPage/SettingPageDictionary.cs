using System.Collections.Generic;
using WebExpress.WebApp.WebAttribute;

namespace WebExpress.WebApp.SettingPage
{
    public class SettingPageDictionary : Dictionary<string, SettingPageDictionaryItemContext>
    {
        /// <summary>
        /// Fügt eine Seite hinzu
        /// </summary>
        /// <param name="applicationID">Die AnwendungsID</param>
        /// <param name="context">The context.</param>
        /// <param name="section">Die Sektion</param>
        /// <param name="group">Die Gruppe</param>
        /// <param name="page">Die einzufügende Seite</param>
        public void AddPage(string applicationID, string context, SettingSection section, string group, SettingPageDictionaryItemMetaPage page)
        {
            // Standard für Anwendung festlegen
            if (string.IsNullOrWhiteSpace(applicationID))
            {
                applicationID = "*";
            }

            if (!ContainsKey(applicationID))
            {
                Add(applicationID, new SettingPageDictionaryItemContext());
            }

            this[applicationID].AddPage(context, section, group, page);
        }

        /// <summary>
        /// Sucht eine Seite anhand seiner ID
        /// </summary>
        /// <param name="applicationID">Die AnwendungsID</param>
        /// <param name="pageID">Die Seite</param>
        /// <returns>Die gefundene Seite oder null</returns>
        /// <summary>
        public SettingPageSearchResult FindPage(string applicationID, string pageID)
        {
            if (ContainsKey(applicationID))
            {
                return this[applicationID].FindPage(pageID);
            }

            return null;
        }

        /// <summary>
        /// Liefere alle Kontexte
        /// </summary>
        /// <param name="applicationID">Die AnwendungsID</param>
        /// <returns>Eine Auflistung aller Kontexte</returns>
        /// <summary>
        public SettingPageDictionaryItemContext GetContexts(string applicationID)
        {
            if (ContainsKey(applicationID))
            {
                return this[applicationID];
            }

            return null;
        }

        /// <summary>
        /// Liefere alle Sektionen, welche den seleben Settingkontext besitzen
        /// </summary>
        /// <param name="applicationID">Die AnwendungsID</param>
        /// <param name="context">The context.</param>
        /// <returns>Eine Auflistung aller Sektionen des gleichen Kontextes</returns>
        /// <summary>
        public SettingPageDictionaryItemSection GetSections(string applicationID, string context)
        {
            if (ContainsKey(applicationID))
            {
                return this[applicationID].GetSections(context);
            }

            return null;
        }
    }
}
