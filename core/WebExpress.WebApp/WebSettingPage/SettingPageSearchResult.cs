using WebExpress.WebApp.WebAttribute;

namespace WebExpress.WebApp.SettingPage
{
    public class SettingPageSearchResult
    {
        /// <summary>
        /// Liefert oder setzt den Kontext
        /// </summary>
        public string Context { get; set; }

        /// <summary>
        /// Liefert oder setzt die Sektion
        /// </summary>
        public SettingSection Section { get; set; }

        /// <summary>
        /// Liefert oder setzt die Gruppe
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// Liefert oder setzt die Seite
        /// </summary>
        public SettingPageDictionaryItemMetaPage Page { get; set; }
    }
}
