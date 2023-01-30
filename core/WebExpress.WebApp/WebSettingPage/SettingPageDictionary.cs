using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.WebApplication;
using WebExpress.WebModule;
using WebExpress.WebPlugin;

namespace WebExpress.WebApp.SettingPage
{
    /// <summary>
    /// key = The context of the plugin.
    /// value = meta data
    /// </summary>
    public class SettingPageDictionary : Dictionary<IPluginContext, SettingPageDictionaryItemContext>
    {
        /// <summary>
        /// Adds a settings page.
        /// </summary>
        /// <param name="pluginContext">The context of the plugin.</param>
        /// <param name="item">The settings page to insert.</param>
        public void AddPage(IPluginContext pluginContext, SettingPageDictionaryItem item)
        {
            if (!ContainsKey(pluginContext))
            {
                Add(pluginContext, new SettingPageDictionaryItemContext());
            }

            this[pluginContext].AddPage(item.Context, item.Section, item.Group, item);
        }

        /// <summary>
        /// Finds a settings page in an application by its id.
        /// </summary>
        /// <param name="application">The context of the application.</param>
        /// <param name="module">The context of the module.</param>
        /// <param name="pageID">The id of the setting page.</param>
        /// <returns>The setting page found or null.</returns>
        /// <summary>
        public SettingPageSearchResult FindPage(IApplicationContext application, IModuleContext module, string pageID)
        {
            var results = Values
                .SelectMany(c => c.Values)
                .SelectMany(s => s.Values)
                .SelectMany(g => g.Values)
                .SelectMany(i => i)
                .Where(x => x != null && x.ModuleContext.ModuleID.Equals(module.ModuleID, StringComparison.OrdinalIgnoreCase))
                .Select(x => new SettingPageSearchResult()
                {
                    Context = x.Context,
                    Section = x.Section,
                    Group = x.Group,
                    Item = x
                });

            return results.FirstOrDefault();
        }

        /// <summary>
        /// Liefere alle Kontexte
        /// </summary>
        /// <param name="applicationID">Die AnwendungsID</param>
        /// <returns>Eine Auflistung aller Kontexte</returns>
        /// <summary>
        public SettingPageDictionaryItemContext GetContexts(string applicationID)
        {
            var results = Values
                .Where(
                    x => x.SelectMany(c => c.Value)
                          .SelectMany(s => s.Value)
                          .SelectMany(g => g.Value)
                          .Where(x => x != null && x.Resource.Context.GetApplicationContexts().Where(x => x.ApplicationID.Equals(applicationID, StringComparison.OrdinalIgnoreCase)).Any())
                          .Any()
                );

            return results.FirstOrDefault();
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
            var results = Values
                .SelectMany(x => x.Values)
                .Where(
                    x => x.SelectMany(s => s.Value)
                          .SelectMany(g => g.Value)
                          .Where(x => x != null && x.Resource.Context.GetApplicationContexts().Where(x => x.ApplicationID.Equals(applicationID, StringComparison.OrdinalIgnoreCase)).Any())
                          .Any()
                );

            return results.FirstOrDefault();
        }
    }
}
