using WebExpress.UI.WebAttribute;
using WebExpress.UI.WebControl;

namespace WebExpress.UI.SettingPage
{
    public class SettingPageDictionaryItem
    {
        /// <summary>
        /// Returns the id of the setting page.
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// Returns the mudule context.
        /// </summary>
        public string ModuleId { get; internal set; }

        /// <summary>
        /// Returns the resource.
        /// </summary>
        public string ResourceId { get; internal set; }

        /// <summary>
        /// Determines whether the page should be displayed or hidden.
        /// </summary>
        public bool Hide { get; internal set; }

        /// <summary>
        /// Returns or sets the icon.
        /// </summary>
        public PropertyIcon Icon { get; internal set; }

        /// <summary>
        /// Returns the setting context.
        /// </summary>
        public string Context { get; internal set; }

        /// <summary>
        /// Returns the section.
        /// </summary>
        public WebExSettingSection Section { get; internal set; }

        /// <summary>
        /// Returns the group.
        /// </summary>
        public string Group { get; internal set; }
    }
}
