using WebExpress.WebApplication;

namespace WebExpress.Internationalization
{
    public static class InternationalizationExtensions
    {
        /// <summary>
        /// Internationalisierung
        /// </summary>
        /// <param name="obj">Das Objekt, welches erweitert wird</param>
        /// <param name="key">Der Schlüssel</param>
        /// <returns>Der Wert des Schlüssels in der aktuellen Sprache</returns>
        public static string I18N(this II18N obj, string key)
        {
            return InternationalizationManager.I18N(obj, key);
        }

        /// <summary>
        /// Internationalisierung
        /// </summary>
        /// <param name="obj">Das Objekt, welches erweitert wird</param>
        /// <param name="pluginID">Die PluginID</param>
        /// <param name="key">Der Schlüssel</param>
        /// <returns>Der Wert des Schlüssels in der aktuellen Sprache</returns>
        public static string I18N(this II18N obj, string pluginID, string key)
        {
            return InternationalizationManager.I18N(obj.Culture, pluginID, key);
        }

        /// <summary>
        /// Internationalisierung
        /// </summary>
        /// <param name="obj">Das Objekt, welches erweitert wird</param>
        /// <param name="application">Die Anwendung</param>
        /// <param name="key">Der Schlüssel</param>
        /// <returns>Der Wert des Schlüssels in der aktuellen Sprache</returns>
        public static string I18N(this II18N obj, IApplicationContext application, string key)
        {
            return InternationalizationManager.I18N(obj.Culture, application?.Plugin?.PluginID, key);
        }
    }
}
