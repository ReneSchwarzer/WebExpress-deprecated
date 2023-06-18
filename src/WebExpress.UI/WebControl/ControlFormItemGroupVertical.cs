using WebExpress.WebHtml;
using WebExpress.Internationalization;

namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Grouping of controls.
    /// </summary>
    public class ControlFormItemGroupVertical : ControlFormItemGroup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlFormItemGroupVertical(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        ///<param name="items">The form controls.</param> 
        public ControlFormItemGroupVertical(string id, params ControlFormItem[] items)
            : base(id, items)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        ///<param name="items">The form controls.</param> 
        public ControlFormItemGroupVertical(params ControlFormItem[] items)
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
                Class = Css.Concatenate("", GetClasses()),
                Style = GetStyles(),
            };

            foreach (var item in Items)
            {
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

                    html.Elements.Add(fieldset);
                }
                else
                {
                    html.Elements.Add(item?.Render(context));
                }
            }

            return html;
        }
    }
}
