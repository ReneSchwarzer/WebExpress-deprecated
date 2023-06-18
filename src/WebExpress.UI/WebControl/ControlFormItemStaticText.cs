using System.Collections.Generic;
using WebExpress.WebHtml;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.UI.WebControl
{
    public class ControlFormItemStaticText : ControlFormItem, IControlFormLabel
    {
        /// <summary>
        /// Returns or sets the label.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Returns or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlFormItemStaticText(string id = null)
            : base(id)
        {
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
            var c = new List<string>
            {
                "form-control-static"
            };

            var html = new HtmlElementTextContentP()
            {
                Text = I18N(context.Culture, Text),
                Class = Css.Concatenate(GetClasses()),
                Style = Style.Concatenate(GetStyles()),
                Role = Role
            };

            return html;
        }
    }
}
