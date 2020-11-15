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
        /// <param name="fallback">Fallbackszenario, wenn der Schlüssel nicht gefunden wird</param>
        /// <returns>Der Wert des Schlüssels in der aktuellen Sprache</returns>
        public static string I18N(this RenderContext context, string key, string fallback = null)
        {
            return Internationalization.InternationalizationExtensions.I18N(context?.Page, context?.Page.GetType().Assembly, key, fallback);
        }

        /// <summary>
        /// Internationalisierung
        /// </summary>
        /// <param name="context">Das Objekt, welches erweitert wird</param>
        /// <param name="assembly">Das Assembly, indem sich der Schlüssel-Wertpaar befindet</param>
        /// <param name="key">Der Schlüssel</param>
        /// <param name="fallback">Fallbackszenario, wenn der Schlüssel nicht gefunden wird</param>
        /// <returns>Der Wert des Schlüssels in der aktuellen Sprache</returns>
        public static string I18N(this RenderContext context, Assembly assembly, string key, string fallback = null)
        {
            return Internationalization.InternationalizationExtensions.I18N(context?.Page, assembly, key, fallback);
        }
    }
}
