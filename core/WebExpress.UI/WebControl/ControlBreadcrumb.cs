using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.Uri;

namespace WebExpress.UI.WebControl
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
        /// Liefert oder setzt die Größe
        /// </summary>
        public TypeSizeButton Size
        {
            get => (TypeSizeButton)GetProperty(TypeSizeButton.Default);
            set => SetProperty(value, () => value.ToClass());
        }

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
            Size = TypeSizeButton.Small;
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
                Class = Css.Concatenate("breadcrumb rounded-0", GetClasses()),
                Style = GetStyles(),
            };

            if (Uri is UriResource resourceUri)
            {
                for (int i = 1; i< resourceUri.Path.Count + 1; i++)
                {
                    var path = resourceUri.Take(i);

                    if (path.Display != null)
                    {
                        var display = context.I18N(path.Display);
                        var href = path.ToString();

                        html.Elements.Add
                        (
                            new HtmlElementTextContentLi
                            (
                                //new ControlIcon(Page)
                                //{ 
                                //    Icon = path.Icon
                                //}.ToHtml(),
                                new HtmlElementTextSemanticsA(display)
                                {
                                    Href = href
                                }
                            )
                            {
                                Class = "breadcrumb-item"
                            }
                        );
                    }
                }
            }

            return html;
        }
    }
}
