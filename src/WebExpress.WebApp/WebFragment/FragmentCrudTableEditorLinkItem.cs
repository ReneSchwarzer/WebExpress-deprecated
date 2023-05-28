using WebExpress.UI.WebControl;

namespace WebExpress.WebApp.WebFragment
{
    /// <summary>
    /// Metainformationen einer CRUD-Tabellenfunktion
    /// </summary>
    public class FragmentCrudTableEditorLinkItem : FragmentCrudTableEditorItem
    {
        /// <summary>
        /// Returns or sets the label. der Splalte
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Returns or sets the icon.
        /// </summary>
        public PropertyIcon Icon { get; set; }

        /// <summary>
        /// Returns or sets the color.
        /// </summary>
        public PropertyColorText Color { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="label">Die Beschriftung der Splalte</param>
        public FragmentCrudTableEditorLinkItem(string label)
        {
            Label = label;
        }
    }
}
