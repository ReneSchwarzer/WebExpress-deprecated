using System;
using WebExpress.UI.WebControl;
using WebExpress.WebModule;
using WebExpress.WebResource;

namespace WebExpress.WebApp.SettingPage
{
    public class SettingPageDictionaryItemMetaPage
    {
        /// <summary>
        /// Liefert oder setzt die ID der Seite
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Liefert oder setzt den Modulkontext
        /// </summary>
        public IModuleContext ModuleContext { get; set; }

        /// <summary>
        /// Liefert oder setzt den Seitenklasse
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Liefert oder setzt die den Sitemapeintrag zu der Seite
        /// </summary>
        public SitemapNode Node { get; set; }

        /// <summary>
        /// Bestimmt ob die Seite angezeigt werden soll oder ausgeblendet wird
        /// </summary>
        public bool Hide { get; set; }

        /// <summary>
        /// Liefert oder setzt das Icon
        /// </summary>
        public PropertyIcon Icon { get; set; }
    }
}
