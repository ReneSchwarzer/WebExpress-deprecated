using System.Linq;
using WebExpress.Html;
using WebExpress.Uri;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlAvatar : Control
    {
        /// <summary>
        /// Liefert oder setzt das Avatarbild
        /// </summary>
        public IUri Image { get; set; }

        /// <summary>
        /// Liefert oder setzt den Namen des Users
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Liefert oder setzt die Größe
        /// </summary>
        public TypeSizeButton Size
        {
            get => (TypeSizeButton)GetProperty(TypeSizeButton.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Liefert oder setzt einen modalen Dialag
        /// </summary>
        public ControlModal Modal { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlAvatar(string id = null)
            : base(id)
        {
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
            var img = null as HtmlElement;

            if (Image != null)
            {
                img = new HtmlElementMultimediaImg() { Src = Image.ToString(), Class = "" };
            }
            else if (!string.IsNullOrWhiteSpace(User))
            {
                var split = User.Split(' ');
                var i = split[0].FirstOrDefault().ToString();
                i += split.Count() > 1 ? split[1].FirstOrDefault().ToString() : "";

                img = new HtmlElementTextSemanticsB(new HtmlText(i))
                {
                    Class = "bg-info text-light"
                };
            }

            var html = new HtmlElementTextContentDiv(img, new HtmlText(User))
            {
                ID = ID,
                Class = Css.Concatenate("profile", GetClasses()),
                Style = GetStyles(),
                Role = Role
            };

            if (Modal != null)
            {
                html.AddUserAttribute("data-bs-toggle", "modal");
                html.AddUserAttribute("data-bs-target", "#" + Modal.ID);

                return new HtmlList(html, Modal.Render(context));
            }

            return html;
        }
    }
}
