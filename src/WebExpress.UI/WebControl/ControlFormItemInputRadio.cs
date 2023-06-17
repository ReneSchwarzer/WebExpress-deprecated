using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;

namespace WebExpress.UI.WebControl
{
    public class ControlFormItemInputRadio : ControlFormItemInput
    {
        /// <summary>
        /// Returns or sets the value of the optiopn.
        /// </summary>
        public string Option { get; set; }

        ///// <summary>
        ///// Returns or sets the value of the optiopn.
        ///// </summary>
        //public new string Value
        //{
        //    get => GetParam(Name);
        //    set
        //    {
        //        var v = GetParam(Name);

        //        if (string.IsNullOrWhiteSpace(v))
        //        {
        //            AddParam(Name, value, Formular.Scope);
        //        }
        //    }
        //}

        /// <summary>
        /// Liefert oder setzt ob die Checkbox in einer neuen Zeile angezeigt werden soll
        /// </summary>
        public bool Inline { get; set; }

        /// <summary>
        /// Returns or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Returns or sets a search pattern that checks the content.
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// Returns or sets whether the radio button is selected
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlFormItemInputRadio(string id = null)
            : base(id)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="name">The name.</param>
        public ControlFormItemInputRadio(string id, string name)
            : this(id)
        {
            Name = name;
        }

        /// <summary>
        /// Initializes the form element.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        public override void Initialize(RenderContextFormular context)
        {
            //AddParam(name, Formular.Scope);
            //Value = GetParam(Name);
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContextFormular context)
        {
            if (!string.IsNullOrWhiteSpace(Value))
            {
                Checked = Value == Option;
            }

            var c = new List<string>
            {
                "radio"
            };

            if (Inline)
            {
                c.Add("form-check-inline");
            }

            if (Disabled)
            {
                c.Add("disabled");
            }

            var html = new HtmlElementTextContentDiv
            (
                new HtmlElementFieldLabel
                (
                    new HtmlElementFieldInput()
                    {
                        Id = Id,
                        Name = Name,
                        Pattern = Pattern,
                        Type = "radio",
                        Disabled = Disabled,
                        Checked = Checked,
                        Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                        Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                        Role = Role,
                        Value = Option
                    },
                    new HtmlText(string.IsNullOrWhiteSpace(Description) ? string.Empty : "&nbsp;" + Description)
                )
                {
                }
            )
            {
                Class = string.Join(" ", c.Where(x => !string.IsNullOrWhiteSpace(x)))
            };

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
