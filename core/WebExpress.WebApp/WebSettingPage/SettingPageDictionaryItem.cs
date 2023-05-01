using WebExpress.UI.WebControl;
using WebExpress.WebApp.WebAttribute;
using WebExpress.WebModule;
using WebExpress.WebResource;

namespace WebExpress.WebApp.SettingPage
{
    public class SettingPageDictionaryItem
    {
        /// <summary>
        /// Returns the id of the setting page.
        /// </summary>
        public string ID { get; internal set; }

        /// <summary>
        /// Returns the mudule context.
        /// </summary>
        public IModuleContext ModuleContext { get; internal set; }

        /// <summary>
        /// Returns the resource.
        /// </summary>
        public IResourceContext Resource { get; internal set; }

        /// <summary>
        /// Determines whether the page should be displayed or hidden.
        /// </summary>
        public bool Hide { get; internal set; }

        /// <summary>
        /// Liefert oder setzt das Icon
        /// </summary>
        public PropertyIcon Icon { get; internal set; }

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
    }
}
