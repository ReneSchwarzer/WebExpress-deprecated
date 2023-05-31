using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.UI.WebControl
{
    public class ControlFormItemInputComboBox : ControlFormItemInput
    {
        /// <summary>
        /// Returns or sets the entries.
        /// </summary>
        public List<ControlFormItemInputComboBoxItem> Items { get; private set; } = new List<ControlFormItemInputComboBoxItem>();

        ///// <summary>
        ///// Returns or sets the selected item.
        ///// </summary>
        //public string Selected { get; set; }

        /// <summary>
        /// Returns or sets the OnChange attribute.
        /// </summary>
        public PropertyOnChange OnChange { get; set; }

        ///// <summary>
        ///// Returns or sets the selected item. anhand des Wertes
        ///// </summary>
        //public string SelectedValue { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
        public ControlFormItemInputComboBox(string id = null)
            : base(id)
        {
            Name = id;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
        /// <param name="items">The ComboBox entries.</param>
        public ControlFormItemInputComboBox(string id, params string[] items)
            : this(id)
        {
            Items.AddRange(from v in items select new ControlFormItemInputComboBoxItem() { Text = v });
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="items">The ComboBox entries.</param>
        public ControlFormItemInputComboBox(string id, params ControlFormItemInputComboBoxItem[] items)
            : this(id)
        {
            Items.AddRange(items);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
        /// <param name="items">The ComboBox entries.</param>
        public ControlFormItemInputComboBox(string id, string name, IEnumerable<ControlFormItemInputComboBoxItem> items)
            : this(id, name)
        {
            Items.AddRange(items);
        }

        /// <summary>
        /// Initializes the form element.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        public override void Initialize(RenderContextFormular context)
        {
            Value = context.Request.GetParameter(Name)?.Value;
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContextFormular context)
        {
            var html = new HtmlElementFieldSelect()
            {
                Id = Id,
                Name = Name,
                Class = Css.Concatenate("form-select", GetClasses()),
                Style = GetStyles(),
                Role = Role,
                Disabled = Disabled,
                OnChange = OnChange?.ToString()
            };

            foreach (var v in Items)
            {
                if (v.SubItems.Count > 0)
                {
                    html.Elements.Add(new HtmlElementFormOptgroup() { Label = v.Text });
                    foreach (var s in v.SubItems)
                    {
                        html.Elements.Add(new HtmlElementFormOption() { Value = s.Value, Text = I18N(context.Culture, s.Text), Selected = (s.Value == Value) });
                    }
                }
                else
                {
                    html.Elements.Add(new HtmlElementFormOption() { Value = v.Value, Text = I18N(context.Culture, v.Text), Selected = (v.Value == Value) });
                }
            }

            return html;
        }

        /// <summary>
        /// Checks the input element for correctness of the data.
        /// </summary>
        /// <param name="context">The context in which the inputs are validated.</param>
        public override void Validate(RenderContextFormular context)
        {
            base.Validate(context);
        }
    }
}
