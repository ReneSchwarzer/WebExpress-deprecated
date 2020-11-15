using WebExpress.Html;

namespace WebExpress.UI.Controls
{
    public class ControlBreadcrumb : Control
    {
        /// <summary>
        /// Liefert oder setzt die Uri
        /// </summary>
        public IUri Uri { get; set; }

        /// <summary>
        /// Liefert oder setzt das Rootelement
        /// </summary>
        public string EmptyName { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlBreadcrumb(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="uri">Der Verzeichnispfad</param>
        public ControlBreadcrumb(string id, IUri uri)
            : base(id)
        {
            Uri = uri;
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
            var html = new HtmlElementTextContentUl()
            {
                Class = Css.Concatenate("breadcrumb", GetClasses()),
                Style = GetStyles(),
            };

            for (int i = 1; i <= Uri.Path.Count; i++)
            {
                var path = Uri.Take(i);

                html.Elements.Add
                (
                    new HtmlElementTextContentLi
                    (
                        //new ControlIcon(Page)
                        //{ 
                        //    Icon = path.Icon
                        //}.ToHtml(),
                        new HtmlElementTextSemanticsA(path.Display)
                        {
                            Href = path.ToString()
                        }
                    )
                    {
                        Class = "breadcrumb-item"
                    }
                );
            }

            return html;
        }
    }
}
