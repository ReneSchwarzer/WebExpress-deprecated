using WebExpress.Html;

namespace WebExpress.UI.Controls
{
    public class ControlHtml : Control
    {
        /// <summary>
        /// Liefert oder setzt den HTML-Quelltext
        /// </summary>
        public string Html { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlHtml(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="value">Der Text</param>
        public ControlHtml(string id, int value)
            : base(id)
        {
            Html = value.ToString();

            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
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
