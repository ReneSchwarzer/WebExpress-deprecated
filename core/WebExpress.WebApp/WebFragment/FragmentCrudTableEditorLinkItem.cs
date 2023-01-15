using WebExpress.UI.WebControl;

namespace WebExpress.WebApp.WebFragment
{
    /// <summary>
    /// Metainformationen einer CRUD-Tabellenfunktion
    /// </summary>
    public class FragmentCrudTableEditorLinkItem : FragmentCrudTableEditorItem
    {
        /// <summary>
        /// Liefert oder setzt die Beschriftung der Splalte
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Liefert oder setzt das Icon
        /// </summary>
        public PropertyIcon Icon { get; set; }

        /// <summary>
        /// Liefert oder setzt die Farbe
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
