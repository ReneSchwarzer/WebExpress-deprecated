using WebExpress.Html;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlHtml : Control
    {
        /// <summary>
        /// Liefert oder setzt den HTML-Quelltext
        /// </summary>
        public string Html { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlHtml(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="value">The text.</param>
        public ControlHtml(string id, int value)
            : base(id)
        {
            Html = value.ToString();

            Init();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private void Init()
        {
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var html = new HtmlRaw
            {
                Html = Html
            };

            return html;
        }
    }
}
