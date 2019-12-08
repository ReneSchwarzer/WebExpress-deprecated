using System.Collections.Generic;

namespace WebExpress.UI.Controls
{
    public class ControlFormularItemComboBoxItem
    {
        public List<ControlFormularItemComboBoxItem> SubItems { get; private set; }

        /// <summary>
        /// Liefert oder setzt den Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Liefert oder setzt einen Wert
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Liefert oder setzt einen Tag-Wert
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ControlFormularItemComboBoxItem()
        {
            SubItems = new List<ControlFormularItemComboBoxItem>();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="subitems">Die untergeordnetetn Einträge</param>
        public ControlFormularItemComboBoxItem(params ControlFormularItemComboBoxItem[] subitems)
            : this()
        {
            SubItems.AddRange(subitems);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="subitems">Die untergeordnetetn Einträge</param>
        public ControlFormularItemComboBoxItem(IEnumerable<ControlFormularItemComboBoxItem> subitems)
            : this()
        {
            SubItems.AddRange(subitems);
        }
    }
}
