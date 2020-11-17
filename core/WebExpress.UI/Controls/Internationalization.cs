using System.Reflection;

namespace WebExpress.UI.Controls
{
    public static class InternationalizationExtensions
    {
        /// <summary>
        /// Internationalisierung
        /// </summary>
        /// <param name="context">Das Objekt, welches erweitert wird</param>
        /// <param name="key">Der Schlüssel</param>
        /// <returns>Der Wert des Schlüssels in der aktuellen Sprache</returns>
        public static string I18N(this RenderContext context, string key)
        {
            return Internationalization.InternationalizationExtensions.I18N(context?.Page, context?.Page.Context.AppArtifactID, key);
        }

        /// <summary>
        /// Internationalisierung
        /// </summary>
        /// <param name="context">Das Objekt, welches erweitert wird</param>
        /// <param name="application">Die Anwendung, welche die Internationalisierungsdaten zugewiesen werden</param>
        /// <param name="key">Der Schlüssel</param>
        /// <returns>Der Wert des Schlüssels in der aktuellen Sprache</returns>
        public static string I18N(this RenderContext context, string application, string key)
          {
            return Internationalization.InternationalizationExtensions.I18N(context?.Page, application, key);
        }
    }
}
