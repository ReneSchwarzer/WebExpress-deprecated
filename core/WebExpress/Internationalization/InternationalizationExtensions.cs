using System.Reflection;
using WebExpress.Pages;

namespace WebExpress.Internationalization
{
    public static class InternationalizationExtensions
    {
        /// <summary>
        /// Internationalisierung
        /// </summary>
        /// <param name="page">Das Objekt, welches erweitert wird</param>
        /// <param name="key">Der Schlüssel</param>
        /// <returns>Der Wert des Schlüssels in der aktuellen Sprache</returns>
        public static string I18N(this IPage page, string key)
        {
            return I18N(page, page.Context.AppArtifactID, key);
        }

        /// <summary>
        /// Internationalisierung
        /// </summary>
        /// <param name="page">Das Objekt, welches erweitert wird</param>
        /// <param name="application">Die Anwendung, welche die Internationalisierungsdaten zugewiesen werden</param>
        /// <param name="key">Der Schlüssel</param>
        /// <returns>Der Wert des Schlüssels in der aktuellen Sprache</returns>
        public static string I18N(this IPage page, string application, string key)
        {
            return InternationalizationManager.I18N(page, application, key);
        }
    }
}
