using System.Collections.Generic;
using WebExpress.WebApp.WebAttribute;
using WebExpress.WebApplication;
using WebExpress.WebPlugin;

namespace WebExpress.WebApp.SettingPage
{
    /// <summary>
    /// key = The context of the plugin.
    /// value = 
    /// </summary>
    public class SettingPageDictionary : Dictionary<IPluginContext, SettingPageDictionaryItemContext>
    {
        /// <summary>
        /// Adds a settings page.
        /// </summary>
        /// <param name="pluginContext">The context of the plugin.</param>
        /// <param name="context">The context.</param>
        /// <param name="section">The section.</param>
        /// <param name="group">The grup.</param>
        /// <param name="page">The settings page to insert.</param>
        public void AddPage(IPluginContext pluginContext, string context, SettingSection section, string group, SettingPageDictionaryItemMetaPage page)
        {
            if (!ContainsKey(pluginContext))
            {
                Add(pluginContext, new SettingPageDictionaryItemContext());
            }

            this[pluginContext].AddPage(context, section, group, page);
        }

        /// <summary>
        /// Finds a settings page in an application by its id.
        /// </summary>
        /// <param name="applicationContext">Die AnwendungsID</param>
        /// <param name="pageID">Die Seite</param>
        /// <returns>Die gefundene Seite oder null</returns>
        /// <summary>
        public SettingPageSearchResult FindPage(IApplicationContext applicationContext, string pageID)
        {
            //if (ContainsKey(applicationContext))
            //{
            //    return this[applicationContext].FindPage(pageID);
            //}

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
            //if (ContainsKey(applicationID))
            //{
            //    return this[applicationID];
            //}

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
            //if (ContainsKey(applicationID))
            //{
            //    return this[applicationID].GetSections(context);
            //}

            return null;
        }
    }
}
