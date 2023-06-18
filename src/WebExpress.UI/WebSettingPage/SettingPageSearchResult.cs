using WebExpress.UI.WebAttribute;

namespace WebExpress.UI.SettingPage
{
    public class SettingPageSearchResult
    {
        /// <summary>
        /// Returns the setting context.
        /// </summary>
        public string Context { get; internal set; }

        /// <summary>
        /// Returns the section.
        /// </summary>
        public SettingSection Section { get; internal set; }

        /// <summary>
        /// Returns the group.
        /// </summary>
        public string Group { get; internal set; }

        /// <summary>
        /// A list of all currently existing setting contexts that can be accessed through the settings page.
        /// </summary>
        public SettingPageDictionaryItem Item { get; internal set; }
    }
}
