using WebExpress.WebHtml;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.UI.WebControl
{
    public class ControlFormItemLabel : ControlFormItem
    {
        /// <summary>
        /// Returns or sets the text of the label.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Returns or sets the associated form field.
        /// </summary>
        public ControlFormItem FormularItem { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlFormItemLabel(string id)
            : base(id)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="text">The text.</param>
        public ControlFormItemLabel(string id, string text)
            : this(id)
        {
            Text = text;
        }

        /// <summary>
        /// Initializes the form element.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        public override void Initialize(RenderContextFormular context)
        {
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContextFormular context)
        {
            return new HtmlElementFieldLabel()
            {
                Text = I18N(context.Culture, Text),
                Class = GetClasses(),
                Style = GetStyles(),
                Role = Role,
                For = FormularItem != null ?
                    string.IsNullOrWhiteSpace(FormularItem.Id) ?
                    FormularItem.Name :
                    FormularItem.Id :
                    null
            };
        }
    }
}
