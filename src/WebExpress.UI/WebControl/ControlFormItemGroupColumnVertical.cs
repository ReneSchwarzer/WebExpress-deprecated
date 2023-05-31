using System.Linq;
using WebExpress.Html;
using WebExpress.Internationalization;

namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Grouping of controls.
    /// </summary>
    public class ControlFormItemGroupColumnVertical : ControlFormItemGroupColumn
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
        public ControlFormItemGroupColumnVertical(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Returns or sets the id.</param>
        ///<param name="items">The form controls.</param> 
        public ControlFormItemGroupColumnVertical(string id, params ControlFormItem[] items)
            : base(id, items)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        ///<param name="items">The form controls.</param> 
        public ControlFormItemGroupColumnVertical(params ControlFormItem[] items)
            : base(null, items)
        {
        }

        /// <summary>
        /// Initializes the form element.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        public override void Initialize(RenderContextFormular context)
        {
            var grpupContex = new RenderContextFormularGroup(context, this);

            foreach (var item in Items)
            {
                item.Initialize(grpupContex);
            }
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContextFormular context)
        {
            var renderContext = new RenderContextFormularGroup(context, this);

            var html = new HtmlElementTextContentDiv()
            {
                Id = Id,
                Class = Css.Concatenate("form-group-column", GetClasses()),
                Style = GetStyles(),
            };

            var max = 100;
            var offset = 0;

            foreach (var item in Items)
            {
                var div = new HtmlElementTextContentDiv() { Style = "" };
                var width = -1;

                if (Distribution.Count > offset)
                {
                    width = Distribution.Skip(offset).Take(1).FirstOrDefault();
                    div.Style = $"width: {width}%";
                    max = max - width;

                    offset++;
                }
                else if (Items.Count > offset)
                {
                    width = max / (Items.Count - offset);
                    div.Style = $"width: {width}%";
                }

                if (item is ControlFormItemInput input)
                {
                    var icon = new ControlIcon() { Icon = input?.Icon };
                    var label = new ControlFormItemLabel(!string.IsNullOrEmpty(item.Id) ? item.Id + "_label" : string.Empty);
                    var help = new ControlFormItemHelpText(!string.IsNullOrEmpty(item.Id) ? item.Id + "_help" : string.Empty);
                    var fieldset = new HtmlElementFormFieldset() { Class = "form-group" };

                    label.Initialize(renderContext);
                    help.Initialize(renderContext);

                    label.Text = context.I18N(input?.Label);
                    label.FormularItem = item;
                    help.Text = context.I18N(input?.Help);

                    if (icon.Icon != null)
                    {
                        icon.Classes.Add("me-2 pt-1");
                        fieldset.Elements.Add(new HtmlElementTextSemanticsSpan(icon.Render(renderContext), label.Render(renderContext))
                        {
                            Style = "display: flex;"
                        });
                    }
                    else
                    {
                        fieldset.Elements.Add(label.Render(renderContext));
                    }

                    fieldset.Elements.Add(item.Render(renderContext));

                    if (!string.IsNullOrWhiteSpace(input?.Help))
                    {
                        fieldset.Elements.Add(help.Render(renderContext));
                    }

                    div.Elements.Add(fieldset);
                }
                else
                {
                    div.Elements.Add(item.Render(context));
                }

                html.Elements.Add(div);
            }

            return html;
        }
    }
}
