using System.Collections.Generic;

namespace WebExpress.UI.WebControl
{
    public class ControlFormularItemInputComboBoxItem
    {
        public List<ControlFormularItemInputComboBoxItem> SubItems { get; private set; }

        /// <summary>
        /// Returns or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Returns or sets a value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Liefert oder setzt einen Tag-Wert
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ControlFormularItemInputComboBoxItem()
        {
            SubItems = new List<ControlFormularItemInputComboBoxItem>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="subitems">Die untergeordnetetn Einträge</param>
        public ControlFormularItemInputComboBoxItem(params ControlFormularItemInputComboBoxItem[] subitems)
            : this()
        {
            SubItems.AddRange(subitems);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="subitems">Die untergeordnetetn Einträge</param>
        public ControlFormularItemInputComboBoxItem(IEnumerable<ControlFormularItemInputComboBoxItem> subitems)
            : this()
        {
            SubItems.AddRange(subitems);
        }
    }
}
