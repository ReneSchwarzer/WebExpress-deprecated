using System.Collections.Generic;

namespace WebExpress.UI.WebControl
{
    public class ControlFormItemInputComboBoxItem
    {
        public List<ControlFormItemInputComboBoxItem> SubItems { get; private set; }

        /// <summary>
        /// Returns or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Returns or sets a value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Returns or sets a tag value.
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ControlFormItemInputComboBoxItem()
        {
            SubItems = new List<ControlFormItemInputComboBoxItem>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="subitems">The child entries.</param>
        public ControlFormItemInputComboBoxItem(params ControlFormItemInputComboBoxItem[] subitems)
            : this()
        {
            SubItems.AddRange(subitems);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="subitems">The child entries.</param>
        public ControlFormItemInputComboBoxItem(IEnumerable<ControlFormItemInputComboBoxItem> subitems)
            : this()
        {
            SubItems.AddRange(subitems);
        }
    }
}
