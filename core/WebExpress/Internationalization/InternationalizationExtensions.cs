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
        /// <param name="fallback">Fallbackszenario, wenn der Schlüssel nicht gefunden wird</param>
        /// <returns>Der Wert des Schlüssels in der aktuellen Sprache</returns>
        public static string I18N(this IPage page, string key, string fallback = null)
        {
            return I18N(page, page.GetType().Assembly, key, fallback);
        }

        /// <summary>
        /// Internationalisierung
        /// </summary>
        /// <param name="page">Das Objekt, welches erweitert wird</param>
        /// <param name="assembly">Das Assembly, indem sich der Schlüssel-Wertpaar befindet</param>
        /// <param name="key">Der Schlüssel</param>
        /// <param name="fallback">Fallbackszenario, wenn der Schlüssel nicht gefunden wird</param>
        /// <returns>Der Wert des Schlüssels in der aktuellen Sprache</returns>
        public static string I18N(this IPage page, Assembly assembly, string key, string fallback = null)
        {
            var language = page?.Request?.HeaderFields?.AcceptLanguage?.TrimStart().Substring(0, 2).ToLower();

            if (string.IsNullOrWhiteSpace(key))
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(language) || language == "*")
            {
                language = "en";
            }

            var item = InternationalizationDictionary.Instance[language];
            var assemblyName = assembly.GetName().Name.ToLower();

            if (item.ContainsKey(assemblyName + ":" + key.ToLower()))
            {
                return item[assemblyName + ":" + key.ToLower()];
            }

            if (string.IsNullOrWhiteSpace(fallback))
            {
                return key;
            }

            return fallback;
        }
    }
}
