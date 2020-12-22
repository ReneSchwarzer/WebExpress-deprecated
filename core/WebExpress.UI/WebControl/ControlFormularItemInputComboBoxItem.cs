using System.Collections.Generic;

namespace WebExpress.UI.WebControl
{
    public class ControlFormularItemInputComboBoxItem
    {
        public List<ControlFormularItemInputComboBoxItem> SubItems { get; private set; }

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
        public ControlFormularItemInputComboBoxItem()
        {
            SubItems = new List<ControlFormularItemInputComboBoxItem>();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="subitems">Die untergeordnetetn Einträge</param>
        public ControlFormularItemInputComboBoxItem(params ControlFormularItemInputComboBoxItem[] subitems)
            : this()
        {
            SubItems.AddRange(subitems);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="subitems">Die untergeordnetetn Einträge</param>
        public ControlFormularItemInputComboBoxItem(IEnumerable<ControlFormularItemInputComboBoxItem> subitems)
            : this()
        {
            SubItems.AddRange(subitems);
        }
    }
}
