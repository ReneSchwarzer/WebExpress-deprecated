using WebExpress.WebHtml;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.UI.WebControl
{
    public class ControlFormItemHelpText : ControlFormItem
    {
        /// <summary>
        /// Returns or sets the size of the text.
        /// </summary>
        public PropertySizeText Size
        {
            get => (PropertySizeText)GetPropertyObject();
            set => SetProperty(value, () => value?.ToClass(), () => value?.ToStyle());
        }

        /// <summary>
        /// Returns or sets the help text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlFormItemHelpText(string id)
            : base(id)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="text">The text.</param>
        public ControlFormItemHelpText(string id, string text)
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
            TextColor = new PropertyColorText(TypeColorText.Muted);
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContextFormular context)
        {
            return new HtmlElementTextSemanticsSmall()
            {
                Text = I18N(context.Culture, Text),
                Class = Css.Concatenate("", GetClasses()),
                Style = GetStyles(),
                Role = Role
            };
        }
    }
}
