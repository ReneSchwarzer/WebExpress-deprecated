using WebExpress.Html;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlNavigationItemHeader : Control, IControlNavigationItem
    {
        /// <summary>
        /// Liefert oder setzt die Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlNavigationItemHeader(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="text">Der Text</param>
        public ControlNavigationItemHeader(string id, string text)
            : base(id)
        {
            Text = text;

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
            return new HtmlElementTextContentLi(new HtmlText(Text))
            {
                ID = ID,
                Class = Css.Concatenate("dropdown-header", GetClasses()),
                Style = GetStyles(),
                Role = Role
            };
        }
    }
}
